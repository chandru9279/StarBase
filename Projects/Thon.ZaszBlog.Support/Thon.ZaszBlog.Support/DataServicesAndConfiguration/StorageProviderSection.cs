using System;
using System.Collections.Generic;
using System.Configuration;

namespace Thon.ZaszBlog.Support.DataServicesAndConfiguration
{  
  // GetSection() method returns an object with all the configuration settings.
  // This class is the type of that object. Also a type must be specified in the web.config file.
  public class StorageProviderSection : ConfigurationSection
  { 
    // A collection of registered providers.    
    [ConfigurationProperty("providers")]
    public ProviderSettingsCollection Providers
    {
      get { return (ProviderSettingsCollection)base["providers"]; }
    }

    
    // The name of the default provider     
    [StringValidator(MinLength = 1)]
    [ConfigurationProperty("defaultProvider", DefaultValue = "XmlStorageProvider")]
    public string DefaultProvider
    {
      get { return (string)base["defaultProvider"]; }
      set { base["defaultProvider"] = value; }
    }

  }
}
