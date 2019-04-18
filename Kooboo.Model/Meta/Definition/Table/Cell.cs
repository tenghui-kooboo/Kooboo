using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Model.Meta.Definition
{
  public  class Cell
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public EnumCellType Type { get; set; }  // Link, Text... 
         
        public string Style { get; set; }

        public string Class { get; set; }

        //for label
        public string Text { get; set; }

        //summary cell
        List<string> Data { get; set; }

        //Badage 
        public CellCondition Condition { get; set; }

        //btn icon
        public string IconClass { get; set; }
        //btn icon
        public string InnerText { get; set; }

        public EnumActionType Action { get; set; } 

        public string Url { get; set; }
        //    cell: {
        //    type: "link",//cell type
        //    style: "",//cell style
        //    class: "",//cell class
        //    action: "newWindow",//cell action
        //    url: "/layout/detail?id={id}" //cell url
        //    } 

        //condition: {//condition setting
        //  class: {
        //    true: "green",//if the column value is true,
        //                  //it will add this class
        //    false: "red" //if the column value is false,
        //                //it will add this class
        //  },
        //  visible: true//if this is true,it will show badage
        //                //otherwise it will not
        //},
    }

    public class CellCondition
    {
        public Dictionary<bool,string> Class { get; set; }

        public bool Visible { get; set; }
    }
    public enum EnumCellType
    {
        Text = 0, 
        Link = 1,
        Badge=2,
        Label=7,
        Array=3,
        Icon=4,
        Summary=5,
        Button=6,
        Date=8
    }

    public enum EnumActionType
    {
        NewWindow = 0,
        Redirect = 1,
        Popup=2,
        Event=3,
        Post=4,
        //Ajax = 2
    } 
}
