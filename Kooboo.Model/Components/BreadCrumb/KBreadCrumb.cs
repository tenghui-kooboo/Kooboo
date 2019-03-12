using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.Attributes;

namespace Kooboo.Model.Components.BreadCrumb
{
    public class KBreadCrumb:IComponent
    {
        public ComponentType Type => ComponentType.KBreadCrumb;

        public List<KBreadCrumbItem> Breadcrumbs { get; set; } = new List<KBreadCrumbItem>();

        public VueField GetField()
        {
            //todo can be optimized
            var list = new List<Dictionary<string, string>>();
            
            foreach(var item in Breadcrumbs)
            {
                list.Add(item.GetData());
            }
           

            var field = new VueField()
            {
                Name = "breadCrumb",
                Value = list
            };

            return field;
        }

        public bool IsMatch(Attribute attribute)
        {
            return attribute is BreadCrumbAttribute;
        }

        public bool IsValid()
        {
            var isValid = true;
            for(var i=0;i<Breadcrumbs.Count;i++)
            {
                var item = Breadcrumbs[i];
                //last item don't need url
                if(string.IsNullOrEmpty(item.DisplayName) ||
                    (string.IsNullOrEmpty(item.Url) && i!=Breadcrumbs.Count-1))
                {
                    isValid = false;
                    break;
                }

            }

            return isValid;
        }

        public void SetData(List<Dictionary<string, List<Attribute>>> attributes)
        {
            foreach(var attr in attributes)
            {
                foreach(var keyPair in attr)
                {
                    var list = keyPair.Value;
                    foreach(var item in list)
                    {
                        var breadCrumbAttr = item as BreadCrumbAttribute;
                        if(breadCrumbAttr!=null)
                        {
                            var breadCrumbItem = new KBreadCrumbItem()
                            {
                                DisplayName = breadCrumbAttr.DisplayName,
                                Url = breadCrumbAttr.Url
                            };
                            Breadcrumbs.Add(breadCrumbItem);
                        }
                        
                    }
                   
                }
            }
        }
    }

    public class KBreadCrumbItem
    {
        public string DisplayName { get; set; }

        public string Url { get; set; }

        public Dictionary<string,string> GetData()
        {
            var dic = new Dictionary<string, string>();
            dic.Add("displayName", DisplayName);
            dic.Add("url", Url);
            return dic;
        }
    }
}
