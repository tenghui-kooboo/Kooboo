using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Model.Meta.Definition
{
  public   class KTable
    {
        public List<Column> Columns { get; set; } = new List<Column>(); 

    }

    public class KTableMeta : IKMeta
    {
        public List<Dictionary<string, object>> Menu { get; set; }

        public KTable Table { get; set; }
    }

  //  var meta = {
  //columns: [
  //  {
  //     name: "online",//column name
  //    header: { //table header
  //      displayName: "在线情况",//header displayName
  //      class: "",//header class
  //      style: ""//header style
  //    },
  //    cell: {
  //      type: "link",//cell type
  //      style: "",//cell style
  //      class: "",//cell class
  //      action: "newWindow",//cell action
  //      url: "/layout/detail?id={id}" //cell url
  //    }
  //  },
  //]


}
