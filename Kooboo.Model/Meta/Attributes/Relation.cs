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

        public Type Model { get; set; }

        public string LoadDataApi { get; set; }

        public string SubmitApi { get; set; }

        public RelationAttribute(Type model,string apiOrModel,string submitApi)
        {
            Model = model;
            LoadDataApi = apiOrModel;
            SubmitApi = submitApi;
        }
    }
}
