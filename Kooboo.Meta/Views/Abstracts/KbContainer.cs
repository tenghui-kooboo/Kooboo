using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta.Views.Abstracts
{
    public abstract  class KbContainer : KbView
    {
        public List<KbView> Views { get; set; } = new List<KbView>();
    }
}
