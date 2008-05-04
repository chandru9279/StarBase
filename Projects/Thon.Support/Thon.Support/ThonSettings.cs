using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Web;
using System.Configuration;

namespace Thon.Support
{
	public class ThonSettings
	{
        public static event EventHandler<EventArgs> Changed;    //raised when settings have been changed
        
        #region Constructor		

		private ThonSettings()
		{Load();}

		#endregion		

        //Done
        #region GENERALSETTINGS

                //============================================================
		        //	GENERAL SETTINGS
		        //============================================================

                //Done
                #region Instance
                private static ThonSettings thonSettingsSingleton;      //singleton instance
                public static ThonSettings Instance
                {
                    get
                    {
                        if (thonSettingsSingleton == null) thonSettingsSingleton = new ThonSettings();
                        return thonSettingsSingleton;
                    }
                }
                #endregion
                //Stores the Description
		        #region Description		
		        // This value is also used for the description meta tag.
                private string thonDescription = String.Empty;
		        public string Description
		        {
			        get
			        {return thonDescription;}

			        set
			        {
				        if (String.IsNullOrEmpty(value)) thonDescription = String.Empty;				
				        else thonDescription = value;
			        }
		        }
		        #endregion                
                //Used in the Compression Module
		        #region EnableHttpCompression		
                private bool enableHttpCompression;
		        public bool EnableHttpCompression
		        {
			        get
			        {return enableHttpCompression;}
			        set
			        {enableHttpCompression = value;}
		        }
		        #endregion
                //Used wherever name of the Application is needed
		        #region Name
                private string appName = String.Empty;
		        public string Name
		        {
			        get
			        {return appName;}
			        set
			        {
				        if (String.IsNullOrEmpty(value)) appName = String.Empty;				
				        else appName = value;
			        }
		        }
		        #endregion
                //Used pretty much everywhere
		        #region StorageLocation		
		        // Gets or sets the default storage location for website data.
		        public string StorageLocation
		        {
			        get
			        {return "~/App_Data/";}
		        }
		        #endregion
                //Used in Thon.ZaszBlog.Web.Controls.ThonBasePage
                #region RemoveWhitespaceInStyleSheets
                // Gets or sets a value indicating if whitespace in stylesheets should be removed        
                private bool removeWhitespaceInStyleSheets;
                public bool RemoveWhitespaceInStyleSheets
                {
                    get
                    {return removeWhitespaceInStyleSheets;}
                    set
                    {removeWhitespaceInStyleSheets = value;}
                }
                #endregion
                //Used in Thon.ZaszBlog.Web.Controls.ThonBasePage
		        #region EnableOpenSearch		
                private bool enableOpenSearch;
		        public bool EnableOpenSearch
		        {
			        get
			        {return enableOpenSearch;}
			        set
			        {enableOpenSearch = value;}
		        }
		        #endregion
                //Entered from the browser
		        #region TrackingScript		
		        // Gets or sets the tracking script used to collect visitor data.		
                private string trackingScript;
		        public string TrackingScript
		        {
			        get
			        {return trackingScript;}
			        set
			        {trackingScript = value;}
		        }
		        #endregion
                //Used in Thon.ZaszBlog.Web.HttpModules.WwwSubDomainModule
		        #region HandleWwwSubdomain
                private string handleWwwSubdomain;
		        // Gets or sets how to handle the www subdomain of the url (for SEO purposes).		
		        public string HandleWwwSubdomain
		        {
			        get
			        {return handleWwwSubdomain;}
			        set
			        {handleWwwSubdomain = value;}
                }
		        #endregion
                //Used in ReferrerModule
                #region EnableReferrerTracking
                private bool enableReferrerTracking;
                public bool EnableReferrerTracking
                {
                    get
                    { return enableReferrerTracking; }
                    set
                    { enableReferrerTracking = value; }
                }
                #endregion
                //Retrns T Chandirasekar always
                #region MyName
                public string MyName
                {
                    get
                    { return "T.Chandirasekar"; }
                }
                #endregion

        #endregion


        #region EMAILSETTINGS

                //============================================================
		        //	EMAIL SETTINGS
		        //============================================================

		        #region Email
                private string emailAddress = String.Empty;
		        public string Email
		        {
			        get
			        {return emailAddress;}
			        set
			        {
				        if (String.IsNullOrEmpty(value)) emailAddress = String.Empty;
				        else emailAddress = value;
			        }
		        }
		        #endregion

                #region EmailSubjectPrefix
                private string _EmailSubjectPrefix;
                /// <summary>
                /// Gets or sets the email subject prefix.
                /// </summary>
                /// <value>The email subject prefix.</value>
                public string EmailSubjectPrefix
                {
                    get { return _EmailSubjectPrefix; }
                    set { _EmailSubjectPrefix = value; }
                }
                #endregion

		        #region SmtpPassword		
                private string smtpPassword = String.Empty;
		        public string SmtpPassword
		        {
			        get
			        {return smtpPassword;}
			        set
			        {
				        if (String.IsNullOrEmpty(value)) smtpPassword = String.Empty;
				        else smtpPassword = value;
			        }
		        }
		        #endregion

		        #region SmtpServer
		        // Gets or sets the DNS name or IP address of the SMTP server used to send notification emails.
                private string smtpServer = String.Empty;		
		        public string SmtpServer
		        {
			        get
			        {return smtpServer;}
			        set
			        {
                        if (String.IsNullOrEmpty(value)) smtpServer = String.Empty;
				        else smtpServer = value;
			        }
		        }
		        #endregion        
                
		        #region SmtpServerPort
		        // Gets or sets the DNS name or IP address of the SMTP server used to send notification emails.		
                private int smtpServerPort = Int32.MinValue;
		        public int SmtpServerPort
		        {
			        get
			        {return smtpServerPort;}
			        set
			        {smtpServerPort = value;}
		        }
		        #endregion

		        #region SmtpUsername
                private string smtpUsername = String.Empty;
		        // Gets or sets the user name used to connect to the SMTP server.
		        public string SmtpUserName
		        {
			        get
			        {return smtpUsername;}
			        set
			        {
				        if (String.IsNullOrEmpty(value)) smtpUsername = String.Empty;
				        else smtpUsername = value;
			        }
		        }
		        #endregion

		        #region EnableSsl
		        // Gets or sets a value indicating if SSL is enabled for sending e-mails
                private bool enableSsl;
		        public bool EnableSsl
		        {
			        get
			        {return enableSsl;}
			        set
			        {enableSsl = value;}
		        }
		        #endregion

        #endregion

        // Done
        #region CONTACT_FORM

                //============================================================
		        //	CONTACT FORM
		        //============================================================

		        #region ContactFormMessage
                private string contactFormMessage;
		        public string ContactFormMessage
		        {
			        get { return contactFormMessage; }
			        set
			        {
				        if (String.IsNullOrEmpty(value)) contactFormMessage = String.Empty; 
				        else contactFormMessage = value; 
			        }
		        }
		        #endregion

		        #region ContactThankMessage
                private string contactThankMessage;
		        public string ContactThankMessage
		        {
			        get { return contactThankMessage; }
			        set
			        {
				        if (String.IsNullOrEmpty(value)) contactThankMessage = String.Empty; 
				        else contactThankMessage = value;
			        }
		        }
		        #endregion

		        #region HtmlHeader
                private string htmlHeader;
		        public string HtmlHeader
		        {
			        get { return htmlHeader; }
			        set
			        {
				        if (String.IsNullOrEmpty(value)) htmlHeader = String.Empty;
				        else htmlHeader = value;
			        }
		        }
		        #endregion

		        #region EnableContactAttachments
		        // Gets or sets whether or not to allow visitors to send attachments via the contact form.
                private bool enableContactAttachments;
		        public bool EnableContactAttachments
		        {
			        get { return enableContactAttachments; }
			        set { enableContactAttachments = value; }
		        }
		        #endregion

        #endregion

        // Done
        #region PRIVATE_ROUTINES

                //============================================================
		        //	PRIVATE ROUTINES
		        //============================================================

		        #region Load()
        		//Loads values from the provider(eg. xml file ~/App_Data/ThonSettings.xml),
                //see StaticDataService.LoadSettings() comment
		        private void Load()
		        {
			        Type settingsType = this.GetType();
			        System.Collections.Specialized.StringDictionary dic = StaticService.LoadSettings();
			        foreach (string key in dic.Keys)
			        {
				        string name = key;
				        string value = dic[key];
				        foreach (PropertyInfo propertyInformation in settingsType.GetProperties())
				        {
					        if (propertyInformation.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
					        {
						        try
						        {
							        propertyInformation.SetValue(this, Convert.ChangeType(value, propertyInformation.PropertyType), null);
						        }
						        catch(Exception e)
						        {
                                    throw e;
                                    //throw new ConfigurationErrorsException("Exception while setting values to ThonSettings.Instance");                                    
						        }
						        break;
					        }
				        }
			        }
		        }
		        #endregion

		        #region OnChanged()
        		
		        private static void OnChanged()
		        {
			        try
			        {
				        if (ThonSettings.Changed != null)
				            ThonSettings.Changed(null, new EventArgs());
			        }
			        catch
			        {
                        //Catch any exception that occurs while executing event handler and rethrow it.
				        throw;//Why? Nothing must be left to CLR, explicit coding gives better security, I(TC) think.
			        }
		        }
		        #endregion

        #endregion  

        // Done
        #region PUBLIC_ROUTINES

                //============================================================
		        //	PUBLIC ROUTINES
		        //============================================================

		        #region Save()        		
		        public void Save()
		        {
			        System.Collections.Specialized.StringDictionary dic = new System.Collections.Specialized.StringDictionary();
			        Type settingsType = this.GetType();
			        foreach (PropertyInfo propertyInformation in settingsType.GetProperties())
			        {
				        try
				        {
                            // Except this all hs to be persisted, this contains the current object
					        if (propertyInformation.Name != "Instance") 
					        {
						        object propertyValue = propertyInformation.GetValue(this, null);
						        string valueAsString = propertyValue.ToString();
						        //	Format null/default property values as empty strings
						        if (propertyValue.Equals(null))
						        {
							        valueAsString = String.Empty;
						        }
						        if (propertyValue.Equals(Int32.MinValue))
						        {
							        valueAsString = String.Empty;
						        }
						        if (propertyValue.Equals(Single.MinValue))
						        {
							        valueAsString = String.Empty;
						        }
						        dic.Add(propertyInformation.Name, valueAsString);
					        }
				        }
				        catch { }
			        }

			        StaticService.SaveSettings(dic);
			        OnChanged();
		        }
		        #endregion

        #endregion

    }
}
