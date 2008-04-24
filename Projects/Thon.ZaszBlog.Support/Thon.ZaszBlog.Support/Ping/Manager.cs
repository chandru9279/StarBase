using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using Thon.ZaszBlog.Support.CodedRepresentations;

namespace Thon.ZaszBlog.Support.Ping
{
  /// <summary>
  /// Manages to send out trackbacks and then pingbacks if trackbacks aren't supported by the linked site.
  /// </summary>
  public static class Manager
  {
    /// <summary>
    /// Sends the trackback or pingback message.
	/// <remarks>
	/// It will try to send a trackback message first, and if the refered web page
	/// doesn't support trackbacks, a pingback is sent.
	/// </remarks>
    /// <para>
    /// Gets all anchors from the content of the IShowed item and ignores links to ourselves.
    /// From all the rest of the links, we resolve them and check for trackback links,
    /// if present we send trackback, else we do a pingback.
    /// </para>
    /// </summary>
    public static void Send(IShowed item)
    {
      foreach (Uri url in GetUrlsFromContent(item.Content))
      {
				if (url.Host == SupportUtilities.AbsoluteWebRoot.Host)
					continue;

                string pageContent = ReadFromWeb(url);
				Uri trackbackUrl = GetTrackBackUrlFromPage(pageContent);
				bool isTrackbackSent = false;

				if (trackbackUrl != null)
				{
					TrackbackMessage message = new TrackbackMessage(item, trackbackUrl);
					isTrackbackSent = Trackback.Send(message);					
				}

				if (!isTrackbackSent)
				{
					Pingback.Send(item.AbsoluteLink, url);
				}
      }
    }

    #region "RegEx Methods"

		/// <summary>
		/// Regex used to find all hyperlinks.
		/// </summary>
    private static readonly Regex urlsRegex = new Regex(@"\<a\s+href=""(http://.*?)"".*\>.+\<\/a\>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		/// <summary>
		/// Regex used to find the trackback link on a remote web page.
		/// </summary>
    private static readonly Regex trackbackLinkRegex = new Regex("trackback:ping=\"([^\"]+)\"", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		/// <summary>
		/// Gets all the URLs from the specified string.
		/// </summary>
    private static List<Uri> GetUrlsFromContent(string content)
    {
      List<Uri> urlsList = new List<Uri>();
      foreach (Match myMatch in urlsRegex.Matches(content))
      {
        string url = myMatch.Groups[1].ToString().Trim();
        Uri uri;
        if (Uri.TryCreate(url, UriKind.Absolute, out uri))
          urlsList.Add(uri);
      }

      return urlsList;
    }

		/// <summary>
		/// Examines the web page source code to retrieve the trackback link from the RDF.
		/// </summary>
    private static Uri GetTrackBackUrlFromPage(string input)
    {
      string url = trackbackLinkRegex.Match(input).Groups[1].ToString().Trim();
      Uri uri;

      if (Uri.TryCreate(url, UriKind.Absolute, out uri))
        return uri;
      else
        return null;
    }

    #endregion

	/// <summary>
    /// Returns the HTML code of a given URL, Downloads pages from the internet.
    /// </summary>
    /// <param name="sourceUrl">The URL you want to extract the html code.</param>
    /// <returns></returns>
    private static string ReadFromWeb(Uri sourceUrl)
    {
      string html;
      using (WebClient client = new WebClient())
      {
          client.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)");
          html = client.DownloadString(sourceUrl);
      }
      return html;
    }
  }
}