using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Kooboo.Model.Meta.Table
{
    public class Column : INamedMeta
    {
        public string Name { get; set; }

        private Header _header;
        public Header Header
        {
            get
            {
                if (_header == null)
                {
                    _header = new Header(); 
                }
                return _header;
            }
            set
            {
                _header = value;
            }
        }

        public Cell Cell { get; set; }
    }
}
