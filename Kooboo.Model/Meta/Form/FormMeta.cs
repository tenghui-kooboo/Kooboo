using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta.Form
{
    public class FormMeta : IViewMeta
    {
        public string LoadApi { get; set; }

        public string SubmitApi { get; set; }

        private FormItemCollection _items;
        public FormItemCollection Items
        {
            get
            {
                if (_items == null)
                {
                    _items = new FormItemCollection();
                }
                return _items;
            }
        }
    }
}
