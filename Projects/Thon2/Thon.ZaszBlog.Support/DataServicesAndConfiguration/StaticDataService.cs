using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Web.Configuration;
using System.Web;
using Thon.ZaszBlog.Support.CodedRepresentations;

namespace Thon.ZaszBlog.Support.DataServicesAndConfiguration
{
    
    // The proxy class for communication between the business objects and the providers.
    // FULLY STATIC - so sync is necessary.
    public static class StaticDataService
    {

        #region Provider model

        private static StorageProvider _provider;
        private static StorageProviderCollection _providers;
        private static object _lock = new object();
        
        public static StorageProvider Provider
        {
            get { return _provider; }
        }
        
        public static StorageProviderCollection Providers
        {
            get { return _providers; }
        }

        // Load the providers from the web.config.        
        private static void LoadProviders()
        {
            // Avoid claiming lock if providers are already loaded
            if (_provider == null)
            {
                lock (_lock)
                {   
                    if (_provider == null)
                    {
                        StorageProviderSection section = (StorageProviderSection)WebConfigurationManager.GetSection("ThonZaszBlogSupport/StorageProvider");
                        // Load registered providers and point _provider to the default provider
                        _providers = new StorageProviderCollection();
                        ProvidersHelper.InstantiateProviders(section.Providers, _providers, typeof(StorageProvider));
                        _provider = _providers[section.DefaultProvider];

                        if (_provider == null)
                            throw new ProviderException("Unable to load default StorageProvider");
                    }
                }
            }
        }

        #endregion

        #region Posts

        
        // Returns a Post based on the specified id.
        public static Post SelectPost(Guid id)
        {
            LoadProviders();
            return _provider.SelectPost(id);
        }

        
        // Persists a new Post in the current provider.
        public static void InsertPost(Post post)
        {
            LoadProviders();
            _provider.InsertPost(post);
        }

        
        // Updates an exsiting Post.
        
        public static void UpdatePost(Post post)
        {
            LoadProviders();
            _provider.UpdatePost(post);
        }

        
        // Deletes the specified Post from the current provider.
        public static void DeletePost(Post post)
        {
            LoadProviders();
            _provider.DeletePost(post);
        }

        public static List<Post> FillPosts()
        {
            LoadProviders();
            return _provider.FillPosts();
        }

        #endregion

        #region Pages

        
        // Returns a Page based on the specified id.        
        public static Page SelectPage(Guid id)
        {
            LoadProviders();
            return _provider.SelectPage(id);
        }

        
        // Persists a new Page in the current provider.        
        public static void InsertPage(Page page)
        {
            LoadProviders();
            _provider.InsertPage(page);
        }

        
        // Updates an exsiting Page.        
        public static void UpdatePage(Page page)
        {
            LoadProviders();
            _provider.UpdatePage(page);
        }

        
        // Deletes the specified Page from the current provider.        
        public static void DeletePage(Page page)
        {
            LoadProviders();
            _provider.DeletePage(page);
        }
                
        public static List<Page> FillPages()
        {
            LoadProviders();
            return _provider.FillPages();
        }

        #endregion

        #region Categories

        
        // Returns a Category based on the specified id.        
        public static Category SelectCategory(Guid id)
        {
            LoadProviders();
            return _provider.SelectCategory(id);
        }

        
        // Persists a new Category in the current provider.        
        public static void InsertCategory(Category category)
        {
            LoadProviders();
            _provider.InsertCategory(category);
        }

        
        // Updates an exsiting Category.        
        public static void UpdateCategory(Category category)
        {
            LoadProviders();
            _provider.UpdateCategory(category);
        }

        
        // Deletes the specified Category from the current provider.        
        public static void DeleteCategory(Category category)
        {
            LoadProviders();
            _provider.DeleteCategory(category);
        }
        
        public static List<Category> FillCategories()
        {
            LoadProviders();
            return _provider.FillCategories();
        }

        #endregion

        #region Settings

        // Loads the settings from the provider(XML provider which uses the 
        // BlogSettings.Instance.StorageLocation/settings.xml as persistance data store )
        // and returns them in a StringDictionary for the BlogSettings class to use.        
        public static StringDictionary LoadSettings()
        {
            //LoadProviders();
            //return _provider.LoadSettings();

            //This is used for Winforms application to access the settings
            Thon.ZaszBlog.Support.XmlProvider.XmlStorageProvider inst = new Thon.ZaszBlog.Support.XmlProvider.XmlStorageProvider();
            return inst.LoadSettings();

        }

        
        // Save the settings to the current provider (See above comment).        
        public static void SaveSettings(StringDictionary settings)
        {
            //LoadProviders();
            //_provider.SaveSettings(settings);

            //This is used for Winforms application to access the settings
            Thon.ZaszBlog.Support.XmlProvider.XmlStorageProvider inst = new Thon.ZaszBlog.Support.XmlProvider.XmlStorageProvider();
            inst.SaveSettings(settings);
        }

        #endregion

        #region Ping services
      
        public static StringCollection LoadPingServices()
        {
            LoadProviders();
            return _provider.LoadPingServices();
        }

        public static void SavePingServices(StringCollection services)
        {
            LoadProviders();
            _provider.SavePingServices(services);
        }

        #endregion

    }
}
