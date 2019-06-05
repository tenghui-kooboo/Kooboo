using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta.Popup
{
    public class PopupMeta : IViewMeta
    {
        public string Title { get; set; }

        public Description Description { get; set; }

        private List<IViewMeta> _views;
        public List<IViewMeta> Views
        {
            get
            {
                if (_views == null)
                {
                    _views = new List<IViewMeta>();
                }
                return _views;
            }
        }
    }
}
