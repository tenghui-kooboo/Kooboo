using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Model.Meta.Tab
{
    public class TabMeta : IViewMeta
    {
        public string ModelType { get; set; }

        public List<Tab> Tabs { get; set; } = new List<Tab>();

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

    public class Tab
    {
        public Localizable Text { get; set; }

        public string DataApi { get; set; }

        public IViewMeta View { get; set; }
    }
}
