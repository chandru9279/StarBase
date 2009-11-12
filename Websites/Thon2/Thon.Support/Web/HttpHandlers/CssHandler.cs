using System;
using System.Net;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;
using System.IO.Compression;
using System.Web.Caching;
using System.Collections.Generic;

namespace Thon.Support.Web.HttpHandlers
{
    ///<summary>
	/// Removes whitespace in all stylesheets added to the header of the HTML document. 
    /// In .master files, we usually add this link in the header. 
    /// </summary>
	public class CssHandler : IHttpHandler
	{
		public void ProcessRequest(HttpContext context)
		{
            string name = context.Request.QueryString["name"];
            string file = context.Server.MapPath(context.Request.Path.Replace("Css.ashx", name));
			ReduceCss(file, context);
			SetHeaders(file, context);

			if (ThonSettings.Instance.EnableHttpCompression)
				Compress(context);
		}

		// Removes all unwanted text from the CSS file, including comments and whitespace, & caches it.
		private static void ReduceCss(string file, HttpContext context)
		{
			if (!file.EndsWith(".css", StringComparison.OrdinalIgnoreCase))
			{
				throw new System.Security.SecurityException("No access");
			}

			if (context.Cache[file + "date"] == null)
			{
				using (StreamReader reader = new StreamReader(file))
				{
                    //file will be like "StyleSheets/MasterPageStyleSheet.css"                    
					string body = StripWhitespace(reader);                    
					context.Cache.Insert(file, body, new CacheDependency(file));
					context.Cache.Insert(file + "date", File.GetLastWriteTime(file), new CacheDependency(file));
				}
			}

			context.Response.Write((string)context.Cache[file]);
		}

        private static string ChangeUrls(string input)
        {
            // Regex match
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex(@"url\(([^\)]*)\)", options);

            // Check for match
            bool isMatch = regex.IsMatch(input);
            if (isMatch)
            {
                // Get matches
                MatchCollection matches = regex.Matches(input);
                List<int> l = new List<int>();
                for (int i = 0; i != matches.Count; ++i)
                {
                    int semicolon = matches[i].Index + matches[i].Value.Length;
                    int j;
                    for (j = semicolon; input[j] != '/'; j--) ;
                    l.Add(j);
                }
                // "CssImage.ashx?image=".Length = 20
                for (int i = 0; i < l.Count; i++)
                    l[i] += i * 20;
                for (int i = 0; i < l.Count; i++)
                    input = input.Insert(l[i]+1, "CssImage.ashx?image=");
            }
            return input;
        }

		// Strips the whitespace from any .css file.
		private static string StripWhitespace(StreamReader reader)
		{
			string body = reader.ReadToEnd();
            body = ChangeUrls(body);
			body = body.Replace("  ", String.Empty);
			body = body.Replace(Environment.NewLine, String.Empty);
			body = body.Replace("\t", string.Empty);
			body = body.Replace(" {", "{");
			body = body.Replace(" :", ":");
			body = body.Replace(": ", ":");
			body = body.Replace(", ", ",");
			body = body.Replace("; ", ";");
			body = body.Replace(";}", "}");
			body = Regex.Replace(body, @"(?<=[>])\s{2,}(?=[<])|(?<=[>])\s{2,}(?=&nbsp;)|(?<=&ndsp;)\s{2,}(?=[<])", String.Empty);
			return body;
		}

		// This will make the browser and server keep the output in its cache and thereby improve performance.
		private static void SetHeaders(string file, HttpContext context)
		{
			context.Response.ContentType = "text/css";
			context.Response.Cache.VaryByHeaders["Accept-Encoding"] = true;

			DateTime date = DateTime.Now;
			if (context.Cache[file + "date"] != null)
				date = (DateTime)context.Cache[file + "date"];

			string etag = "\"" + date.GetHashCode() + "\"";
			string incomingEtag = context.Request.Headers["If-None-Match"];

			context.Response.Cache.SetExpires(DateTime.Now.ToUniversalTime().AddDays(7));
			context.Response.Cache.SetCacheability(HttpCacheability.Public);
			context.Response.Cache.SetMaxAge(new TimeSpan(7, 0, 0, 0));
			context.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
			context.Response.Cache.SetETag(etag);

			if (String.Compare(incomingEtag, etag) == 0)
			{
				context.Response.StatusCode = (int)HttpStatusCode.NotModified;
				context.Response.End();
			}
		}

		#region Compression

		private const string GZIP = "gzip";
		private const string DEFLATE = "deflate";

		private static void Compress(HttpContext context)
		{
			if (context.Request.UserAgent != null && context.Request.UserAgent.Contains("MSIE 6"))
				return;
            //If browser is MS Internet Explorer6 then no compression of any kind.

			if (IsEncodingAccepted(GZIP))
			{
				context.Response.Filter = new GZipStream(context.Response.Filter, CompressionMode.Compress);
				SetEncoding(GZIP);
			}
			else if (IsEncodingAccepted(DEFLATE))
			{
				context.Response.Filter = new DeflateStream(context.Response.Filter, CompressionMode.Compress);
				SetEncoding(DEFLATE);
			}
		}

		// Checks the request headers to see if the specified encoding is accepted by the client.
		private static bool IsEncodingAccepted(string encoding)
		{
			return HttpContext.Current.Request.Headers["Accept-encoding"] != null && HttpContext.Current.Request.Headers["Accept-encoding"].Contains(encoding);
		}

		// Adds the specified encoding to the response headers.
		private static void SetEncoding(string encoding)
		{
			HttpContext.Current.Response.AppendHeader("Content-encoding", encoding);
		}

		#endregion

		public bool IsReusable
		{
			get { return false; }
		}
    }
}