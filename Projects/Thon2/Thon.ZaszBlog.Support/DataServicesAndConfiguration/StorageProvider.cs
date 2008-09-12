using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using Thon.ZaszBlog.Support.CodedRepresentations;

namespace Thon.ZaszBlog.Support.DataServicesAndConfiguration
{
    public abstract class StorageProvider : ProviderBase
    {
        public abstract Post SelectPost(Guid id);
        public abstract void InsertPost(Post post);
        public abstract void UpdatePost(Post post);
        public abstract void DeletePost(Post post);
        public abstract List<Post> FillPosts();

        public abstract Page SelectPage(Guid id);
        public abstract void InsertPage(Page page);
        public abstract void UpdatePage(Page page);
        public abstract void DeletePage(Page page);
        public abstract List<Page> FillPages();

        public abstract Category SelectCategory(Guid id);
        public abstract void InsertCategory(Category category);
        public abstract void UpdateCategory(Category category);
        public abstract void DeleteCategory(Category category);
        public abstract List<Category> FillCategories();

        public abstract StringDictionary LoadSettings();
        public abstract void SaveSettings(StringDictionary settings);
   
        public abstract StringCollection LoadPingServices();
        public abstract void SavePingServices(StringCollection services);

    }
   
    public class StorageProviderCollection : ProviderCollection
    {
        public new StorageProvider this[string name]
        {
            get { return (StorageProvider)base[name]; }
        }

        public override void Add(ProviderBase provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            if (!(provider is StorageProvider))
                throw new ArgumentException
                    ("Invalid provider type", "provider");

            base.Add(provider);
        }
    }

}
