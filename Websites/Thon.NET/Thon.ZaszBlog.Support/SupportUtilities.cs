using System;
using System.Net.Mail;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Web;
using System.Threading;
using Thon.Support;

namespace Thon.ZaszBlog.Support
{	
	// Utilities for the entire solution to use.	
	public static class SupportUtilities
	{
		// Strips all illegal characters from the specified title.		
		public static string RemoveIllegalCharacters(string text)
		{
			if (string.IsNullOrEmpty(text))
				return text;

			text = text.Replace(":", string.Empty);
			text = text.Replace("/", string.Empty);
			text = text.Replace("?", string.Empty);
			text = text.Replace("#", string.Empty);
			text = text.Replace("[", string.Empty);
			text = text.Replace("]", string.Empty);
			text = text.Replace("@", string.Empty);
			text = text.Replace(".", string.Empty);
			text = text.Replace("\"", string.Empty);
			text = text.Replace("&", string.Empty);
			text = text.Replace("'", string.Empty);
			text = text.Replace(" ", "-");

            //Converts " " to %20 and "<" ">" to  %3c %3e
			return HttpUtility.UrlEncode(text).Replace("%", string.Empty);
		}

		public static string RelativeWebRoot
		{
            // will return like "/ThonZNet/" for "~/" and for "~/ZaszBlog/" in xml settings file
            // will be like "/ThonZNet/ZaszBlog/"
            // Server's folder for our application + the virtual path in our web.config
            get { return VirtualPathUtility.ToAbsolute(BlogSettings.Instance.VirtualPath); }
		}

		private static Uri _AbsoluteWebRoot;
		public static Uri AbsoluteWebRoot
		{
			get
			{
				if (_AbsoluteWebRoot == null)
				{
					HttpContext context = HttpContext.Current;
					if (context == null)
						throw new System.Net.WebException("AbsoluteWebRoot : The current HttpContext is null");
                    
                    if(ThonSettings.Instance.IsHosted)
                        _AbsoluteWebRoot = new Uri(context.Request.Url.Scheme + "://www.chandruon.net" + RelativeWebRoot);
                    else
                        _AbsoluteWebRoot = new Uri(context.Request.Url.Scheme + "://" + context.Request.Url.Authority + RelativeWebRoot);
                        
				}
				return _AbsoluteWebRoot;
                // will return like "http://www.thon.net/ThonZNet/ZaszBlog/" (or)
                // "http://163.160.2.73:8080/ThonZNet/ZaszBlog/"  (or)
                // "http://localhost:49174/ThonZNet/ZaszBlog/"
			}
		}

        private static Uri _InternetAppRoot;
        public static Uri InternetAppRoot
        {
            get
            {
                if (_InternetAppRoot == null)
                {
                    HttpContext context = HttpContext.Current;
                    if (context == null)
                        throw new System.Net.WebException("The current HttpContext is null");

                    _InternetAppRoot = new Uri(context.Request.Url.Scheme + "://" + context.Request.Url.Authority + VirtualPathUtility.ToAbsolute("~/"));
                }
                return _InternetAppRoot;
                // will return like "http://www.thon.net/ThonZNet/" (or)
                // "http://163.160.2.73:8080/ThonZNet/"  (or)
                // "http://localhost:49174/ThonZNet/"
            }
        }

        public static string FeedUrl
        {
            //example -> http://localhost:49174/ThonZNet/ZaszBlog/ZaszBlogHttpHandlers/Syndication.ashx
            get
            {
                if (!string.IsNullOrEmpty(BlogSettings.Instance.AlternateFeedUrl))
                    return BlogSettings.Instance.AlternateFeedUrl;
                else
                    return AbsoluteWebRoot.ToString() + "ZaszBlogHttpHandlers/Syndication.ashx";
            }
        }

		public static Uri ConvertToAbsolute(Uri relativeUri)
		{
			return ConvertToAbsolute(relativeUri.ToString()); ;
		}

		public static Uri ConvertToAbsolute(string relativeUri)
		{
			if (String.IsNullOrEmpty(relativeUri))
				throw new ArgumentNullException("relativeUri");

			string absolute = AbsoluteWebRoot.ToString();
			int index = absolute.LastIndexOf(RelativeWebRoot.ToString());
            // absolute.Substring(0, index) = "http://www.chandruon.net/"
            // if relativeUri = "/ThonZNet/ZaszBlog/post/2009/08/<post_title>.aspx" then this returns
            // http://163.160.2.73:8080/ThonZNet/ZaszBlog/post/2009/08/<post_title>.aspx
			return new Uri(absolute.Substring(0, index) + relativeUri);
		}

		public static void SendMailMessage(MailMessage message)
		{
           Thon.Support.HelperUtilities.SendMailMessage(message);
		}

		public static void SendMailMessageAsync(MailMessage message)
		{
            Thon.Support.HelperUtilities.SendMailMessageAsync(message);
		}
    }
}
