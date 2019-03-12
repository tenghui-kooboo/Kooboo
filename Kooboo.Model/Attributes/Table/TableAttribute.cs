using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.Components.Table;
using Kooboo.Model.Components;

namespace Kooboo.Model
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public class TableActionAttribute : Attribute
    {
        public string DisplayName { get; set; }

        public ButtonActionType ActionType { get; set; }

        public string Color { get; set; }

        public TableActionAttribute(string displayName,ButtonActionType actionType,string color)
        {
            DisplayName = displayName;
            ActionType = actionType;
            Color = color;
        }
    }

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

}
