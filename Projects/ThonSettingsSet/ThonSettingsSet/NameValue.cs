using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThonSettingsSet
{
    class NameValue
    {
        public NameValue(string key, string value)
        {
            this._key = key;
            this._value = value;
        }
        private string _key;
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        private string _value;
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}
