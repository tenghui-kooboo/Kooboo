using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.Components.Table;

namespace Kooboo.Model.Components
{
    public class KBTable : IComponent
    {
        public ComponentType Type => ComponentType.KTable;

        public string ModelName { get; set; }//todo change to name

        public List<TableColumn> Columns { get; set; } = new List<TableColumn>();

        public List<TableColumn> RowActions { get; set; } = new List<TableColumn>();

        public List<KBTableActionBtn> Actions { get; set; } = new List<KBTableActionBtn>();

        public bool Selectable { get; set; } = true;

        public VueField GetField()
        {
            var field = new VueField();
            field.Name = "tableData";
            var dic = new Dictionary<string, object>();
            dic.Add("modelName", ModelName);
            dic.Add("docs", new List<string>());
            dic.Add("selectable", Selectable);
            
            dic.Add("cols", GetCols());
            dic.Add("rowActions", GetRowCols());
            dic.Add("actions", GetActions());
            
            field.Value = dic;

            return field;
        }
        private List<object> GetCols()
        {
            var cols = new List<object>();
            foreach (var col in Columns)
            {
                cols.Add(col.GetData());
            }
            return cols;
        }
        private List<object> GetRowCols()
        {
            var rowCols = new List<object>();
            foreach (var col in RowActions)
            {
                rowCols.Add(col.GetData());
            }
            return rowCols;
        }
        private List<object> GetActions()
        {
            var actions = new List<object>();
            foreach (var action in Actions)
            {
                var actionField = action.GetField();
                actions.Add(actionField.Value);
            }
            return actions;

        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }

    public class TableColumn
    {
        public string DisplayName { get; set; }

        public string FieldName { get; set; }

        public ICell Cell { get; set; }

        public object GetData()
        {
            var dic = new Dictionary<string, string>();
            dic.Add("displayName", DisplayName);
            dic.Add("fieldName", FieldName);
            //todo confirm
            var cellType = Cell.CellType.ToString();
            //todo review
            dic.Add("type", cellType.Substring(0, 1).ToLower() + cellType.Substring(1));
            dic.Add("dataType", Cell.CellDataType.ToString());
            //dic.Add("cellData",Cell.GetData())

            return dic;
        }

    }

}
