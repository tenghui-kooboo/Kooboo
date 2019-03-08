using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.Components.Table.Attributes;

namespace Kooboo.Model.Components.Table
{
    public class TableModel:IComponent
    {
        public ComponentType Type => ComponentType.KTable;

        public string ModelName { get; set; }

        public List<TableColumn> Columns { get; set; } = new List<TableColumn>();

        public bool Selectable { get; set; } = true;

        public List<TableAction> Actions { get; set; } = new List<TableAction>();

        public List<TableRowAction> RowActions { get; set; } = new List<TableRowAction>();

        //todo need review this setting
        public List<UrlAttribute> UrlColumns { get; set; } = new List<UrlAttribute>();

        public List<TableRowRelation> RelationColumns
        {
            get
            {
                var columns = Columns.FindAll(c => c.CellType == CellType.Array).ToList();

                var list = new List<TableRowRelation>();
                foreach(var col in columns)
                {
                    list.Add(new TableRowRelation()
                    {
                        FieldName=col.FieldName,
                        Config = new object(),
                        IsShow=false
                    });
                }
                return list;
            }
        }

        public VueField GetField()
        {
            var tableData = new VueField();
            tableData.Name = "tableData";

            //temp to put table fields in tableData
            //todo confirm
            var dic = new Dictionary<string,object>();
            dic.Add("modelName", ModelName);
            dic.Add("docs", new List<string>());
            dic.Add("cols", GetColumnsData());
            dic.Add("selectable", Selectable);

            dic.Add("rowActions", GetRowActions());
            dic.Add("actions", Actions);
            
            //url for link
            dic.Add("urls", UrlColumns);

            dic.Add("relations", RelationColumns);

            tableData.Value = dic;

            return tableData;
        }

        //public List<VueField> DoGetTableFields()
        //{
        //    var list = new List<VueField>();
        //    list.Add(new VueField()
        //    {
        //        Name = "actions",
        //        Value =GetActionsData()
        //    });

        //    list.Add(new VueField()
        //    {
        //        Name="cols",
        //        Value=GetColumsData()
        //    });

        //    list.Add(new VueField()
        //    {
        //        Name = "rowActions",
        //        Value = GetRowActions()
        //    });

        //    list.Add(new VueField()
        //    {
        //        Name = "docs",
        //        Value = "[]"
        //    });
        //    return list;
        //}
        //private string GetActionsData()
        //{
        //    return Kooboo.Lib.Helper.JsonHelper.Serialize(Actions);
        //}

        private List<Dictionary<string, string>> GetColumnsData()
        {
            var list = new List<Dictionary<string, string>>();
            foreach(var col in Columns)
            {
                var dic = new Dictionary<string, string>();
                dic.Add("displayName", col.DisplayName);
                dic.Add("fieldName", col.FieldName);
                //todo confirm
                var cellType = col.CellType.ToString();
                //todo review
                dic.Add("type", cellType.Substring(0,1).ToLower()+ cellType.Substring(1));
                dic.Add("dataType", col.CellDataType.ToString());

                list.Add(dic);
            }

            return list;
        }

        public List<Dictionary<string, string>> GetRowActions()
        {
            var list = new List<Dictionary<string, string>>();
            foreach (var action in RowActions)
            {
                var dic = new Dictionary<string, string>();
                dic.Add("fieldName", action.FieldName);
                dic.Add("displayName", action.DisplayName);
                var cellType = action.CellType.ToString();
                dic.Add("type", cellType.Substring(0, 1).ToLower() + cellType.Substring(1));

                list.Add(dic);
            }
            return list;
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }

    public class TableAction
    {
        public string ActionName { get; set; }
        public string DisplayName { get; set; }
        public ActionType ActionType { get; set; }
        public string Url { get; set; }
    }

    public class TableColumn
    {
        public string DisplayName { get; set; }

        public string FieldName { get; set; }

        public CellType CellType { get; set; }

        public CellDataType CellDataType { get; set; }

    }

    public class TableRowAction
    {
        public string FieldName { get; set; }

        public string DisplayName { get; set; }

        public CellType CellType { get; set; }
    }

    public class TableRowRelation
    {
        public object Config { get; set; }

        public bool IsShow { get; set; }

        public string FieldName { get; set; }
    }

    public enum ActionType
    {
        Link,
        Modal
    }


}
