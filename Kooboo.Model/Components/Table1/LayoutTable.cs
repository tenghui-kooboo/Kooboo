using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Data.Language;
using Kooboo.Data.Context;

namespace Kooboo.Model.Components.Table
{
    public  class LayoutTable:ITableComponents
    {
        public string Name => "Layout";

        public List<TableAction> Actions { get; set; }

        public List<TableColumn> Columns { get; set; }

        public List<TableRowAction> RowActions { get; set; }

        public bool Selectable { get; set; }

        public string DeleteEvent { get; set; }

        public RenderContext Context { get; set; }

        public LayoutTable()
        {
            Actions = new List<TableAction>()
            {
                new TableAction()
                {
                    Text =Hardcoded.GetValue("Create",Context),
                    ClassName="green",
                    IsShow=true,
                    ClickEvent="goToCreateUrl"
                },
                new TableAction()
                {
                    Text =Hardcoded.GetValue("Copy",Context),
                    ClassName="green",
                    ShowCondition="selectedDocs.length == 1",
                    ClickEvent="showCopyModal"
                },
                new TableAction()
                {
                    Text=Hardcoded.GetValue("Copy",Context),
                    ClassName="red",
                    ShowCondition="selectedDocs.length>0",
                    ClickEvent="delete"
                }
            };

            Columns = new List<TableColumn>()
            {
                new TableColumn()
                {
                    DisplayName=Hardcoded.GetValue("name",Context),
                    FieldName="name",
                    CellType=new LinkCell("{modeldata.name}",RouteHelper.GetUrl(Context,"Development/Layout","id={modeldata.id}"))
                },
                new TableColumn()
                {
                    DisplayName=Hardcoded.GetValue("Used by",Context),
                    FieldName="relations",
                    //后台计算or 前台计算？
                    //render by background
                    //render by fontend

                    CellType=new ArrayCell("{modeldata.relations}",this.Name)
                },
                new TableColumn()
                {
                    DisplayName=Hardcoded.GetValue("Last modified",Context),
                    FieldName="date",
                    CellType=new TextCell("{modeldata.date}")
                }

            };

            RowActions = new List<TableRowAction>()
            {
                new TableRowAction()
                {
                    FieldName="versions",
                    CellType=new ButtonCell("",Hardcoded.GetValue("View all versions",Context),
                        RouteHelper.GetUrl(Context,"System/SiteLog/LogVersions","KeyHash={modeldata.keyHash}&storeNameHash={date.storeNameHash}"),
                        "btn-xs blue","fa-clock-o",true)
                }

            };

            Selectable = true;

        }

    }
}
