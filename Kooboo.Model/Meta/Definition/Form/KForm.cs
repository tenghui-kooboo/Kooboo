using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Kooboo.Model.Meta.Definition
{
    public class KForm
    {
        public string LoadApi { get; set; }

        public string SubmitApi { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public FormLayout Layout { get; set; }

        public List<Dictionary<string,object>> Items { get; set; }

        //{
        //  title:"title",
        //  layout:'horizontal',//值为horizontal|vertical|inline
        //  items:[
        //    { type: "checkbox" },
        //    { type: "datetime" },
        //    { type: "number" },
        //    { type: "radiobox" },
        //    { type: "richeditor" },
        //    { type: "selection" },
        //    { type: "switch" },
        //    { type: "textarea" },
        //    { type: "textbox" }
        //]};
    }

    public enum FormLayout
    {
        Horizontal=0,
        Vertical=1,
        Inline=2
    }
}
