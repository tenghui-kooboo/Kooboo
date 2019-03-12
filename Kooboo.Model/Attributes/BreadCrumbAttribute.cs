using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public class BreadCrumbAttribute:Attribute
    {
        public string DisplayName { get; set; }

        public string Url { get; set; }

        public BreadCrumbAttribute(string displayName,string url)
        {
            DisplayName = displayName;
            Url = url;
        }
    }
}
