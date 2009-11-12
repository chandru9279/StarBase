using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Thon.ZaszBlog.Support.CodedRepresentations;
using Thon.Support;

namespace Thon.ZaszBlog.Support
{
    public class SyndicationGenerator
	{
		private const string GENERATOR_NAME = "ZaszBlog Syndication Generator";
		private static readonly Uri GENERATOR_URI = new Uri("http://thon.net/");
		private static readonly Version GENERATOR_VERSION = new Version("1.0.0.0");
		
        /// <summary>
        /// Constructor that takes in and stores the settings and a list of Categories
        /// </summary>
        /// <param name="settings">The <see cref="Thon.ZaszBlog.Support.BlogSettings"/> 
        /// singleton instance that contains all the settings . </param>
        /// <param name="categories"> A List of all Categories present in the blog</param>
        /// <remarks>
        /// Copies the list of categories to an array and to a list
        /// </remarks>
        public SyndicationGenerator(BlogSettings settings, List<Category> categories)
		{            
			if (settings == null)
				throw new ArgumentNullException("settings");
			if (categories == null)
				throw new ArgumentNullException("categories");		
			this.Settings = settings;
			if (categories.Count > 0)
			{
				Category[] values = new Category[categories.Count];
				categories.CopyTo(values);
                foreach (Category category in values)
					this.Categories.Add(category);
			}
        }

        #region Categories
        private List<Category> blogCategories;
		public List<Category> Categories
		{
			get
			{
				if (blogCategories == null)
					blogCategories = new List<Category>();
				return blogCategories;
			}
        }
        #endregion

        #region SupportedNamespaces
        private static Dictionary<string, string> xmlNamespaces;
        public static Dictionary<string, string> SupportedNamespaces
		{
			get
			{
				if (xmlNamespaces == null)
				{
					xmlNamespaces = new Dictionary<string, string>();
					xmlNamespaces.Add("blogChannel", "http://backend.userland.com/blogChannelModule");
					xmlNamespaces.Add("pingback", "http://madskills.com/public/xml/rss/module/pingback/");
					xmlNamespaces.Add("trackback", "http://madskills.com/public/xml/rss/module/trackback/");
					xmlNamespaces.Add("wfw", "http://wellformedweb.org/CommentAPI/");
					xmlNamespaces.Add("slash", "http://purl.org/rss/1.0/modules/slash/");
				}

				return xmlNamespaces;
			}
        }
        #endregion

        #region Settings
        private BlogSettings blogSettings;
		public BlogSettings Settings
		{
			get
			{
				return blogSettings;
			}

			protected set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				else
				{
					blogSettings = value;
				}
			}
        }
        #endregion

        //============================================================
		//	STATIC UTILITY METHODS
        //============================================================

        #region FormatW3cOffset
        /// <summary>
		/// Converts the value of the specified TimeSpan to its equivalent string representation.
        /// </summary>
		/// <returns>A string representation of the TimeSpan.</returns>
		private static string FormatW3cOffset(TimeSpan offset, string separator)
		{            
			string formattedOffset = String.Empty;
			if (offset >= TimeSpan.Zero)
			{
				formattedOffset = "+";
			}

			return String.Concat(formattedOffset, offset.Hours.ToString("00"), separator, offset.Minutes.ToString("00"));
            // example : "+02:23"
        }
        #endregion

        private static Uri GetPermaLink(IShowed publishable)
		{
			Post post = publishable as Post;
			if (post != null)
			{
				return post.PermaLink;
			}
			return SupportUtilities.ConvertToAbsolute(publishable.RelativeLink);
		}

		#region ToRfc822DateTime(DateTime dateTime)
		/// <summary>
		/// Converts the supplied <see cref="DateTime"/> to its equivalent <a href="http://asg.web.cmu.edu/rfc/rfc822.html">RFC-822 DateTime</a> string representation.
		/// </summary>
		/// <param name="dateTime">The <see cref="DateTime"/> to convert.</param>
		/// <returns>The equivalent <a href="http://asg.web.cmu.edu/rfc/rfc822.html">RFC-822 DateTime</a> string representation.</returns>
		public static string ToRfc822DateTime(DateTime dateTime)
		{
			int offset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours;
			string timeZone = "+" + offset.ToString().PadLeft(2, '0');

			//------------------------------------------------------------
			//	Adjust time zone based on offset
			//------------------------------------------------------------
			if (offset < 0)
			{
				int i = offset * -1;
				timeZone = "-" + i.ToString().PadLeft(2, '0');

			}

			return dateTime.ToString("ddd, dd MMM yyyy HH:mm:ss " + timeZone.PadRight(5, '0'));
		}
		#endregion

		#region ToW3CDateTime(DateTime utcDateTime)
		/// <summary>
		/// Converts the supplied <see cref="DateTime"/> to its equivalent <a href="http://www.w3.org/TR/NOTE-datetime">W3C DateTime</a> string representation.
		/// </summary>
		/// <param name="utcDateTime">The Coordinated Universal Time (UTC) <see cref="DateTime"/> to convert.</param>
		/// <returns>The equivalent <a href="http://www.w3.org/TR/NOTE-datetime">W3C DateTime</a> string representation.</returns>
		private static string ToW3CDateTime(DateTime utcDateTime)
		{
			TimeSpan utcOffset = TimeSpan.Zero;
			return (utcDateTime + utcOffset).ToString("yyyy-MM-ddTHH:mm:ss") + SyndicationGenerator.FormatW3cOffset(utcOffset, ":");
		}
		#endregion

		#region ConvertPathsToAbsolute(string content)
		/// <summary>
		/// Converts all relative paths in the spcified content to absolute.
        /// Paths occur in the posts like :
        /// &lt;a rel="enclosure" href="/ThonZNet/ThonHttpHandlers/File.ashx?file=chandru.txt"&gt;chandru.txt (45.11 kb)&lt;/a&gt;
        /// and href="/ .....
        /// which in html looks like an ordinary anchor tag
        /// These are converted into :
        /// &lt;a rel="enclosure" href="http://www.thon.net/ThonZNet/ThonHttpHandlers/File.ashx?file=chandru.txt"&gt;chandru.txt (45.11 kb)&lt;/a&gt;
        /// and href="http://localhost:49174/ThonZNet/ZaszBlog/"
		/// </summary>
		private static string ConvertPathsToAbsolute(string content)
		{
            content = content.Replace("\"" + HelperUtilities.InternetAppRoot.AbsolutePath + "ThonHttpHandlers/Image.ashx", "\"" + HelperUtilities.InternetAppRoot + "ThonHttpHandlers/Image.ashx");
            content = content.Replace("\"" + HelperUtilities.InternetAppRoot.AbsolutePath + "ThonHttpHandlers/File.ashx", "\"" + HelperUtilities.InternetAppRoot + "ThonHttpHandlers/File.ashx");
			content = content.Replace("href=\"/", "href=\"" + SupportUtilities.AbsoluteWebRoot);
			return content;
		}
		#endregion

		//============================================================
		//	PUBLIC METHODS
		//============================================================
		#region WriteFeed(SyndicationFormat format, Stream stream, List<IShowed> publishables)
		/// <summary>
		/// Writes a generated syndication feed that conforms to the supplied <see cref="SyndicationFormat"/> using the supplied <see cref="Stream"/> and collection.
		/// </summary>
		/// <param name="format">A <see cref="SyndicationFormat"/> enumeration value indicating the syndication format to generate.</param>
		/// <param name="stream">The <see cref="Stream"/> to which you want to write the syndication feed.</param>
		/// <param name="publishables">The collection of <see cref="IShowed"/> objects used to generate the syndication feed content.</param>
		/// <param name="title">The title of the RSS channel</param>
		public void WriteFeed(SyndicationFormat format, Stream stream, List<IShowed> publishables, string title)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (publishables == null)
			{
				throw new ArgumentNullException("publishables");
			}
			if (!stream.CanWrite)
			{
				throw new ArgumentException(String.Format(null, "Unable to generate {0} syndication feed. The provided stream does not support writing.", format), "stream");
			}

			//------------------------------------------------------------
			//	Write syndication feed based on specified format
			//------------------------------------------------------------
			switch (format)
			{
				case SyndicationFormat.Atom:
					this.WriteAtomFeed(stream, publishables, title);
					break;

				case SyndicationFormat.Rss:
					this.WriteRssFeed(stream, publishables, title);
					break;
			}
		}
		#endregion

		//============================================================
		//	SYNDICATION WRITE METHODS
		//============================================================
		#region WriteAtomFeed(Stream stream, List<IShowed> publishables)
		/// <summary>
		/// Writes a generated Atom syndication feed to the specified <see cref="Stream"/> using the supplied collection.
		/// </summary>
		/// <param name="stream">The <see cref="Stream"/> to which you want to write the syndication feed.</param>
		/// <param name="publishables">The collection of <see cref="IShowed"/> objects used to generate the syndication feed content.</param>
		/// <param name="title">The title of the ATOM content.</param>
		private void WriteAtomFeed(Stream stream, List<IShowed> publishables, string title)
		{
			XmlWriterSettings writerSettings = new XmlWriterSettings();
			writerSettings.Encoding = System.Text.Encoding.UTF8;
			writerSettings.Indent = true;

			//------------------------------------------------------------
			//	Create writer against stream using defined settings
			//------------------------------------------------------------
			using (XmlWriter writer = XmlWriter.Create(stream, writerSettings))
			{
				writer.WriteStartElement("feed", "http://www.w3.org/2005/Atom");
				writer.WriteAttributeString("version", "1.0");

				//------------------------------------------------------------
				//	Write XML namespaces used to support syndication extensions
				//------------------------------------------------------------
				foreach (string prefix in SyndicationGenerator.SupportedNamespaces.Keys)
				{
					writer.WriteAttributeString("xmlns", prefix, null, SyndicationGenerator.SupportedNamespaces[prefix]);
				}

				//------------------------------------------------------------
				//	Write feed content
				//------------------------------------------------------------
				this.WriteAtomContent(writer, publishables, title);

				writer.WriteFullEndElement();
			}
		}
		#endregion

		#region WriteRssFeed(Stream stream, List<IShowed> publishables)
		/// <summary>
		/// Writes a generated RSS syndication feed to the specified <see cref="Stream"/> using the supplied collection.
		/// </summary>
		/// <param name="stream">The <see cref="Stream"/> to which you want to write the syndication feed.</param>
		/// <param name="publishables">The collection of <see cref="IShowed"/> objects used to generate the syndication feed content.</param>
		/// <param name="title">The title of the RSS channel.</param>
		private void WriteRssFeed(Stream stream, List<IShowed> publishables, string title)
		{
			XmlWriterSettings writerSettings = new XmlWriterSettings();
			writerSettings.Encoding = System.Text.Encoding.UTF8;
			writerSettings.Indent = true;

			//------------------------------------------------------------
			//	Create writer against stream using defined settings
			//------------------------------------------------------------
			using (XmlWriter writer = XmlWriter.Create(stream, writerSettings))
			{
				writer.WriteStartElement("rss");
				writer.WriteAttributeString("version", "2.0");

				//------------------------------------------------------------
				//	Write XML namespaces used to support syndication extensions
				//------------------------------------------------------------
				foreach (string prefix in SyndicationGenerator.SupportedNamespaces.Keys)
				{
					writer.WriteAttributeString("xmlns", prefix, null, SyndicationGenerator.SupportedNamespaces[prefix]);
				}

				//------------------------------------------------------------
				//	Write <channel> element
				//------------------------------------------------------------
				this.WriteRssChannel(writer, publishables, title);

				writer.WriteFullEndElement();
			}
		}
		#endregion

		//============================================================
		//	PRIVATE RSS METHODS
		//============================================================
		#region WriteRssChannel(XmlWriter writer, List<IShowed> publishables)
		/// <summary>
		/// Writes the RSS channel element information to the specified <see cref="XmlWriter"/> using the supplied collection.
		/// </summary>
		/// <param name="writer">The <see cref="XmlWriter"/> to write channel element information to.</param>
		/// <param name="publishables">The collection of <see cref="IShowed"/> objects used to generate syndication content.</param>
		/// <param name="title">The title of the RSS channel.</param>
		private void WriteRssChannel(XmlWriter writer, List<IShowed> publishables, string title)
		{
			writer.WriteStartElement("channel");

			writer.WriteElementString("title", title);
			writer.WriteElementString("description", this.Settings.Description);
			writer.WriteElementString("link", SupportUtilities.AbsoluteWebRoot.ToString());

			//------------------------------------------------------------
			//	Write common/shared channel elements
			//------------------------------------------------------------
			this.WriteRssChannelCommonElements(writer);

			foreach (IShowed publishable in publishables)
			{
				//------------------------------------------------------------
				//	Skip publishable content if it is not visible
				//------------------------------------------------------------
				if (publishable.IsVisible)
				{
					WriteRssItem(writer, publishable);
				}
			}

			//------------------------------------------------------------
			//	Write </channel> element
			//------------------------------------------------------------
			writer.WriteEndElement();
		}
		#endregion

		#region WriteRssChannelCommonElements(XmlWriter writer)
		/// <summary>
		/// Writes the common/shared RSS channel element information to the specified <see cref="XmlWriter"/>.
		/// </summary>
		/// <param name="writer">The <see cref="XmlWriter"/> to write channel element information to.</param>
		private void WriteRssChannelCommonElements(XmlWriter writer)
		{
            //------------------------------------------------------------
            //	Write optional channel elements
            //------------------------------------------------------------
            writer.WriteElementString("docs", "http://www.rssboard.org/rss-specification");
            writer.WriteElementString("generator", String.Format(null, "{0} {1} ({2})", GENERATOR_NAME, GENERATOR_VERSION, GENERATOR_URI));

            //------------------------------------------------------------
            //	Write blogChannel syndication extension element
            //------------------------------------------------------------
            Uri blogRoll;
            if (Uri.TryCreate(String.Concat(SupportUtilities.AbsoluteWebRoot.ToString().TrimEnd('/'), "/ZaszBlogHttpHandlers/Opml.ashx"), UriKind.RelativeOrAbsolute, out blogRoll))
            {
                writer.WriteElementString("blogChannel", "blogRoll", "http://backend.userland.com/blogChannelModule", blogRoll.ToString());
            }

            //------------------------------------------------------------
            //	Write Dublin Core syndication extension elements
            //------------------------------------------------------------
            if (!String.IsNullOrEmpty(Thon.Support.ThonSettings.Instance.MyName))
            {
                writer.WriteElementString("dc", "creator", "http://purl.org/dc/elements/1.1/", Thon.Support.ThonSettings.Instance.MyName);
            }
            if (!String.IsNullOrEmpty(this.Settings.Name))
            {
                writer.WriteElementString("dc", "title", "http://purl.org/dc/elements/1.1/", this.Settings.Name);
            }
            //------------------------------------------------------------------------------------------------------------------------
            // To Chandru(Myself) : Look around in the web for any syndication extension and include 'em with their namespaces here.
            //------------------------------------------------------------------------------------------------------------------------
		}
		#endregion

		#region WriteRssItem(XmlWriter writer, IShowed publishable)
		/// <summary>
		/// Writes the RSS channel item element information to the specified <see cref="XmlWriter"/> using the supplied <see cref="Page"/>.
		/// </summary>
		/// <param name="writer">The <see cref="XmlWriter"/> to write channel item element information to.</param>
		/// <param name="publishable">The <see cref="IShowed"/> used to generate channel item content.</param>
		private static void WriteRssItem(XmlWriter writer, IShowed publishable)
		{
				//------------------------------------------------------------
				//	Cast IShowed as Post to support comments/trackback
				//------------------------------------------------------------
				Post post = publishable as Post;
				Comment comment = publishable as Comment;

				//------------------------------------------------------------
				//	Raise serving event
				//------------------------------------------------------------                
				ServingEventArgs arg = new ServingEventArgs(publishable.Content, ServingLocation.Feed);
				publishable.OnServing(arg);
				if (arg.Cancel)
				{
					return;
				}

				//------------------------------------------------------------
				//	Modify post content to make references absolute
				//------------------------------------------------------------    
				string content = ConvertPathsToAbsolute(arg.Body);

				writer.WriteStartElement("item");
				//------------------------------------------------------------
				//	Write required channel item elements
				//------------------------------------------------------------
				writer.WriteElementString("title", publishable.Title);
				writer.WriteElementString("description", content);
				writer.WriteElementString("link", SupportUtilities.ConvertToAbsolute(publishable.RelativeLink).ToString());

				//------------------------------------------------------------
				//	Write optional channel item elements
				//------------------------------------------------------------
				writer.WriteElementString("author", publishable.Author);
				if (post != null)
				{
					writer.WriteElementString("comments", String.Concat(SupportUtilities.ConvertToAbsolute(publishable.RelativeLink).ToString(), "#comment"));
				}
				writer.WriteElementString("guid", SyndicationGenerator.GetPermaLink(publishable).ToString());
				writer.WriteElementString("pubDate", SyndicationGenerator.ToRfc822DateTime(publishable.DateCreated));

				//------------------------------------------------------------
				//	Write channel item category elements
				//------------------------------------------------------------
				if (publishable.Categories != null)
				{
					foreach (Category category in publishable.Categories)
					{
						writer.WriteElementString("category", category.Title);
					}
				}

                //------------------------------------------------------------
                //	Write Dublin Core syndication extension elements
                //------------------------------------------------------------
                if (!String.IsNullOrEmpty(publishable.Author))
                {
                    writer.WriteElementString("dc", "publisher", "http://purl.org/dc/elements/1.1/", publishable.Author);
                }

				//------------------------------------------------------------
				//	Write pingback syndication extension elements
				//------------------------------------------------------------
				Uri pingbackServer;
                if (Uri.TryCreate(String.Concat(SupportUtilities.AbsoluteWebRoot.ToString().TrimEnd('/'), "/ZaszBlogHttpHandlers/PingBack.ashx"), UriKind.RelativeOrAbsolute, out pingbackServer))
				{
					writer.WriteElementString("pingback", "server", "http://madskills.com/public/xml/rss/module/pingback/", pingbackServer.ToString());
					writer.WriteElementString("pingback", "target", "http://madskills.com/public/xml/rss/module/pingback/", SyndicationGenerator.GetPermaLink(publishable).ToString());
				}

				//------------------------------------------------------------
				//	Write slash syndication extension elements
				//------------------------------------------------------------
				if (post != null && post.Comments != null)
				{
					writer.WriteElementString("slash", "comments", "http://purl.org/rss/1.0/modules/slash/", post.Comments.Count.ToString());
				}

				//------------------------------------------------------------
				//	Write trackback syndication extension elements
				//------------------------------------------------------------
				if (post != null && post.TrackbackLink != null)
				{
					writer.WriteElementString("trackback", "ping", "http://madskills.com/public/xml/rss/module/trackback/", post.TrackbackLink.ToString());
				}

				//------------------------------------------------------------
				//	Write well-formed web syndication extension elements
				//------------------------------------------------------------
				writer.WriteElementString("wfw", "comment", "http://wellformedweb.org/CommentAPI/", String.Concat(SupportUtilities.ConvertToAbsolute(publishable.RelativeLink).ToString(), "#comment"));
                writer.WriteElementString("wfw", "commentRss", "http://wellformedweb.org/CommentAPI/", SupportUtilities.AbsoluteWebRoot.ToString().TrimEnd('/') + "/ZaszBlogHttpHandlers/Syndication.ashx?post=" + publishable.Id.ToString());

				//------------------------------------------------------------
				//	Write </item> element
				//------------------------------------------------------------
				writer.WriteEndElement();
		}
		#endregion

		//============================================================
		//	PRIVATE ATOM METHODS
		//============================================================
		#region WriteAtomContent(XmlWriter writer, List<IShowed> publishables)
		/// <summary>
		/// Writes the Atom feed element information to the specified <see cref="XmlWriter"/> using the supplied collection.
		/// </summary>
		/// <param name="writer">The <see cref="XmlWriter"/> to write channel element information to.</param>
		/// <param name="publishables">The collection of <see cref="IShowed"/> objects used to generate syndication content.</param>
		/// <param name="title">The title of the ATOM content.</param>
		private void WriteAtomContent(XmlWriter writer, List<IShowed> publishables, string title)
		{
				//------------------------------------------------------------
				//	Write required feed elements
				//------------------------------------------------------------
				writer.WriteElementString("id", SupportUtilities.AbsoluteWebRoot.ToString());
				writer.WriteElementString("title", title);
				writer.WriteElementString("updated", (publishables.Count > 0) ? SyndicationGenerator.ToW3CDateTime(publishables[0].DateModified.ToUniversalTime()) : SyndicationGenerator.ToW3CDateTime(DateTime.UtcNow));

				//------------------------------------------------------------
				//	Write recommended feed elements
				//------------------------------------------------------------
				writer.WriteStartElement("link");
				writer.WriteAttributeString("href", SupportUtilities.AbsoluteWebRoot.ToString());
				writer.WriteEndElement();

				writer.WriteStartElement("link");
				writer.WriteAttributeString("rel", "self");
				writer.WriteAttributeString("href", SupportUtilities.AbsoluteWebRoot.ToString());
				writer.WriteEndElement();

				writer.WriteStartElement("link");
				writer.WriteAttributeString("rel", "alternate");
				writer.WriteAttributeString("href", SupportUtilities.FeedUrl.ToString());
				writer.WriteEndElement();

				//------------------------------------------------------------
				//	Write optional feed elements
				//------------------------------------------------------------
				writer.WriteElementString("subtitle", this.Settings.Description);

				//------------------------------------------------------------
				//	Write common/shared feed elements
				//------------------------------------------------------------
				this.WriteAtomContentCommonElements(writer);

				foreach (IShowed publishable in publishables)
				{
					//------------------------------------------------------------
					//	Skip publishable content if it is not visible
					//------------------------------------------------------------
					if (!publishable.IsVisible)
					{
						continue;
					}

					//------------------------------------------------------------
					//	Write <entry> element for publishable content
					//------------------------------------------------------------
					WriteAtomEntry(writer, publishable);
				}
		}
		#endregion

		#region WriteAtomContentCommonElements(XmlWriter writer)
		/// <summary>
		/// Writes the common/shared Atom feed element information to the specified <see cref="XmlWriter"/>.
		/// </summary>
		/// <param name="writer">The <see cref="XmlWriter"/> to write channel element information to.</param>
	    private void WriteAtomContentCommonElements(XmlWriter writer)
		{
				//------------------------------------------------------------
				//	Write optional feed elements
				//------------------------------------------------------------
				writer.WriteStartElement("author");
				writer.WriteElementString("name", Thon.Support.ThonSettings.Instance.MyName);
				writer.WriteEndElement();

				writer.WriteStartElement("generator");
				writer.WriteAttributeString("uri", GENERATOR_URI.ToString());
				writer.WriteAttributeString("version", GENERATOR_VERSION.ToString());
				writer.WriteString(GENERATOR_NAME);
				writer.WriteEndElement();

				//------------------------------------------------------------
				//	Write blogChannel syndication extension elements
				//------------------------------------------------------------
				Uri blogRoll;
                if (Uri.TryCreate(String.Concat(SupportUtilities.AbsoluteWebRoot.ToString().TrimEnd('/'), "/ZaszBlogHttpHandlers/Opml.ashx"), UriKind.RelativeOrAbsolute, out blogRoll))
				{
					writer.WriteElementString("blogChannel", "blogRoll", "http://backend.userland.com/blogChannelModule", blogRoll.ToString());
				}

				//------------------------------------------------------------
				//	Write Dublin Core syndication extension elements
				//------------------------------------------------------------
				if (!String.IsNullOrEmpty(Thon.Support.ThonSettings.Instance.MyName))
				{
					writer.WriteElementString("dc", "creator", "http://purl.org/dc/elements/1.1/", Thon.Support.ThonSettings.Instance.MyName );
				}
				if (!String.IsNullOrEmpty(this.Settings.Description))
				{
					writer.WriteElementString("dc", "description", "http://purl.org/dc/elements/1.1/", this.Settings.Description);
				}
			    if (!String.IsNullOrEmpty(this.Settings.Name))
				{
					writer.WriteElementString("dc", "title", "http://purl.org/dc/elements/1.1/", this.Settings.Name);
				}
		}
		#endregion

		#region WriteAtomEntry(XmlWriter writer, IShowed publishable)
		/// <summary>
		/// Writes the Atom feed entry element information to the specified <see cref="XmlWriter"/> using the supplied <see cref="Page"/>.
		/// </summary>
		/// <param name="writer">The <see cref="XmlWriter"/> to write feed entry element information to.</param>
		/// <param name="publishable">The <see cref="IShowed"/> used to generate feed entry content.</param>
		private static void WriteAtomEntry(XmlWriter writer, IShowed publishable)
		{
				Post post = publishable as Post;
				Comment comment = publishable as Comment;

				//------------------------------------------------------------
				//	Raise serving event
				//------------------------------------------------------------                
				ServingEventArgs arg = new ServingEventArgs(publishable.Content, ServingLocation.Feed);
				publishable.OnServing(arg);
				if (arg.Cancel)
				{
					return;
				}

				//------------------------------------------------------------
				//	Modify publishable content to make references absolute
				//------------------------------------------------------------
				string content = ConvertPathsToAbsolute(arg.Body);

				writer.WriteStartElement("entry");
				//------------------------------------------------------------
				//	Write required entry elements
				//------------------------------------------------------------
				writer.WriteElementString("id", SupportUtilities.ConvertToAbsolute(publishable.RelativeLink).ToString());
				writer.WriteElementString("title", publishable.Title);
				writer.WriteElementString("updated", SyndicationGenerator.ToW3CDateTime(publishable.DateCreated.ToUniversalTime()));

				//------------------------------------------------------------
				//	Write recommended entry elements
				//------------------------------------------------------------
				writer.WriteStartElement("link");
				writer.WriteAttributeString("rel", "self");
				writer.WriteAttributeString("href", SyndicationGenerator.GetPermaLink(publishable).ToString());
				writer.WriteEndElement();

				writer.WriteStartElement("link");
				writer.WriteAttributeString("href", SupportUtilities.ConvertToAbsolute(publishable.RelativeLink).ToString());
				writer.WriteEndElement();

				writer.WriteStartElement("author");
				writer.WriteElementString("name", publishable.Author);
				writer.WriteEndElement();

				writer.WriteStartElement("summary");
				writer.WriteAttributeString("type", "html");
				writer.WriteString(content);
				writer.WriteEndElement();

				//------------------------------------------------------------
				//	Write optional entry elements
				//------------------------------------------------------------
				writer.WriteElementString("published", SyndicationGenerator.ToW3CDateTime(publishable.DateCreated.ToUniversalTime()));

				writer.WriteStartElement("link");
				writer.WriteAttributeString("rel", "related");
				writer.WriteAttributeString("href", String.Concat(SupportUtilities.ConvertToAbsolute(publishable.RelativeLink).ToString(), "#comment"));
				writer.WriteEndElement();

				//------------------------------------------------------------
				//	Write entry category elements
				//------------------------------------------------------------
				if (publishable.Categories != null)
				{
					foreach (Category category in publishable.Categories)
					{
						writer.WriteStartElement("category");
						writer.WriteAttributeString("term", category.Title);
						writer.WriteEndElement();
					}
				}

		    	//------------------------------------------------------------
				//	Write pingback syndication extension elements
				//------------------------------------------------------------
				Uri pingbackServer;
                if (Uri.TryCreate(String.Concat(SupportUtilities.AbsoluteWebRoot.ToString().TrimEnd('/'), "ZaszBlogHttpHandlers/Pingback.ashx"), UriKind.RelativeOrAbsolute, out pingbackServer))
				{
					writer.WriteElementString("pingback", "server", "http://madskills.com/public/xml/rss/module/pingback/", pingbackServer.ToString());
					writer.WriteElementString("pingback", "target", "http://madskills.com/public/xml/rss/module/pingback/", SyndicationGenerator.GetPermaLink(publishable).ToString());
				}

				//------------------------------------------------------------
				//	Write slash syndication extension elements
				//------------------------------------------------------------
				if (post != null && post.Comments != null)
				{
					writer.WriteElementString("slash", "comments", "http://purl.org/rss/1.0/modules/slash/", post.Comments.Count.ToString());
				}

				//------------------------------------------------------------
				//	Write trackback syndication extension elements
				//------------------------------------------------------------
				if (post != null && post.TrackbackLink != null)
				{
					writer.WriteElementString("trackback", "ping", "http://madskills.com/public/xml/rss/module/trackback/", post.TrackbackLink.ToString());
				}

				//------------------------------------------------------------
				//	Write well-formed web syndication extension elements
				//------------------------------------------------------------
				writer.WriteElementString("wfw", "comment", "http://wellformedweb.org/CommentAPI/", String.Concat(SupportUtilities.ConvertToAbsolute(publishable.RelativeLink).ToString(), "#comment"));
                writer.WriteElementString("wfw", "commentRss", "http://wellformedweb.org/CommentAPI/", SupportUtilities.AbsoluteWebRoot.ToString().TrimEnd('/') + "ZaszBlogHttpHandlers/Syndication.ashx?post=" + publishable.Id.ToString());

				//------------------------------------------------------------
				//	Write </entry> element
				//------------------------------------------------------------
				writer.WriteEndElement();
		}
		#endregion
	}
}
