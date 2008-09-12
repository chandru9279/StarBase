using System;
using System.IO;
using System.Xml;
using System.Net;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Thon.ZaszBlog.Support.CodedRepresentations;

namespace Thon.ZaszBlog.Support.Ping
{
  /// <summary>
  /// This class is used to send a trackback message and check the response.
  /// </summary>
  public static class Trackback
  {

    /// <summary>
    /// Sends a trackback message, and sees if te response is OK.
    /// </summary>
    /// <param name="message">The TrackbackMessage to send</param>
    /// <returns>A boolean value indicating if the message sending was successfull</returns>    
    public static bool Send(TrackbackMessage message)
    {
			if (!BlogSettings.Instance.EnableTrackBackSend)
				return false;

      if (message == null)
        throw new ArgumentNullException("message");

      OnSending(message.UrlToNotifyTrackback);
      HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(message.UrlToNotifyTrackback); 
      request.Credentials = CredentialCache.DefaultNetworkCredentials;
      request.Method = "POST";
      request.ContentLength = message.ToString().Length;
      request.ContentType = "application/x-www-form-urlencoded";
      request.KeepAlive = false;
      request.Timeout = 10000;

      using (StreamWriter myWriter = new StreamWriter(request.GetRequestStream()))
      {
        myWriter.Write(message.ToString());
      }

      bool result = false;
      HttpWebResponse response;
      try
      {
        response = (HttpWebResponse)request.GetResponse();
        OnSent(message.UrlToNotifyTrackback);
        string answer;
        using (System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream()))
        {
          answer = sr.ReadToEnd();
        }

        if (response.StatusCode == HttpStatusCode.OK)
        {
          //This could be a strict XML parsing if necessary maybe logging activity here too
          if (answer.Contains("<error>0</error>"))
          { result = true; }
          else
          { result = false; }
        }
        else
        { result = false; }
      }
      catch(Exception)
      {
        result = false;
      }
      return result;
    }

    #region

    /// <summary>
    /// Occurs just before a trackback is sent.
    /// </summary>
    public static event EventHandler<EventArgs> Sending;
    private static void OnSending(Uri url)
    {
      if (Sending != null)
      {
        Sending(url, new EventArgs());
      }
    }

    /// <summary>
    /// Occurs when a trackback has been sent
    /// </summary>
    public static event EventHandler<EventArgs> Sent;
    private static void OnSent(Uri url)
    {
      if (Sent != null)
      {
        Sent(url, new EventArgs());
      }
    }
    #endregion
  }
}