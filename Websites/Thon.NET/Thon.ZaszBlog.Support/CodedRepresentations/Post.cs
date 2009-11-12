using System;
using System.Web;
using System.IO;
using System.Xml;
using System.Text;
using System.Net.Mail;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.ComponentModel;
using Thon.ZaszBlog.Support.DataServicesAndConfiguration;
using Thon.Support;

namespace Thon.ZaszBlog.Support.CodedRepresentations
{	
	//A post is an entry on the blog - a blog post.	
	[Serializable]
	public class Post : BusinessBase<Post, Guid>, IComparable<Post>, IShowed
	{
		public Post()
		{
			base.Id = Guid.NewGuid();
			_Comments = new List<Comment>();
			_Categories = new StateList<Category>();
			_Tags = new StateList<string>();
			DateCreated = DateTime.Now;
			_IsPublished = true;
			_IsCommentsEnabled = true;
        }

        #region Properties:Author,Title,Description,Content,Comments,ApprovedComments,NotApprovedComments,Categories,Tags,IsCommentsEnabled,IsPublished,Rating,Raters,Slug,NotificationEmails,IsVisible,Previous,Next

        private string _Author;
		public string Author
		{
			get { return _Author; }
			set
			{
				if (_Author != value) MarkChanged("Author");
				_Author = value;
			}
		}

		private string _Title;
		public string Title
		{
			get { return _Title; }
			set
			{
				if (_Title != value) MarkChanged("Title");
				_Title = value;
			}
		}

		private string _Description;
		public string Description
		{
			get { return _Description; }
			set
			{
				if (_Description != value) MarkChanged("Description");
				_Description = value;
			}
		}

		private string _Content;
		public string Content
		{
			get { return _Content; }
			set
			{
				if (_Content != value) MarkChanged("Content");
				_Content = value;
			}
		}
        
		private readonly List<Comment> _Comments;
		public List<Comment> ApprovedComments
		{
			get
			{
				return _Comments.FindAll(delegate(Comment obj)
				{
					return obj.IsApproved;
				});
			}
		}

        public List<Comment> NotApprovedComments
		{
			get
			{
				return _Comments.FindAll(delegate(Comment obj)
				{
					return !obj.IsApproved;
				});
			}
		}

        public List<Comment> Comments
		{
			get { return _Comments; }

		}

		private StateList<Category> _Categories;
        public StateList<Category> Categories
		{
			get { return _Categories; }
		}
				
		private StateList<string> _Tags;
        public StateList<string> Tags
		{
			get { return _Tags; }
		}
        
		private bool _IsCommentsEnabled;
		public bool IsCommentsEnabled
		{
			get { return _IsCommentsEnabled; }
			set
			{
				if (_IsCommentsEnabled != value) MarkChanged("IsCommentsEnabled");
				_IsCommentsEnabled = value;
			}
		}

		private bool _IsPublished;
		public bool IsPublished
		{
			get { return _IsPublished; }
			set
			{
				if (_IsPublished != value) MarkChanged("IsPublished");
				_IsPublished = value;
			}
		}

		private float _Rating;
		public float Rating
		{
			get { return _Rating; }
			set
			{
				if (_Rating != value) MarkChanged("Rating");
				_Rating = value;
			}
		}
        
		private int _Raters;
		public int Raters
		{
			get { return _Raters; }
			set
			{
				if (_Raters != value) MarkChanged("Raters");
				_Raters = value;
			}
		}

        // A Slug is the relative URL used by the posts.
        private string _Slug;
		public string Slug
		{
			get
			{
				if (string.IsNullOrEmpty(_Slug))
                    return SupportUtilities.RemoveIllegalCharacters(Title);
				return _Slug;
			}
			set { _Slug = value; }
		}

        // A collection of email addresses that is signed up for comment notification on the specific post.
        // After accessing this property we can use the Add() method to register emails here
        private StringCollection _NotificationEmails;
        public StringCollection NotificationEmails
		{
			get
			{
				if (_NotificationEmails == null)
					_NotificationEmails = new StringCollection();

				return _NotificationEmails;
			}
		}
        
		public bool IsVisible
		{
			get
			{
				if (IsPublished && DateCreated <= DateTime.Now)
					return true;

				return false;
			}
		}

        // Gets the previous post relative to this one based on time. Null, If this post is the oldest.
		private Post _Prev;
        public Post Previous
		{
			get { return _Prev; }
		}

        // Gets the next post relative to this one based on time. Null, If this post is the newest.
		private Post _Next;
        public Post Next
		{
			get { return _Next; }
		}

		#endregion

		#region Links
			
		public Uri PermaLink
		{
            // example : http://163.160.2.73:8080/ThonZNet/ZaszBlog/post.aspx?id=<GUID>
			get { return new Uri(SupportUtilities.AbsoluteWebRoot.ToString() + "post.aspx?id=" + Id.ToString()); }
		}

        // note that we include the string "post/" ---> this part is used by the UrlRewrite Module
        // to convert Requests pretty url to permalinks
		public string RelativeLink
		{
			get
			{
                //can be <post_title>.aspx, usuallu user enter the slug in UI if not its got from title
				string slug = SupportUtilities.RemoveIllegalCharacters(Slug) + BlogSettings.Instance.FileExtension;
                return SupportUtilities.RelativeWebRoot + "post/" + DateCreated.Year + "/" + DateCreated.ToString("MM") + "/" + slug;
                //can be ThonZNet/ZaszBlog/post/2009/08/<post_title>.aspx
            }
		}

		public Uri AbsoluteLink
		{
			get { return SupportUtilities.ConvertToAbsolute(RelativeLink); }
            // returns like http://163.160.2.73:8080/ThonZNet/ZaszBlog/post/2009/08/<post_title>.aspx
		}

        //PermaLink     :http://163.160.2.73:8080/ThonZNet/ZaszBlog/post.aspx?id=<GUID>  (   versus   )
        //AbsoluteLink  :http://163.160.2.73:8080/ThonZNet/ZaszBlog/post/2009/08/<post_title>.aspx

		
		// The trackback link to the post.		
		public Uri TrackbackLink
		{
            get { return new Uri(SupportUtilities.AbsoluteWebRoot.ToString() + "ZaszBlogHttpHandlers/TrackBack.ashx?id=" + Id.ToString()); }
            //http://163.160.2.73:8080/ThonZNet/ZaszBlog/ZaszBlogHttpHandlers/TrackBack.ashx?id=<post_id>
		}

		#endregion

		#region Methods

		private static object _SyncRoot = new object();
		private static List<Post> _Posts;		
		// A sorted collection of all posts in the blog.
		// Sorted by date.		
		public static List<Post> Posts
		{
			get
			{
				if (_Posts == null)
				{
					lock (_SyncRoot)
					{
						if (_Posts == null)
						{
							_Posts = StaticDataService.FillPosts();
							_Posts.TrimExcess();
							AddRelations();
						}
					}
				}

				return _Posts;
			}
		}

		private static void AddRelations()
		{
			for (int i = 0; i < _Posts.Count; i++)
			{
				if (i > 0)
					_Posts[i]._Next = _Posts[i - 1];

				if (i < _Posts.Count - 1)
					_Posts[i]._Prev = _Posts[i + 1];
			}
		}
		
		public static List<Post> GetPostsByCategory(Guid categoryId)
		{
			List<Post> col = Posts.FindAll(

            delegate(Post p) 
			{return p.Categories.Contains(Category.GetCategory(categoryId));}
            
            );
			col.Sort();
			col.TrimExcess();
			return col;
		}

        public static Post GetPost(Guid id)
		{
			return Posts.Find(

            delegate(Post p)
			{return p.Id == id;}
          
            );
		}
		
        //title must be unique becoz the title is part of the url
		public static bool IsTitleUnique(string title)
		{
			foreach (Post post in Posts)
			{
				if (SupportUtilities.RemoveIllegalCharacters(post.Title).Equals(SupportUtilities.RemoveIllegalCharacters(title), StringComparison.OrdinalIgnoreCase))
					return false;
			}
			return true;
		}

        //returns only one post - firts one matching the arguments
		public static Post GetPostBySlug(string slug, DateTime date)
		{
            //date has been given in the url like http://localhost:49174/ThonZNet/ZaszBlog/post/2007/12/Welcome-to-ZaszBlog.aspx            
            if (date == DateTime.MinValue)
            {
                return Posts.Find(

                delegate(Post p)
                {
                    return slug.Equals(SupportUtilities.RemoveIllegalCharacters(p.Slug), StringComparison.OrdinalIgnoreCase);
                }

                );
            }
            //date has'nt been given in the url like http://localhost:49174/ThonZNet/ZaszBlog/post/Welcome-to-ZaszBlog.aspx
            else
            {
               return Posts.Find(

               delegate(Post p)
               {
                   if (p.DateCreated.Year != date.Year || p.DateCreated.Month != date.Month)
                       return false;
                   return slug.Equals(SupportUtilities.RemoveIllegalCharacters(p.Slug), StringComparison.OrdinalIgnoreCase);
               }

               );
            }
		}

		public static List<Post> GetPostsByAuthor(string author)
		{
			List<Post> list = Posts.FindAll(

            delegate(Post p)
			{
				string legalTitle = SupportUtilities.RemoveIllegalCharacters(p.Author);
				return SupportUtilities.RemoveIllegalCharacters(author).Equals(legalTitle, StringComparison.OrdinalIgnoreCase);				
			}
            
            );
			list.TrimExcess();
			return list;
		}
		
		public static List<Post> GetPostsByTag(string tag)
		{
			List<Post> list = Posts.FindAll(
            
            delegate(Post p)
			{return p.Tags.Contains(tag);}
            
            );

			list.TrimExcess();
			return list;
		}
		
		public static List<Post> GetPostsByDate(DateTime dateFrom, DateTime dateTo)
		{
			List<Post> list = Posts.FindAll(

            delegate(Post p)
			{return (p.DateCreated.Date >= dateFrom && p.DateCreated.Date <= dateTo);}
            
            );

			list.TrimExcess();
			return list;
		}
        		
		// Adds a rating to the post.
		public void Rate(int rating)
		{
			if (Raters > 0)
			{
				float total = Raters * Rating;
				total += rating;
				Raters++;
				Rating = (float)(total / Raters);
			}
			else
			{
				Raters = 1;
				Rating = rating;
			}

			DataUpdate();
			OnRated(this);
		}
		
		// Imports Post (without all standard saving routines)
		public void Import()
		{               
			if (this.IsDeleted)
			{
				if (!this.IsNew)
				{
					StaticDataService.DeletePost(this);
				}
			}
			else
			{
				if (this.IsNew)
				{
					StaticDataService.InsertPost(this);
				}
				else
				{
					StaticDataService.UpdatePost(this);
				}
			}
		}
        
        // Force reload of all posts
		public static void Reload()
		{
			_Posts = StaticDataService.FillPosts();
			_Posts.Sort();
			AddRelations();
		}
		
		// Adds a comment to the collection and saves the post.
		public void AddComment(Comment comment)
		{
			CancelEventArgs e = new CancelEventArgs();
			OnAddingComment(comment, e);
			if (!e.Cancel)
			{
				Comments.Add(comment);
				DataUpdate();
				OnCommentAdded(comment);
				SendNotifications(comment);
			}
		}

        // Imports a comment to comment collection and saves.  Does not notify user or run extension events.		
		public void ImportComment(Comment comment)
		{
			Comments.Add(comment);
			DataUpdate();
		}
		
		private void SendNotifications(Comment comment)
		{
			if (NotificationEmails.Count == 0)
				return;

			MailMessage mail = new MailMessage();
			mail.From = new MailAddress(Thon.Support.ThonSettings.Instance.Email, BlogSettings.Instance.Name);
			mail.Subject = "New comment on " + Title;
			mail.Body = "Comment by " + comment.Author + Environment.NewLine + Environment.NewLine;
			mail.Body += comment.Content + "\n\n" + AbsoluteLink.ToString();

			foreach (string email in NotificationEmails)
			{
				if (email != comment.Email)
				{
					mail.To.Clear();
					mail.To.Add(email);
					SupportUtilities.SendMailMessageAsync(mail);
				}
			}
		}

		public void RemoveComment(Comment comment)
		{
			CancelEventArgs e = new CancelEventArgs();
			OnRemovingComment(comment, e);
			if (!e.Cancel)
			{
				Comments.Remove(comment);
				DataUpdate();
				OnCommentRemoved(comment);
			}
		}

		public void ApproveComment(Comment comment)
		{
			CancelEventArgs e = new CancelEventArgs();
			Comment.OnApproving(comment, e);
			if (!e.Cancel)
			{
				int index = Comments.IndexOf(comment);
				Comments[index].IsApproved = true;
				DataUpdate();
				Comment.OnApproved(comment);
			}
		}
		
		public void ApproveAllComments()
		{
			foreach (Comment comment in Comments)
			{
				ApproveComment(comment);
			}
		}

		#endregion

		#region Base overrides

		protected override void ValidationRules()
		{
			AddRule("Title", "Title must be set", String.IsNullOrEmpty(Title));
			AddRule("Content", "Content must be set", String.IsNullOrEmpty(Content));
		}

		protected override Post DataSelect(Guid id)
		{
			return StaticDataService.SelectPost(id);
		}

		protected override void DataUpdate()
		{
			StaticDataService.UpdatePost(this);
			Posts.Sort();
		}

		protected override void DataInsert()
		{
			StaticDataService.InsertPost(this);

			if (this.IsNew)
			{
				Posts.Add(this);
				Posts.Sort();
				AddRelations();
			}
		}

		protected override void DataDelete()
		{
			StaticDataService.DeletePost(this);
			if (Posts.Contains(this))
			{   
				Posts.Remove(this);
                AddRelations();
			}
		}

		public override bool IsChanged
		{
			get
			{
				if (base.IsChanged)
					return true;

				if (Categories.IsChanged || Tags.IsChanged)
					return true;

				return false;
			}
		}

		public override string ToString()
		{
			return Title;
		}

		public override void MarkOld()
		{
			this.Categories.MarkOld();
			this.Tags.MarkOld();
			base.MarkOld();
		}

		public new static Post Load(Guid id)
		{
			Post instance = new Post();
			instance = instance.DataSelect(id);
			instance.Id = id;
			if (instance != null)
			{
				instance.MarkOld();
				return instance;
			}
			return null;
		}

		#endregion

		#region IComparable<Post> Members

		public int CompareTo(Post other)
		{
			return other.DateCreated.CompareTo(this.DateCreated);
		}

		#endregion

		#region Events
        		
		public static event EventHandler<CancelEventArgs> AddingComment;
		protected virtual void OnAddingComment(Comment comment, CancelEventArgs e)
		{
			if (AddingComment != null)
			{
				AddingComment(comment, e);
			}
		}
		
		public static event EventHandler<EventArgs> CommentAdded;
		protected virtual void OnCommentAdded(Comment comment)
		{
			if (CommentAdded != null)
			{
				CommentAdded(comment, new EventArgs());
			}
		}

		public static event EventHandler<CancelEventArgs> RemovingComment;
		protected virtual void OnRemovingComment(Comment comment, CancelEventArgs e)
		{
			if (RemovingComment != null)
			{
				RemovingComment(comment, e);
			}
		}
		public static event EventHandler<EventArgs> CommentRemoved;
		protected virtual void OnCommentRemoved(Comment comment)
		{
			if (CommentRemoved != null)
			{
				CommentRemoved(comment, new EventArgs());
			}
		}

		public static event EventHandler<EventArgs> Rated;
		protected virtual void OnRated(Post post)
		{
			if (Rated != null)
			{
				Rated(post, new EventArgs());
			}
		}
		public static event EventHandler<ServingEventArgs> Serving;
		public static void OnServing(Post post, ServingEventArgs arg)
		{
			if (Serving != null)
			{
				Serving(post, arg);
			}
		}

		public void OnServing(ServingEventArgs eventArgs)
		{
			if (Serving != null)
			{
				Serving(this, eventArgs);
			}
		}

		#endregion
    }
}
