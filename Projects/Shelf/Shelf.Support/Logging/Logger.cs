using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Shelf.Support.Logging
{
    public class LogWriter : IDisposable
    {
        private XmlTextWriter w;

        /// <summary>
        /// Holds the only object of this class
        /// </summary>
        private static LogWriter _ensureSingleton;

        /// <summary>
        /// Allows access to the only possible object for this class
        /// </summary>
        public static LogWriter Instance
        {
            get
            {
                if (_ensureSingleton == null)
                    _ensureSingleton = new LogWriter();
                return _ensureSingleton;
            }
        }

        private LogWriter()
        {
            _ensureSingleton = null;
            w = new XmlTextWriter(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Logs.xml"), Encoding.UTF8);
        }


        ~LogWriter()
        {
            try
            {
                w.Flush();
                w.Close();
            }
            catch { }
        }


        #region IDisposable Members

        void IDisposable.Dispose()
        {
            try
            {
                w.Flush();
                w.Close();
            }
            catch { }
        }

        #endregion

        public void PerformanceLog(object sender, string message)
        {
            try
            {
                w.WriteStartElement("PLog");
                w.WriteAttributeString("Date", DateTime.Now.ToShortDateString());
                w.WriteAttributeString("Time", DateTime.Now.ToShortTimeString());
                w.WriteString("Sender: " + sender.GetType().FullName + "   Message: " + message);
                w.WriteEndElement();
                w.WriteWhitespace(System.Environment.NewLine);
                w.Flush();
            }
            catch (Exception ex)
            {
                Logger.LogThis("Logger", "Exception while logging : " + ex.ToString());
            }
        }
    }

    public static class Logger
    {
        private static LogWriter rr;

        static Logger()
        {
            rr = LogWriter.Instance;
        }

        #region A Zasz RAHA event

        /// <summary>
        /// Delegate object that stores and calls hooked up handlers
        /// </summary>
        /// <param name="sender">Object that raises the event</param>
        /// <param name="message">Message that the sender object sends</param>
        public delegate void LogDelegate(string sender,string message);
        
        /// <summary>
        /// A Raise Anywhere, Handle Anywhere event, that can be used for logging.
        /// </summary>
        public static event LogDelegate Log;

        /// <summary>
        /// Helper method that check whether an event has any hooked up handlers,
        /// and only if it has, raises the event.
        /// </summary>
        /// <param name="sender">Object that raises the event</param>
        /// <param name="message">Message that the sender object sends</param>
        public static void LogThis(string sender,string message)
        {
            if(Log != null)
            {
                Log(sender,message);
            }
        }

        #endregion

        public static void PerformanceLog(object sender, string message)
        {
            rr.PerformanceLog(sender, message);
        }
    }
}
