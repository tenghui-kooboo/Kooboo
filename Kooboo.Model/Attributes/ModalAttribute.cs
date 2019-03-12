using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public class ModalAttribute:Attribute
    {
        public string ModalName { get; set; }

        public string DataFrom { get; set; }

        public string[] Paras { get; set; }

        public ModalAttribute(string modalName,string dataFrom,string[] paras)
        {
            ModalName = modalName;
            DataFrom = DataFrom;
            Paras = paras;
        }
    }
}
