using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Attributes
{
    public class LinkAttribute : Attribute
    {
        public string FieldName { get; set; }
        public string Url { get; set; }
        public string[] Paras { get; set; }
        //url can be optimized
        public LinkAttribute(string name, string url, params string[] paras)
        {
            FieldName = name;
            Url = url;
            Paras = paras;
        }
    }
}
