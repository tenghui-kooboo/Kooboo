using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.Components.Table;
using Kooboo.Model.Attributes;
using Kooboo.Model.Components;

namespace Kooboo.Model.Setting
{
    //config data setting by model
    [ModelName("Layout")]
    [Title("Layouts")]
    public class LayoutModel: KoobooSetting, IKoobooModel
    {
        //[TableAction("Create",ButtonActionType.Link)]
        //[Link("Kooboo.Route.Layout.Create", null, null)]
        //[TableAction("Copy",ButtonActionType.Modal)]
        //[Modal("copy",null,null)]
        //[TableAction("Delete",ButtonActionType.None)]
        //public string Actions { get; set; }

        [BreadCrumb("sites", "/_Admin/Sites")]
        [BreadCrumb("dashboard", "/_Admin/Site")]
        [BreadCrumb("Layouts", "")]
        public string BreadCrumbs { get; set; }

        [TableAction("Create", ButtonActionType.Link,KBColor.Green)]
        [Link("/Development/Layout/Create", null, null)]
        public string Create { get; set; }

        [TableAction("Copy", ButtonActionType.Modal,KBColor.Green)]
        [Modal("copy", null, null)]
        public string Copy { get; set; }

        [TableAction("Delete", ButtonActionType.None,KBColor.Red)]
        public string Delete { get; set; }


        [Column("Name", CellType.Link)]
        [Link("name", "/Development/Layout/Layout", "id")]
        public string Name { get; set; }

        [Column("Used by", CellType.Array)]
        [Modal("relation",null,null)]
        public string Relations { get; set; }

        [Column("Last modified", CellType.Text,CellDataType.Date)]
        public DateTime LastModified { get; set; }

        [RowAction("versions","View all versions", CellType.Button)]
        [Link("versions","Kooboo.Route.SiteLog.LogVersions", "keyHash", "storeNameHash")]
        public string Versions { get; set; }

    }

}
