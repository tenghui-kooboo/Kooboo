using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Components.BreadCrumb
{
    public class KBreadCrumb:IComponent
    {
        public ComponentType Type => ComponentType.KBreadCrumb;

        public List<KBreadCrumbItem> Breadcrumbs { get; set; } = new List<KBreadCrumbItem>();

        public string DisplayName { get; set; }

        public string Url { get; set; }

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
