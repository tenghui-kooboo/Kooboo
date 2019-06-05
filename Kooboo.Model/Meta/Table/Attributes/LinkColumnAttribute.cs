using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta.Table
{
    public class LinkColumnAttribute : ColumnAttribute
    {
        public LinkColumnAttribute(string header, ActionType linkType, string url)
            : base(header, new LinkCell { Action = new ActionMeta { Type = linkType, Url = url } })
        {
        }

        public LinkColumnAttribute(ActionType linkType, string url)
            : this(null, linkType, url)
        {
        }
    }
}
