using System;
using System.Collections.Generic;
using System.Text;
using Kooboo.Model.Meta.Definition;

namespace Kooboo.Model.Meta.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CellSummaryData : Attribute, IMetaAttribute
    {

        public bool IsHeader => false;

        public string PropertyName => "data";
        public string[] Data { get; set; }

        public CellSummaryData(params string[] data)
        {
            this.Data = data;
        }

        public object Value()
        {
            var sb = new StringBuilder();
            var list = new List<string>();
            for(var i = 0; i < Data.Length; i++)
            {
                list.Add(Data[i]);
                
            }
            return list;
        }
    }
}
