using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.Components.Table.Attributes;
using Kooboo.Model.Components.Table;

namespace Kooboo.Model.Setting
{

    [ModelName("View")]
    public class ViewsModel : KoobooSetting, ITable
    {
        [TableAction("Create", "Kooboo.Route.View.Create")]
        [TableAction("Copy")]
        [TableAction("Delete")]
        public string Actions { get; set; }

        [Column("name",CellType.Link)]
        [Url("name", "Kooboo.Route.View.DetailPage","id")]
        public string Name { get; set; }

        [Column("date",CellType.Text,CellDataType.Date)]
        public string LastModified { get; set; }

        [Column("Used by", CellType.Array)]
        public string Relations { get; set; }

        [RowAction("versions", "View all versions", CellType.Button)]
        [Url("versions", "Kooboo.Route.SiteLog.LogVersions", "keyHash", "storeNameHash")]
        public string RowActions { get; set; }
    }
}
