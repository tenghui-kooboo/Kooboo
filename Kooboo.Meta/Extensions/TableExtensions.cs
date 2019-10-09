using Kooboo.Meta.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta
{
    public static class TableExtensions
    {
        public static KbTable.Column AddColumn(this KbTable table, Action<KbTable.Column> action)
        {
            var column = new KbTable.Column();
            action(column);
            table.Columns.Add(column);
            return column;
        }
    }

    public static class TableColumnExtensions
    {
        public static KbTable.Column.Template AddCellTemplate(this KbTable.Column column, Action<KbTable.Column.Template> action)
        {
            var template = new KbTable.Column.Template();
            action(template);
            column.CellTemplate = template;
            return template;
        }
    }
}
