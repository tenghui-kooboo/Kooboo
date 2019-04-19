using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Model.Meta.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FormOptionAttribute : Attribute, IMetaAttribute
    {
        public bool IsHeader => false;

        public string PropertyName => "optionConfig";

        public string DisplayNameKey { get; set; }

        public string ValueKey { get; set; }

        //public DefaultOptionAttribute Option { get; set; }

        public FormOptionAttribute(string displayName,string value)
        {
            DisplayNameKey = displayName;
            ValueKey = value;
            //Option = option;
        }
        public object Value()
        {
            var dic = new Dictionary<string, object>();
            dic.Add("displayName", DisplayNameKey);
            dic.Add("value", ValueKey);

            //if(Option!=null)
            //{
            //    var defaultDic = new Dictionary<string, string>();
            //    defaultDic.Add("displayName", Option.DisplayName);
            //    defaultDic.Add("value", Option.Value);
            //    dic.Add("default", defaultDic);
            //}
            
            return dic;
        }
    }

    public class DefaultOptionAttribute : Attribute
    {
        public string DisplayName { get; set; }

        public string Value{ get; set; }
        public DefaultOptionAttribute(string displayName,string value)
        {
            DisplayName = displayName;
            Value = value;
        }
    }

}
