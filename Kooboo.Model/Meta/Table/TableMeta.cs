using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta.Table
{
    public class TableMeta : IViewMeta
    {
        public string ListName { get; set; }

        public string DataApi { get; set; }

        private List<MenuItem> _menu;
        public List<MenuItem> Menu
        {
            get
            {
                if (_menu == null)
                {
                    _menu = new List<Table.MenuItem>();
                }
                return _menu;
            }
        }

        private NamedMetaCollection<Column> _columns;
        public NamedMetaCollection<Column> Columns
        {
            get
            {
                if (_columns == null)
                {
                    _columns = new NamedMetaCollection<Column>();
                }
                return _columns;
            }
        }
    }
}
