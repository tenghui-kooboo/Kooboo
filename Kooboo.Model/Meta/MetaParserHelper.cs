using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Kooboo.Model.Meta.Attributes;
using System.Linq;
using Kooboo.Model.Meta.Definition;
namespace Kooboo.Model.Meta
{
    public class MetaParserHelper
    {
        public static List<Dictionary<string,object>> GetMenu(IKoobooModel model)
        {
            var properties = model.GetType().GetProperties().ToList();
            return GetMeta(properties, typeof(MenuBtn));
             
        }
        public static List<Dictionary<string,object>> GetMeta(List<PropertyInfo> properties,Type modelType)
        {
            var list = new List<Dictionary<string, object>>();

            var modelProperties = modelType.GetProperties();
            foreach (var prop in properties)
            {
                var dic = new Dictionary<string, object>();
                var attrs = prop.GetCustomAttributes().ToList()
                    .Where(a=>a is IMetaAttribute)
                    .Select(a => a as IMetaAttribute).ToList();
                if(attrs.Count>0)
                {
                    foreach (var itemPropery in modelProperties)
                    {
                        var attr = attrs.Find(a => a.PropertyName.Equals(itemPropery.Name, StringComparison.OrdinalIgnoreCase));
                        if (attr != null)
                        {
                            dic.Add(attr.PropertyName, attr.Value());
                        }

                    }
                    list.Add(dic);
                }
                

            }
            return list;
        }
    }
}
