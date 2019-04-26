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

        private NamedMetaCollection<FormItem> _items;
        public NamedMetaCollection<FormItem> Items
        {
            get
            {
                if (_items == null)
                {
                    _items = new NamedMetaCollection<FormItem>();
                }
                return _items;
            }
        }
    }
}
