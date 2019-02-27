using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Components.Table.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public class ColumnAttribute : Attribute
    {
        public string DisplayName { get; set; }

        public CellType CellType { get; set; }

        public CellDataType CellDataType { get; set; }

        public ColumnAttribute(string displayName, CellType cellType, CellDataType cellDataType=CellDataType.Text)
        {
            DisplayName = displayName;
            CellType = cellType;
            CellDataType = cellDataType;
        }
    }

    public class RowActionAttribute : Attribute
    {
        public string FieldName { get; set; }
        public string DisplayName { get; set; }

        public CellType CellType { get; set; }

        public RowActionAttribute(string fieldName,string displayName, CellType cellType)
        {
            FieldName = fieldName;
            DisplayName = displayName;
            CellType = cellType;
        }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public class TableActionAttribute : Attribute
    {
        public string DisplayName { get; set; }

        public string Url { get; set; }
        public TableActionAttribute(string displayName,string url="")
        {
            DisplayName = displayName;
            Url = url;
        }
    }

    public class UrlAttribute:Attribute
    {
        [Newtonsoft.Json.JsonIgnore]
        public override object TypeId { get; }
        public string FieldName { get; set; }
        public string Url { get; set; }
        public string[] Paras { get; set; }
        public UrlAttribute(string name,string url,params string[] paras)
        {
            FieldName = name;
            Url = url;
            Paras = paras;
        }
    }

}
