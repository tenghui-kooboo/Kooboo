using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.Components.Table;
using Kooboo.Model.Attributes;

namespace Kooboo.Model.Components
{
    public class KBTable : IComponent
    {
        public ComponentType Type => ComponentType.KTable;

        public string ModelName { get; set; }//todo change to name

        public List<TableColumn> Columns { get; set; } = new List<TableColumn>();

        public List<TableColumn> RowActions { get; set; } = new List<TableColumn>();

        public List<IAction> Actions { get; set; } = new List<IAction>();

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

        public bool IsMatch(Attribute attribute)
        {
            return attribute is RowActionAttribute ||
                attribute is ColumnAttribute ||
                attribute is TableActionAttribute;
        }

        public void SetData(List<Dictionary<string, List<Attribute>>> attributes)
        {
            //todo to be optimized
            foreach (var item in attributes)
            {
                if (item.Count > 0)
                {
                    var keyPair = item.First();
                    var fieldName = keyPair.Key;
                    var list = keyPair.Value;

                    var obj = list.Find(l => l is TableActionAttribute);
                    if (obj != null)
                    {
                        var attr = obj as TableActionAttribute;

                        var action = ButtonActionManager.Instance.Get(attr.ActionType);
                        if (action != null)
                        {
                            action.Name = fieldName;
                            action.DisplayName = attr.DisplayName;
                            action.Color = attr.Color;
                            action.SetData(list);
                        }
                        Actions.Add(action);
                        continue;
                    }

                    obj = list.Find(l => (l is ColumnAttribute));
                    if (obj != null)
                    {
                        var attr = obj as ColumnAttribute;
                        var column = new TableColumn();
                        column.FieldName = fieldName;
                        column.DisplayName = attr.DisplayName;

                        column.Cell = CellManager.Instance.Get(attr.CellType);
                        if (column.Cell != null)
                        {
                            column.Cell.CellDataType = attr.CellDataType;
                            column.Cell.ColumnName = fieldName;
                            column.Cell.SetData(list);
                        }
                        Columns.Add(column);
                        continue;
                    }

                    obj = list.Find(l => (l is RowActionAttribute));
                    if (obj != null)
                    {
                        var attr = obj as RowActionAttribute;
                        var column = new TableColumn();
                        column.FieldName = fieldName;
                        column.DisplayName = attr.DisplayName;
                        column.Cell = CellManager.Instance.Get(attr.CellType);
                        if (column.Cell != null)
                        {
                            column.Cell.ColumnName = fieldName;
                            column.Cell.SetData(list);
                        }

                        RowActions.Add(column);
                        continue;
                    }
                }

            }
        }
    }

    public class TableColumn
    {
        public string DisplayName { get; set; }

        public string FieldName { get; set; }

        public ICell Cell { get; set; }

        public object GetData()
        {
            var dic = new Dictionary<string, object>();
            dic.Add("displayName", DisplayName);
            dic.Add("fieldName", FieldName);
            //todo confirm
            var cellType = Cell.CellType.ToString();
            //todo review
            //redundant fields
            dic.Add("type", cellType.Substring(0, 1).ToLower() + cellType.Substring(1));
            dic.Add("dataType", Cell.CellDataType.ToString());
            //dic.Add("cellData",Cell.GetData())
            dic.Add("data", Cell.GetData());

            return dic;
        }

    }

}
