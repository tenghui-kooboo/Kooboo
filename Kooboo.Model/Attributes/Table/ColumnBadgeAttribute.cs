using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Attributes
{
    public class ColumnBadgeAttribute:Attribute
    {
        public string Class { get; set; }

        //modal
        public string ModalDataForm { get; set; }
        public string ModalName { get; set; }

        //url
        public string Url { get; set; }
        public string[] Paras { get; set; }

        public ColumnBadgeAttribute(string clas,string modalName,string modalDataForm, string url,params string[] paras)
        {
            Class = clas;
            ModalName = modalName;
            ModalDataForm = modalDataForm;
            Url = url;
            Paras = paras;
        }
    }
}
