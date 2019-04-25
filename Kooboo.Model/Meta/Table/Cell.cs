using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta.Table
{
    public abstract class Cell
    {
        protected Cell()
        {
            var typeName = GetType().Name;
            Type = typeName.Substring(0, typeName.Length - 4).ToLower(); 
        }

        protected Cell(string type)
        {
            Type = type;
        }

        public string Type { get; set; }

        public ClassMeta Class { get; set; }

        public string Style { get; set; }

        public Format Text { get; set; }
    }
}
