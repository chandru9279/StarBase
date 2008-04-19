using System;
using System.Collections.Generic;
using System.ComponentModel;
using Thon.ZaszBlog.Support.DataServicesAndConfiguration;

namespace Thon.ZaszBlog.Support.CodedRepresentations
{
	// Represents a comment to a blog post.
	[Serializable]
	public sealed class Comment : IComparable<Comment>, IShowed
    {

        #region Properties;Id;Author;Email;Website;IsVisible;Content;IP;Parent;IsApproved;DateCreated;DateModified;Categories;RelativeLink;AbsoluteLink;Description;Title;

        private Guid _Id;		
		public Guid Id
		{
			get { return _Id; }
			set { _Id = value; }
		}

		private string _Author;
		public string Author
		{
			get { return _Author; }
			set { _Author = value; }
		}

		private string _Email;
		public string Email
		{
			get { return _Email; }
			set { _Email = value; }
		}

		private Uri _Website;
		public Uri Website
		{
			get { return _Website; }
			set { _Website = value; }
		}

		private string _Content;
		public string Content
		{
			get { return _Content; }
			set { _Content = value; }
		}

		private string _IP;
		public string IP
		{
			get { return _IP; }
			set { _IP = value; }
		}

		private IShowed _Post;
		public IShowed Parent
		{
			get { return _Post; }
			set { _Post = value; }
		}

		private bool _IsApproved;
		public bool IsApproved
		{
			get { return _IsApproved; }
			set { _IsApproved = value; }
		}
        public bool IsVisible
		{
			get { return IsApproved; }
		}

		private DateTime _DateCreated = DateTime.MinValue;
		public DateTime DateCreated
		{
			get { return _DateCreated; }
			set { _DateCreated = value; }
		}

		public DateTime DateModified
		{
			get { return DateCreated; }
		}

		public String Title
		{
			get { return Author + " on " + Parent.Title; }
		}

		public string RelativeLink
		{
			get { return Parent.RelativeLink.ToString() + "#id_" + Id; }
		}

		public Uri AbsoluteLink
		{
			get { return new Uri(Parent.AbsoluteLink + "#id_" + Id); }
		}

		public String Description
		{
			get { return string.Empty; }
		}

		public StateList<Category> Categories
		{
			get { return null; }
		}
        
		#endregion

		#region IComparable<Comment> Members

		public int CompareTo(Comment other)
		{
			return this.DateCreated.CompareTo(other.DateCreated);
		}

		#endregion

		#region Events
        		
		public static event EventHandler<ServingEventArgs> Serving;
		public static void OnServing(Comment comment, ServingEventArgs arg)
		{
			if (Serving != null)
			{
				Serving(comment, arg);
			}
		}

		public void OnServing(ServingEventArgs eventArgs)
		{
			if (Serving != null)
			{
				Serving(this, eventArgs);
			}
		}

		public static event EventHandler<CancelEventArgs> Approving;
		internal static void OnApproving(Comment comment, CancelEventArgs e)
		{
			if (Approving != null)
			{
				Approving(comment, e);
			}
		}

		public static event EventHandler<EventArgs> Approved;
		internal static void OnApproved(Comment comment)
		{
			if (Approved != null)
			{
				Approved(comment, EventArgs.Empty);
			}
		}

		
		// Occurs when the page is being attacked by robot spam.
		public static event EventHandler<EventArgs> SpamAttack;		
		public static void OnSpamAttack()
		{
			if (SpamAttack != null)
			{
				SpamAttack(null, new EventArgs());
			}
		}

		#endregion
	}
}
