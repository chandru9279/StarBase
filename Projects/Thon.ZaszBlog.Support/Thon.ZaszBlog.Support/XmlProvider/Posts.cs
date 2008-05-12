using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System.Collections.Specialized;
using Thon.ZaszBlog.Support.CodedRepresentations;
using Thon.ZaszBlog.Support.DataServicesAndConfiguration;

namespace Thon.ZaszBlog.Support.XmlProvider
{
	public partial class XmlStorageProvider : StorageProvider
	{
		public override Post SelectPost(Guid id)
		{
			string fileName = _Folder + "Posts" + Path.DirectorySeparatorChar + id.ToString() + ".xml";
			Post post = new Post();
			XmlDocument doc = new XmlDocument();
			doc.Load(fileName);

			post.Title = doc.SelectSingleNode("post/title").InnerText;
			post.Description = doc.SelectSingleNode("post/description").InnerText;
			post.Content = doc.SelectSingleNode("post/content").InnerText;
			post.DateCreated = DateTime.Parse(doc.SelectSingleNode("post/pubDate").InnerText);

			if (doc.SelectSingleNode("post/lastModified") != null)
				post.DateModified = DateTime.Parse(doc.SelectSingleNode("post/lastModified").InnerText);

			if (doc.SelectSingleNode("post/author") != null)
				post.Author = doc.SelectSingleNode("post/author").InnerText;

			if (doc.SelectSingleNode("post/ispublished") != null)
				post.IsPublished = bool.Parse(doc.SelectSingleNode("post/ispublished").InnerText);

			if (doc.SelectSingleNode("post/iscommentsenabled") != null)
				post.IsCommentsEnabled = bool.Parse(doc.SelectSingleNode("post/iscommentsenabled").InnerText);

			if (doc.SelectSingleNode("post/raters") != null)
				post.Raters = int.Parse(doc.SelectSingleNode("post/raters").InnerText);

			if (doc.SelectSingleNode("post/rating") != null)
				post.Rating = float.Parse(doc.SelectSingleNode("post/rating").InnerText, System.Globalization.CultureInfo.GetCultureInfo("en-gb"));

			if (doc.SelectSingleNode("post/slug") != null)
				post.Slug = doc.SelectSingleNode("post/slug").InnerText;

			// Tags
			foreach (XmlNode node in doc.SelectNodes("post/tags/tag"))
			{
				if (!string.IsNullOrEmpty(node.InnerText))
					post.Tags.Add(node.InnerText);
			}

			// Comments
			foreach (XmlNode node in doc.SelectNodes("post/comments/comment"))
			{
				Comment comment = new Comment();
				comment.Id = new Guid(node.Attributes["id"].InnerText);
				comment.Author = node.SelectSingleNode("author").InnerText;
				comment.Email = node.SelectSingleNode("email").InnerText;
				comment.Parent = post;

				if (node.SelectSingleNode("ip") != null)
					comment.IP = node.SelectSingleNode("ip").InnerText;

				if (node.SelectSingleNode("website") != null)
				{
					Uri website;
					if (Uri.TryCreate(node.SelectSingleNode("website").InnerText, UriKind.Absolute, out website))
						comment.Website = website;
				}

				if (node.Attributes["approved"] != null)
					comment.IsApproved = bool.Parse(node.Attributes["approved"].InnerText);
				else
					comment.IsApproved = true;

				comment.Content = node.SelectSingleNode("content").InnerText;
				comment.DateCreated = DateTime.Parse(node.SelectSingleNode("date").InnerText);
				post.Comments.Add(comment);
			}

			post.Comments.Sort();

			// categories
			foreach (XmlNode node in doc.SelectNodes("post/categories/category"))
			{
				Guid key = new Guid(node.InnerText);
				Category cat = Category.GetCategory(key);
				if (cat != null)//CategoryDictionary.Instance.ContainsKey(key))
					post.Categories.Add(cat);
			}

			// Notification e-mails
			foreach (XmlNode node in doc.SelectNodes("post/notifications/email"))
			{
				post.NotificationEmails.Add(node.InnerText);
			}

			return post;
		}
		
		public override void InsertPost(Post post)
		{
			if (!Directory.Exists(_Folder + "Posts"))
				Directory.CreateDirectory(_Folder + "Posts");

			string fileName = _Folder + "Posts" + Path.DirectorySeparatorChar + post.Id.ToString() + ".xml";
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
            using (XmlWriter writer = XmlWriter.Create(fileName, settings))
			{
				writer.WriteStartDocument(true);
				writer.WriteStartElement("post");

				writer.WriteElementString("author", post.Author);
				writer.WriteElementString("title", post.Title);
				writer.WriteElementString("description", post.Description);
				writer.WriteElementString("content", post.Content);
				writer.WriteElementString("ispublished", post.IsPublished.ToString());
				writer.WriteElementString("iscommentsenabled", post.IsCommentsEnabled.ToString());
				writer.WriteElementString("pubDate", post.DateCreated.ToString("yyyy-MM-dd HH:mm:ss"));
				writer.WriteElementString("lastModified", post.DateModified.ToString("yyyy-MM-dd HH:mm:ss"));
				writer.WriteElementString("raters", post.Raters.ToString());
				writer.WriteElementString("rating", post.Rating.ToString());
				writer.WriteElementString("slug", post.Slug);

				// Tags
				writer.WriteStartElement("tags");
				foreach (string tag in post.Tags)
				{
					writer.WriteElementString("tag", tag);
				}
				writer.WriteEndElement();

				// Comments
				writer.WriteStartElement("comments");
				foreach (Comment comment in post.Comments)
				{
					writer.WriteStartElement("comment");
					writer.WriteAttributeString("id", comment.Id.ToString());
					writer.WriteAttributeString("approved", comment.IsApproved.ToString());
					writer.WriteElementString("date", comment.DateCreated.ToString("yyyy-MM-dd HH:mm:ss"));
					writer.WriteElementString("author", comment.Author);
					writer.WriteElementString("email", comment.Email);					
					writer.WriteElementString("ip", comment.IP);
					if (comment.Website != null)
						writer.WriteElementString("website", comment.Website.ToString());
					writer.WriteElementString("content", comment.Content);
					writer.WriteEndElement();
				}
				writer.WriteEndElement();

				// Categories
				writer.WriteStartElement("categories");
				foreach (Category cat in post.Categories)
				{
					writer.WriteElementString("category", cat.Id.ToString());
				}
				writer.WriteEndElement();

				// Notification e-mails
				writer.WriteStartElement("notifications");
				foreach (string email in post.NotificationEmails)
				{
					writer.WriteElementString("email", email);
				}
				writer.WriteEndElement();

				writer.WriteEndElement();
			}
		}
		
		public override void UpdatePost(Post post)
		{
			InsertPost(post);
		}
        		
		public override void DeletePost(Post post)
		{
			string fileName = _Folder + "Posts" + Path.DirectorySeparatorChar + post.Id.ToString() + ".xml";
			if (File.Exists(fileName))
				File.Delete(fileName);
		}

		public override List<Post> FillPosts()
		{
			string folder = _Folder + "Posts" + Path.DirectorySeparatorChar;
			List<Post> posts = new List<Post>();

			foreach (string file in Directory.GetFiles(folder, "*.xml", SearchOption.TopDirectoryOnly))
			{
				FileInfo info = new FileInfo(file);
				string id = info.Name.Replace(".xml", string.Empty);				
				Post post = Post.Load(new Guid(id));
				posts.Add(post);
			}

			posts.Sort();
			return posts;
		}

	}
}