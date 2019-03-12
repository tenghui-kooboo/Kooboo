using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.Attributes;

namespace Kooboo.Model.Components.Table
{
    public class BadgeCell : ICell
    {
        public string ColumnName { get; set; }

        public CellType CellType => CellType.Badge;

        public CellDataType CellDataType { get; set; }

        public string Class { get; set; }

        //modal
        public string ModalDataForm { get; set; }
        public string ModalName { get; set; }

        //url
        public string Url { get; set; }
        public string[] Paras { get; set; }

        public object GetData()
        {
            var dic = new Dictionary<string, object>();
            dic.Add("class", Class);
            dic.Add("url", Url);
            dic.Add("paras", Paras);
            dic.Add("modalName", ModalName);
            dic.Add("modalDataForm", ModalDataForm);
            return dic;
        }

        public void SetData(List<Attribute> attrs)
        {
            var attr = attrs.Find(a => a is ColumnBadgeAttribute) as ColumnBadgeAttribute;
            if (attr != null)
            {
                Class = attr.Class;
                Url = attr.Url;
                Paras = attr.Paras;
                ModalName = attr.ModalName;
                ModalDataForm = attr.ModalDataForm;
            }
        }
    }
}
