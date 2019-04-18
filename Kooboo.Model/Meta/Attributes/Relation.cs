using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Model.Meta.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RelationAttribute : Attribute
    {
        public bool IsHeader => false;

        public string PropertyName => "relation";

        public Type MenuModel { get; set; }

        public string Api { get; set; }

        public RelationAttribute(Type menuModel,string api)
        {
            MenuModel = menuModel;
            Api = api;
        }
    }
}
