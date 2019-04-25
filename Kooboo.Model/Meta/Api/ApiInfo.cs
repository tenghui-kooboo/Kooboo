using System;
using System.Collections.Generic;

namespace Kooboo.Model.Meta.Api
{
    public class ApiInfo
    {
        public ModelInfo Result { get; set; }

        public ModelInfo Model { get; set; }

        private List<PropertyInfo> _parameters;
        public List<PropertyInfo> Parameters
        {
            get
            {
                if (_parameters == null)
                {
                    _parameters = new List<PropertyInfo>();
                }
                return _parameters;
            }
            set
            {
                _parameters = value;
            }
        }
    }
}
