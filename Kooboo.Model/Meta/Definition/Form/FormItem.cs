using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Kooboo.Model.Meta.Definition
{
    public class FormItem
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public FormItemType Type { get; set; }

        public string Label { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public string PlaceHolder { get; set; }

        public string Class { get; set; }

        public string ToolTip { get; set; }

        //Selection,checkbox,radiobox
        public string DataUrl { get; set; }

        public List<ModelOptions> Options { get; set; }

        //upload
        public bool AllowMultiple { get; set; }

        public string Accept { get; set; }

        public string OptionConfig { get; set; }

        //   {
        //  type: "textbox",//component type
        //  label: "path",//component label
        //  name: "path",//field name
        //  value: "",//default field value
        //  placeholder: "input here",//textbox placeholder
        //  class: "input-small",
        //  tooltip: ""//tooltip
        //}

        //Selection,checkbox,radiobox
        //dataUrl:"/get/options?id={id}",
        //options:[{//component option data
        //displayName:'A',
        //value:'a'
        //},{
        //displayName:'B',
        //value:'b'
        //},{
        //displayName:'C',
        //value:'c'
        //}],

        //upload
        //allow_multiple:false,//whethod allow upload 
        //              //multi files
        //accept:'.jpg, .png, .bmp, .gif',//upload file type
    }

    public enum FormItemType
    {
        checkbox=0,
        datetime=1,
        number=2,
        radiobox=3,
        richeditor=4,
        selection=5,
        switchType=6,
        textarea=7,
        textbox=8,
        upload=9
    }
}
