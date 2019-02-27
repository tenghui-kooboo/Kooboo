using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.Components.Table.Attributes;
using Kooboo.Model.Components.Table;

namespace Kooboo.Model.Setting
{
    //config data setting by model
    [ModelName("Layout")]
    public class LayoutModel: KoobooSetting, ITable
    {
        [TableAction("Create", "Kooboo.Route.Layout.Create")]
        [TableAction("Copy")]
        [TableAction("Delete")]
        public string Actions { get; set; }

        [Column("Name", CellType.Link)]
        [Url("name","Kooboo.Route.Layout.DetailPage","id")]
        public string Name { get; set; }

        [Column("Used by", CellType.Array)]
        public string Relations { get; set; }

        [Column("Last modified", CellType.Text,CellDataType.Date)]
        public DateTime LastModified { get; set; }

        [RowAction("versions","View all versions", CellType.Button)]
        [Url("versions","Kooboo.Route.SiteLog.LogVersions", "keyHash", "storeNameHash")]
        public string RowActions { get; set; }

    }

}
