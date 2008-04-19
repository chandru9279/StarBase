using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Thon.Support;
using System.Collections.Specialized;
using System.Reflection;
using Thon.ZaszBlog.Support;
using Thon.ZaszBlog.Support.DataServicesAndConfiguration;

namespace ThonSettingsSet
{
    public partial class Settings : Form
    {
        List<NameValue> nvl;

        public Settings()
        {
            InitializeComponent();
            nvl = new List<NameValue>();
        }

        private void LoadSettings_Click(object sender, EventArgs e)
        {
            System.Collections.Specialized.StringDictionary dic = new System.Collections.Specialized.StringDictionary();            
            Type settingsType = ThonSettings.Instance.GetType();
            foreach (PropertyInfo propertyInformation in settingsType.GetProperties())
            {
                try
                {
                    if (propertyInformation.Name != "Instance" && propertyInformation.Name != "StorageLocation" && propertyInformation.Name != "MyName")
                    {
                        object propertyValue = propertyInformation.GetValue(ThonSettings.Instance, null);
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

            foreach (string key in dic.Keys)
                nvl.Add(new NameValue(key, dic[key]));

            this.SettingsGrid.AutoGenerateColumns = true;
            this.SettingsSource.DataSource = nvl;
            this.SettingsGrid.Refresh();
        }

        private void SaveSettings_Click(object sender, EventArgs e)
        {
            System.Collections.Specialized.StringDictionary dic = new System.Collections.Specialized.StringDictionary();
            foreach (NameValue nv in nvl)
                dic.Add(nv.Key, nv.Value);
            StaticService.SaveSettings(dic);
        }

        private void SaveBlogSettings_Click(object sender, EventArgs e)
        {
            System.Collections.Specialized.StringDictionary dic = new System.Collections.Specialized.StringDictionary();
            foreach (NameValue nv in nvl)
                dic.Add(nv.Key, nv.Value);
            StaticDataService.SaveSettings(dic);
        }

        private void LoadBlogSettings_Click(object sender, EventArgs e)
        {
            System.Collections.Specialized.StringDictionary dic = new System.Collections.Specialized.StringDictionary();
            Type settingsType = BlogSettings.Instance.GetType();
            foreach (PropertyInfo propertyInformation in settingsType.GetProperties())
            {
                try
                {
                    if (propertyInformation.Name != "Instance" && propertyInformation.Name != "MyName")
                    {
                        object propertyValue = propertyInformation.GetValue(BlogSettings.Instance, null);
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

            foreach (string key in dic.Keys)
                nvl.Add(new NameValue(key, dic[key]));

            this.SettingsGrid.AutoGenerateColumns = true;
            this.SettingsSource.DataSource = nvl;
            this.SettingsGrid.Refresh();
        }
    }
}
