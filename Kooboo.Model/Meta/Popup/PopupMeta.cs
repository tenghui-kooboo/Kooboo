using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta.Popup
{
    public class PopupMeta : IViewMeta
    {
        public Localizable Title { get; set; }

        public Description Description { get; set; }

        public List<PopupButton> _buttons;
        public List<PopupButton> Buttons
        {
            get
            {
                if (_buttons == null)
                {
                    _buttons = new List<PopupButton>();
                }
                return _buttons;
            }
        }

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

        public string ModelType { get; set; }
    }

    public class PopupButton
    {
        public string Class { get; set; }

        public Localizable Text { get; set; }

        public PageButtonAction Type { get; set; }
    }
    
    public enum PageButtonAction
    {
        Submit=0,
        Close=1
    }
}
