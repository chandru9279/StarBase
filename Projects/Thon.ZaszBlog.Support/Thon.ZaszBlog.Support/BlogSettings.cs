using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Web;
using Thon.ZaszBlog.Support.DataServicesAndConfiguration;
using Thon.ZaszBlog.Support.XmlProvider;
using Thon.ZaszBlog.Support.CodedRepresentations;

namespace Thon.ZaszBlog.Support
{
	public class BlogSettings
	{
        public static event EventHandler<EventArgs> Changed;    //raised when settings have been changed
        
        #region Constructor		

		private BlogSettings()
		{Load();}

		#endregion		

        //Done
        #region GENERAL SETTINGS

                //============================================================
		        //	GENERAL SETTINGS
		        //============================================================
                
                #region Instance
                private static BlogSettings blogSettingsSingleton;      //singleton instance
                /// <summary>
                /// Used everytime a setting needs to be accessed
                /// </summary>
                public static BlogSettings Instance
                {
                    get
                    {
                        if (blogSettingsSingleton == null) blogSettingsSingleton = new BlogSettings();
                        return blogSettingsSingleton;
                    }
                }
                #endregion                

		        #region Description		
                private string blogDescription = String.Empty;
                /// <summary>
                /// This value is also used for the description meta tag.
                /// Description text used in the browser and opensearch
                /// </summary>
		        public string Description
		        {
			        get
			        {return blogDescription;}

			        set
			        {
				        if (String.IsNullOrEmpty(value)) blogDescription = String.Empty;				
				        else blogDescription = value;
			        }
		        }
		        #endregion
                // Gets the root of the application this blog is part of
                #region AppRoot
                //eg:C:\Users\Zasz\Documents\Visual Studio 2008\WebSites\ThonZNet\
                public string AppRoot
                {
                    get
                    {return System.Web.HttpRuntime.AppDomainAppPath;}
                }
                #endregion
        
                #region EnableRelatedPosts		
                private bool enableRelatedPosts;
		        public bool EnableRelatedPosts
		        {
			        get
			        {return enableRelatedPosts;}
			        set
			        {enableRelatedPosts = value;}
		        }
		        #endregion
                // Used in the browser and external link tags-syndication,opensearch,etc
		        #region Name
                private string blogName = String.Empty;
		        public string Name
		        {
			        get
			        {return blogName;}
			        set
			        {
				        if (String.IsNullOrEmpty(value)) blogName = String.Empty;				
				        else blogName = value;
			        }
		        }
		        #endregion

		        #region NumberOfRecentPosts		
		        // Gets or sets the default number of recent posts to display.
                private int numberOfRecentPosts = 10;		
		        public int NumberOfRecentPosts
		        {
			        get
			        {return numberOfRecentPosts;}
			        set
			        {numberOfRecentPosts = value;}
		        }
		        #endregion

		        #region NumberOfRecentComments		
		        // Gets or sets the default number of recent comments to display.		
                private int numberOfRecentComments = 10;
		        public int NumberOfRecentComments
		        {
			        get
			        {return numberOfRecentComments;}
			        set
			        {numberOfRecentComments = value;}
		        }
		        #endregion

		        #region PostsPerPage		
		        // Gets or sets the number of posts to show an each page.
                private int postsPerPage = Int32.MinValue;
		        public int PostsPerPage
		        {
			        get
			        {return postsPerPage;}
			        set
			        {postsPerPage = value;}
		        }
		        #endregion

		        #region ShowLivePreview		
		        // Gets or sets a value indicating if live preview of post is displayed.		
                private bool showLivePreview;
		        public bool ShowLivePreview
		        {
			        get
			        {return showLivePreview;}
			        set
			        {showLivePreview = value;}
		        }
		        #endregion
                // Used in Thon.ZaszBlog.Support.Web.ControlsPostViewBase
		        #region EnableRating		
                private bool enableRating;
		        public bool EnableRating
		        {
			        get
			        {return enableRating;}
			        set
			        {enableRating = value;}
		        }
		        #endregion
                // Used in PostViewBase and ThonZNet to get excerpt.
		        #region ShowDescriptionInPostList
		        // Gets or sets a value indicating if the full post is displayed in lists or only the description/exerpt.		
                private bool showDescriptionInPostList;
		        public bool ShowDescriptionInPostList
		        {
			        get
			        {return showDescriptionInPostList;}
			        set
			        {showDescriptionInPostList = value;}
		        }
		        #endregion
                // Used in Thon.ZaszBlog.Support.XmlProvider.XmlStorageProvider/StorageLocation
		        #region StorageLocation		
		        // Gets or sets the default storage location for blog data.
                private string storageLocation = String.Empty;
		        public string StorageLocation
		        {
			        get
			        {return storageLocation;}

			        set
			        {
				        if (String.IsNullOrEmpty(value)) storageLocation = String.Empty;
				        else storageLocation = value;
			        }
		        }
		        #endregion
                // Used in UrlRewrite
                #region FileExtension
                // The  file extension used for aspx pages
                private string _fileExt;
                public string FileExtension
                {
                    get 
                    {
                        if (String.IsNullOrEmpty(_fileExt))
                            return ".aspx";
                        else
                            return _fileExt;
                    }
                    set { _fileExt = value; }
                }
                #endregion
                // The Path of the blog inside the application, used in SupportUtilities
                #region VirtualPath
                private string _VirtualPath;
                public string VirtualPath
                {
                    get 
                    {
                        if (String.IsNullOrEmpty(_VirtualPath))
                            return "~/ZaszBlog";
                        else
                            return _VirtualPath;
                    }
                    set { _VirtualPath = value; }
                }
                #endregion
                
                #region DisplayCommentsOnRecentPosts
                private bool displayCommentsOnRecentPosts;
		        public bool DisplayCommentsOnRecentPosts
		        {   get
			        {return displayCommentsOnRecentPosts;}
			        set
			        {displayCommentsOnRecentPosts = value;}
                }
                #endregion

                #region DisplayRatingsOnRecentPosts
                private bool displayRatingsOnRecentPosts;
                public bool DisplayRatingsOnRecentPosts
		        {   get
			        {return displayRatingsOnRecentPosts;}
			        set
			        {displayRatingsOnRecentPosts = value;}
		        }
                #endregion

                #region ShowPostNavigation
                private bool showPostNavigation;
                public bool ShowPostNavigation
		        {
			        get { return showPostNavigation; }
			        set { showPostNavigation = value; }
		        }
                #endregion		

		        #region EnablePingBackSend
		        private bool _EnablePingBackSend;		
		        public bool EnablePingBackSend
		        {
			        get { return _EnablePingBackSend; }
			        set { _EnablePingBackSend = value; }
		        }
		        #endregion

		        #region EnablePingBackReceive
		        private bool _EnablePingBackReceive;		
		        public bool EnablePingBackReceive
		        {
			        get { return _EnablePingBackReceive; }
			        set { _EnablePingBackReceive = value; }
		        }
		        #endregion

		        #region EnableTrackBackSend
		        private bool _EnableTrackBackSend;
		        public bool EnableTrackBackSend
		        {
			        get { return _EnableTrackBackSend; }
			        set { _EnableTrackBackSend = value; }
		        }
		        #endregion

		        #region EnableTrackBackReceive
		        private bool _EnableTrackBackReceive;
		        public bool EnableTrackBackReceive
		        {
			        get { return _EnableTrackBackReceive; }
			        set { _EnableTrackBackReceive = value; }
		        }
		        #endregion

        #endregion

        //Done
        #region FEEDS AND SYNDICATION SETTINGS
                // Used in Thon.ZaszBlog.Support.SyndicationGenerator & SyndicationHandler
                #region PostsPerFeed
                // Gets or sets the maximum number of characters that are displayed from a blog-roll retrieved post.
                private int postsPerFeed;
                public int PostsPerFeed
                {
                    get
                    { return postsPerFeed; }
                    set
                    { postsPerFeed = value; }
                }
                #endregion
                // Used in Thon.ZaszBlog.Support.SyndicationGenerator & SyndicationHandler
                #region SyndicationFormat
                // Gets or sets the default syndication format used by the blog.
                // If no value is specified, blog defaults to using RSS 2.0 format.        
                private string defaultSyndicationFormat = String.Empty;
                public string SyndicationFormat
                {
                    get
                    { return defaultSyndicationFormat; }
                    set
                    {
                        if (String.IsNullOrEmpty(value)) defaultSyndicationFormat = String.Empty;
                        else defaultSyndicationFormat = value;
                    }
                }
                #endregion
                // Used in Thon.ZaszBlog.Support.SyndicationGenerator & SyndicationHandler
                #region AlternateFeedUrl
                private string alternateFeedUrl;
                public string AlternateFeedUrl
                {
                    get { return alternateFeedUrl; }
                    set { alternateFeedUrl = value; }
                }
                #endregion
        #endregion

        //Done
        #region COMMENT SETTINGS

                //============================================================
		        //	COMMENT SETTINGS
		        //============================================================

		        #region DaysCommentsAreEnabled
		        // Gets or sets the number of days that a post accepts comments.
		        // After this time period has expired, comments on a post are disabled.
                private int daysCommentsAreEnabled = Int32.MinValue;
		        public int DaysCommentsAreEnabled
		        {
			        get
			        {return daysCommentsAreEnabled;}
			        set
			        {daysCommentsAreEnabled = value;}
		        }
		        #endregion

		        #region EnableCountryInComments		
		        // Gets or sets a value indicating if dispay of the country of commenter is enabled.		
                private bool enableCountryInComments;
		        public bool EnableCountryInComments
		        {
			        get
			        {return false;}
			        set
			        {enableCountryInComments = value;}
		        }
		        #endregion
                //Initially set to false. Depends on CoComment.
		        #region IsCoCommentEnabled		
		        // Gets or sets a value indicating if CoComment support is enabled.
                private bool isCoCommentEnabled;
		        public bool IsCoCommentEnabled
		        {
			        get
			        {return isCoCommentEnabled;}
			        set
			        {isCoCommentEnabled = value;}
		        }
		        #endregion

		        #region IsCommentsEnabled
                // Gets or sets a value indicating if comments are enabled for posts.
                private bool areCommentsEnabled;		
		        public bool IsCommentsEnabled
		        {
			        get
			        {return areCommentsEnabled;}
			        set
			        {areCommentsEnabled = value;}
		        }
		        #endregion
                
		        #region EnableCommentsModeration		
		        // Gets or sets a value indicating if comments moderation is used for posts.
                private bool areCommentsModerated = false;
		        public bool EnableCommentsModeration
		        {
			        get
			        {return areCommentsModerated;}
			        set
			        {areCommentsModerated = value;}
		        }
		        #endregion
                // Used in CommentViewBase
		        #region Avatar		
		        // Gets or sets a value indicating if Gravatars are enabled or not.
                private string avatar;
		        public string Avatar
		        {
			        get
			        {return avatar;}
			        set
			        {avatar = value;}
		        }
		        #endregion
                
                #region SendMailOnComment
                private bool sendMailOnComment;
                public bool SendMailOnComment
                {
                    get
                    { return sendMailOnComment; }
                    set
                    { sendMailOnComment = value; }
                }
                #endregion

        #endregion

        //Done
        #region BLOGROLL SETTINGS

                //============================================================
		        //	BLOG ROLL SETTINGS
		        //============================================================

		        #region BlogrollMaxLength		
		        // Gets or sets the maximum number of characters that are displayed from a blog-roll retrieved post.
                private int blogrollMaxLength = Int32.MinValue;
		        public int BlogrollMaxLength
		        {
			        get
			        {return blogrollMaxLength;}
			        set
			        {blogrollMaxLength = value;}
		        }
		        #endregion

		        #region BlogrollUpdateMinutes		
		        // Gets or sets the number of minutes to wait before polling blog-roll sources for changes.		
                private int blogrollUpdateMinutes = Int32.MinValue;
		        public int BlogrollUpdateMinutes
		        {
			        get
			        {return blogrollUpdateMinutes;}
			        set
			        {blogrollUpdateMinutes = value;}
		        }
		        #endregion

		        #region BlogrollVisiblePosts		
		        // Gets or sets the number of posts to display from a blog-roll source.		
                private int blogrollVisiblePosts = Int32.MinValue;
		        public int BlogrollVisiblePosts
		        {
			        get
			        {return blogrollVisiblePosts;}
			        set
			        {blogrollVisiblePosts = value;}
		        }
		        #endregion

        #endregion

        //Done
        #region SEARCH SETTINGS

                //============================================================
		        //	SEARCH SETTINGS
		        //============================================================

		        #region EnableCommentSearch		
		        // Gets or sets a value indicating if search of post comments is enabled.
                private bool enableCommentSearch;
		        public bool EnableCommentSearch
		        {
			        get
			        {return enableCommentSearch;}
			        set
			        {enableCommentSearch = value;}
		        }
		        #endregion

		        #region SearchButtonText		
		        // Gets or sets the search button text to be displayed.
                private string searchButtonText = String.Empty;
		        public string SearchButtonText
		        {
			        get
			        {return searchButtonText;}
			        set
			        {
                        if (String.IsNullOrEmpty(value)) searchButtonText = String.Empty;
				        else searchButtonText = value;
			        }
		        }
		        #endregion

		        #region SearchCommentLabelText		
		        // Gets or sets the search comment label text to display.
                private string searchCommentLabelText = String.Empty;
		        public string SearchCommentLabelText
		        {
			        get
			        {return searchCommentLabelText;}
			        set
			        { 
                        if (String.IsNullOrEmpty(value)) searchCommentLabelText = String.Empty;
				        else searchCommentLabelText = value;
			        }
		        }
		        #endregion

		        #region SearchDefaultText		
		        // Gets or sets the default search text to display.
                private string searchDefaultText = String.Empty;
		        public string SearchDefaultText
		        {
			        get
			        {return searchDefaultText;}
			        set
			        {
                        if (String.IsNullOrEmpty(value)) searchDefaultText = String.Empty;
				        else searchDefaultText = value;
			        }
		        }
		        #endregion

        #endregion

        //Done
        #region PRIVATE ROUTINES

                //============================================================
		        //	PRIVATE ROUTINES
		        //============================================================

		        #region Load()
                ///<summary>
                ///Loads values from the provider(eg. xml file ~/App_Data/ZaszBlog/ZaszBlogSettings.xml),
                ///see StaticDataService.LoadSettings() comment
                ///</summary>
		        private void Load()
		        {
			        Type settingsType = this.GetType();
			        System.Collections.Specialized.StringDictionary dic = StaticDataService.LoadSettings();
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
                                   // throw new ConfigurationErrorsException("Exception while setting values to BlogSettings.Instance");                                    
						        }
						        break;
					        }
				        }
			        }
		        }
		        #endregion

		        #region OnChanged()
        		/// <summary>
        		/// Safely raises the Changed event when the BlogSettings.Instance changes
        		/// </summary>
		        private static void OnChanged()
		        {
			        try
			        {
				        if (BlogSettings.Changed != null)
				            BlogSettings.Changed(null, new EventArgs());
			        }
			        catch
			        {
                        //Catch any exception that occurs while executing event handler and rethrow it.
				        throw;//Why? Nothing must be left to CLR, explicit coding gives better security, I think.
			        }
		        }
		        #endregion

        #endregion  

        //Done
        #region PUBLIC ROUTINES

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

			        StaticDataService.SaveSettings(dic);
			        OnChanged();
		        }
		        #endregion

        #endregion

    }
}
