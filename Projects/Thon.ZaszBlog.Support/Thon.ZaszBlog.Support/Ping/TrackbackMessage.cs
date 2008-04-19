using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Thon.ZaszBlog.Support.CodedRepresentations;

namespace Thon.ZaszBlog.Support.Ping
{
    /// <summary>
    /// Represents a trackback message. Given an IShowed item, it fills up its properties
    /// Title, Post, Url, Excerpt, BlogName, and sends it to the url in UrlToNotifyTrackback
    /// </summary>
    public class TrackbackMessage
    {
        private string _Title;
        /// <summary>
        /// Title of the IShowed item
        /// </summary>
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        private Uri _PostUrl;
        /// <summary>
        /// The absolute link to the IShowed item causing the trackback.
        /// </summary>
        public Uri PostUrl
        {
            get { return _PostUrl; }
            set { _PostUrl = value; }
        }

        private string _Excerpt;
        /// <summary>
        /// An excerpt of the IShowed causing the trackback.
        /// </summary>
        public string Excerpt
        {
            get { return _Excerpt; }
            set { _Excerpt = value; }
        }

        private string _BlogName;
        /// <summary>
        /// The name of the blog
        /// </summary>
        public string BlogName
        {
            get { return _BlogName; }
            set { _BlogName = value; }
        }

        private Uri _UrlToNotifyTrackback;
        /// <summary>
        /// The url to notify trackback
        /// </summary>
        public Uri UrlToNotifyTrackback
        {
            get { return _UrlToNotifyTrackback; }
            set { _UrlToNotifyTrackback = value; }
        }

        /// <summary>
        /// The constructor that initializes all the properties of this trackback
        /// message.
        /// </summary>
        /// <param name="item">The IShowed item causing the trackback.</param>
        /// <param name="urlToNotifyTrackback">The url to which the trackback message is sent.</param>
        public TrackbackMessage(IShowed item, Uri urlToNotifyTrackback)
        {
            if (item == null)
                throw new ArgumentNullException("post");

            Title = item.Title;
            PostUrl = item.AbsoluteLink;
            Excerpt = item.Title;
            BlogName = BlogSettings.Instance.Name;
            UrlToNotifyTrackback = urlToNotifyTrackback;
        }

        /// <summary>
        /// If the QueryString associated with the url to which the track back is sent
        /// is empty, then this starts with a ? else with & to concatenate with the
        /// already present QueryString
        /// </summary>
        /// <returns>A QueryString that is sent to the Host referred in our blog</returns>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(UrlToNotifyTrackback.Query))
            {
                return String.Format("?title={0}&url={1}&excerpt={2}&blog_name={3}", Title, PostUrl, Excerpt, BlogName);
            }
            else
            {
                return String.Format("&title={0}&url={1}&excerpt={2}&blog_name={3}", Title, PostUrl, Excerpt, BlogName);
            }
        }

    }
}
