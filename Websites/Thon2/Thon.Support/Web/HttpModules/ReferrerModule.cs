using System;
using System.Xml;
using System.IO;
using System.Web;
using System.Collections;
using System.Net;
using System.Threading;

namespace Thon.Support.Web.HttpModules
{
  public class ReferrerModule : IHttpModule
  {

    #region IHttpModule Members

    public void Dispose()
    {
    }

    public void Init(HttpApplication context)
    {
      if (ThonSettings.Instance.EnableReferrerTracking)
        context.BeginRequest += new EventHandler(context_BeginRequest);
    }

    #endregion

    private void context_BeginRequest(object sender, EventArgs e)
    {
      HttpContext context = ((HttpApplication)sender).Context;
      if (!context.Request.Path.ToUpperInvariant().Contains(".ASPX"))
        return;

      if (context.Request.UrlReferrer != null)
      {
        Uri referrer = context.Request.UrlReferrer;
          //Usually UrlReferrer.Host and our own Url's Host are same becoz mostly our pages are referred to
          //from our website only.(through links, mostly). If someone else gives a link to a resource on our site we make
          //a log of it -> to prevent leeching obviously
        if (!referrer.Host.Equals(HelperUtilities.InternetAppRoot.Host, StringComparison.OrdinalIgnoreCase) && !IsSearchEngine(referrer.ToString()))
        {
			ThreadPool.QueueUserWorkItem(BeginRegisterClick, new DictionaryEntry(referrer, context.Request.Url));            
        }
      }
    }

    private static object _SyncRoot = new object();
    private static string _Folder = HttpContext.Current.Server.MapPath("~/App_Data/log/");

    private static bool IsSearchEngine(string referrer)
    {
      string lower = referrer.ToUpperInvariant();
      if (lower.Contains("YAHOO") && lower.Contains("P="))
        return true;
      return lower.Contains("?Q=") || lower.Contains("&Q=");        
    }

    private static bool IsSpam(Uri referrer, Uri url)
    {
      try
      {
        using (WebClient client = new WebClient())
        {
          string html = client.DownloadString(referrer).ToUpperInvariant();
          string subdomain = GetSubDomain(url);
          string host = url.Host.ToUpperInvariant();
          
          if (subdomain != null)
            host = host.Replace(subdomain.ToUpperInvariant() + ".", string.Empty);

          return !html.Contains(host);
        }
      }
      catch
      {
        return true;
      }
    }

    // Retrieves the subdomain from the specified URL.
    private static string GetSubDomain(Uri url)
    {
      // www.chandru.blogspot.com => length=4
      if (url.HostNameType == UriHostNameType.Dns)
      {
        string host = url.Host;
        if (host.Split('.').Length > 2)
        {
					int lastIndex = host.LastIndexOf(".");
					int index = host.LastIndexOf(".", lastIndex - 1);
          return host.Substring(0, index);
        }
      }

      return null;
    }

    private static void BeginRegisterClick(object stateInfo)
    {
			try
			{
				DictionaryEntry entry = (DictionaryEntry)stateInfo;
				Uri referrer = (Uri)entry.Key;
				Uri url = (Uri)entry.Value;

				RegisterClick(url, referrer);
				stateInfo = null;
				OnReferrerRegistered(referrer);
			}
			catch (Exception)
			{
				// Somewhere while writing to the document or checking against the document, or due to 
                // concurrancy or sync issues.
                // Could write to the file.
			}
    }

    private static void RegisterClick(Uri url, Uri referrer)
    {
      string fileName = _Folder + DateTime.Now.Date.ToString("dddd") + ".xml";

      lock (_SyncRoot)
      {
        XmlDocument doc = CreateDocument(fileName);

        string address = HttpUtility.HtmlEncode(referrer.ToString());
        //suppose address is http://www.dotnetkicks.com/page/2 (or) http://feeds.feedburner.com/netslave
        XmlNode node = doc.SelectSingleNode("urls/url[@address='" + address + "']");
        // node'll be=null if no such <url> with attribute as http://www.dotnetkicks.com/page/2 (or) http://feeds.feedburner.com/netslave is present.
        // so if it has www we remove it & check, if it doesn't have www we put it and check...
        //still if the node is null we put the host directly and 
				if (node == null && address.Contains("www."))
					node = doc.SelectSingleNode("urls/url[@address='" + address.Replace("www.", string.Empty) + "']");
        // node'll be=null if no such <url> with attribute as http://dotnetkicks.com/page/2 is present.
				if (node == null && !address.Contains("www."))
					node = doc.SelectSingleNode("urls/url[@address='" + address.Replace("://", "://www.") + "']");
        // node'll be=null if no such <url> with attribute as http://feeds.feedburner.com/netslave is present.
				if (node == null)
					node = doc.SelectSingleNode("urls/url[@address='" + HttpUtility.HtmlEncode("http://" + referrer.Host) + "']");
                // node'll be=null if no such <url> with attribute as http://localhost is present.
        if (node == null)
        {
					bool isSpam = IsSpam(referrer, url);
					if (isSpam)
					{
						address = HttpUtility.HtmlEncode("http://" + referrer.Host);
					}
          AddNewUrl(doc, address, isSpam);
        }
        else
        {
          int count = int.Parse(node.InnerText);
          node.InnerText = (count + 1).ToString();
        }
        doc.Save(fileName);
      }
    }

    // Adds a new Url to the XmlDocument.
    private static void AddNewUrl(XmlDocument doc, string address, bool isSpam)
    {
      XmlNode newNode = doc.CreateElement("url");

      XmlAttribute attrAddress = doc.CreateAttribute("address");
      attrAddress.Value = address;
      newNode.Attributes.Append(attrAddress);      

      XmlAttribute attrSpam = doc.CreateAttribute("isSpam");
      attrSpam.Value = isSpam.ToString();
      newNode.Attributes.Append(attrSpam);

      newNode.InnerText = "1";
      doc.ChildNodes[1].AppendChild(newNode);
    }

    private static DateTime _Date = DateTime.Now;

    // Creates the XML file for first time use.
    private static XmlDocument CreateDocument(string fileName)
    {
      XmlDocument doc = new XmlDocument();
      if (!Directory.Exists(_Folder))
        Directory.CreateDirectory(_Folder);
        if (DateTime.Now.Day != _Date.Day || !File.Exists(fileName))
      {
        using (XmlWriter writer = XmlWriter.Create(fileName))
        {
          writer.WriteStartDocument(true);
          writer.WriteStartElement("urls");
          writer.WriteEndElement();
        }
      }
      _Date = DateTime.Now;
      doc.Load(fileName);
      return doc;
    }

    public static event EventHandler<EventArgs> ReferrerRegistered;    
    private static void OnReferrerRegistered(Uri referrer)
    {
      if (ReferrerRegistered != null)
      {
        ReferrerRegistered(referrer, EventArgs.Empty);
      }
    }
  }
}