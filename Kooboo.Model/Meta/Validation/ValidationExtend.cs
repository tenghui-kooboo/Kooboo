using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Kooboo.Data.Context;

namespace Kooboo.Model.Meta.Validation
{
    public static class ValidationExtend
    {
        public static bool ValidModel(this object model, Action<string> callback)
        {
            var type = model.GetType();
            var properties = type.GetProperties();
            var isValid = true;

            foreach(var property in properties)
            {
                var value = property.GetValue(model,null);
                var attrs = property.GetCustomAttributes(true)
                    .Where(p=>p is ValidationRule).Select(p =>p as ValidationRule).ToList();

                foreach(var attr in attrs)
                {
                    if (!attr.IsValid(value))
                    {
                        callback?.Invoke(attr.Message);
                        isValid = false;
                    }
                }
            }

            return isValid;
        }
    }
}
