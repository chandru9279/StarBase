using System;
using System.Net.Mail;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Web;
using System.Threading;

namespace Thon.Support
{	
	// Utilities for the entire solution to use.	
	public static class HelperUtilities
	{
		public static string RelativeAppRoot
		{
            // will return like "/ThonZNet/" for "~/" in web.config
			get { return VirtualPathUtility.ToAbsolute("~/"); }
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
                    _InternetAppRoot = new Uri(context.Request.Url.Scheme + "://" + context.Request.Url.Authority + RelativeAppRoot);
                }
                return _InternetAppRoot;
                // will return like "http://www.thon.net/" (or)
                // "http://163.160.2.73:8080/ThonZNet/"  (or)
                // "http://localhost:49174/ThonZNet/"
            }
        }

		public static void SendMailMessage(MailMessage message)
		{
			if (message == null)
				throw new ArgumentNullException("message");

			try
			{
				message.IsBodyHtml = true;
				message.BodyEncoding = Encoding.UTF8;
				SmtpClient smtp = new SmtpClient(ThonSettings.Instance.SmtpServer);
				smtp.Credentials = new System.Net.NetworkCredential(ThonSettings.Instance.SmtpUserName, ThonSettings.Instance.SmtpPassword);
				smtp.Port = ThonSettings.Instance.SmtpServerPort;
				smtp.EnableSsl = ThonSettings.Instance.EnableSsl;
				smtp.Send(message);
				OnEmailSent(message);
			}
			catch (SmtpException)
			{
				OnEmailFailed(message);
			}
			finally
			{
				// Remove the pointer to the message object so the GC can close the thread.
				message.Dispose();
				message = null;
			}
		}

		public static void SendMailMessageAsync(MailMessage message)
		{
			ThreadPool.QueueUserWorkItem(delegate { HelperUtilities.SendMailMessage(message); });
		}

		public static event EventHandler<EventArgs> EmailSent;
		private static void OnEmailSent(MailMessage message)
		{
			if (EmailSent != null)
			{EmailSent(message, new EventArgs());}
		}

		public static event EventHandler<EventArgs> EmailFailed;
		private static void OnEmailFailed(MailMessage message)
		{
			if (EmailFailed != null)
			{EmailFailed(message, new EventArgs());}
		}
    }
}
