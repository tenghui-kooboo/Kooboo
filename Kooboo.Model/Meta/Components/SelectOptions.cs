using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta
{
    public class SelectOptions
    {
        public List<OptionItem> Items { get; set; }

        public OptionContext Context { get; set; }

        /// <summary>
        /// Get select options from context data
        /// </summary>
        public static SelectOptions UseContext(string dataField, string textField, string valueField = null, OptionContext defaultOption = null)
        {
            return new SelectOptions 
            {
                Context = new OptionContext
                {
                    Data = dataField,
                    Text = textField,
                    Value = valueField,
                    Default=defaultOption
                }
            };
        }

        public static OptionContext UseDefaultOption(string text,string value)
        {
            return new OptionContext()
            {
                Text = "System default",
                Value = value
            };
        }

        public static SelectOptions UseList(params OptionItem[] items)
        {
            return new SelectOptions
            {
                Items = items.ToList()
            };
        }

        public class OptionContext
        {
            public string Data { get; set; }

            public string Text { get; set; }

            public string Value { get; set; }

            public OptionContext Default { get; set; }
        }

        public class OptionItem
        {
            public string Text { get; set; }

            public string Data { get; set; }
        }
    }
}
