using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Data.Context;

namespace Kooboo.Model.Components.Table
{
    public interface ITableComponents
    {
        string Name { get;  }

        List<TableAction> Actions { get; set; }

        List<TableColumn> Columns { get; set; }

        //List<TableDoc> Docs { get; set; }
        
        List<TableRowAction> RowActions { get; set; }

        bool Selectable { get; set; }

        string DeleteEvent { get; set; }

        RenderContext Context { get; set; }

    }

    public class TableAction
    {
        public string Text { get; set; }

        public string ClassName { get; set; }

        public bool IsShow { get; set; }

        public string ShowCondition { get; set; }

        public string ClickEvent { get; set; }
        //url can also use clickEvent
    }

    public class TableColumn
    {
        public string DisplayName { get; set; }
        
        public string FieldName { get; set; }

        //type
        public ICellType CellType { get; set; }

        public string Class { get; set; }

    }
    
    //data from api
    //public class TableDoc
    //{
    //    public string Id { get; set; }
    //}

    public class TableRowAction
    {
        public string FieldName { get; set; }

        public ICellType CellType { get; set; }
    }

   

    //api-->data-->columns-->docs,and data
    //public 

}
