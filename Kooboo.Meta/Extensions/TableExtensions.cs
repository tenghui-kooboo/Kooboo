using Kooboo.Meta.Models;
using Kooboo.Meta.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta
{
    public static class TableExtensions
    {

        public static KbTable.Row SetRowTemplate(this KbTable table, Action<KbTable.Row> action)
        {
            table.RowTemplate = new KbTable.Row();
            action(table.RowTemplate);
            return table.RowTemplate;
        }

        public static KbTable SetData(this KbTable table, string id, JsCode source)
        {
            table.AddHook("dataLoad", id, $"k.self.items={source};");
            return table;
        }

        public static KbTable.Cell AddCell(this KbTable.Row row, Action<KbTable.Cell> action)
        {
            var cell = new KbTable.Cell();
            action(cell);
            row.Cells.Add(cell);
            return cell;
        }

        public static KbTable.Cell SetItemTemplate(this KbTable.Cell cell, Action<KbTable.Cell.Template> action)
        {
            cell.ItemTemplate = new KbTable.Cell.Template();
            action(cell.ItemTemplate);
            return cell;
        }
    }
}
