using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Data.Language;

namespace Kooboo.Model.Meta.Validation
{
    public class SameAsRule:ValidationRule
    {
        private string _sameAsField;
        public SameAsRule(string field,string message="")
        {
            _sameAsField = field;
            Message = message;
        }

        private string _message;
        public override string Message
        {
            get
            {
                _message = string.IsNullOrEmpty(_message)
                   ? string.Format(Hardcoded.GetValue("not same as {0}", Context), _sameAsField)
                    : string.Format(Hardcoded.GetValue(_message, Context), _sameAsField);

                return _message;
            }
            set
            {
                _message = value;
            }
        }

        public override string GetRule()
        {
            return string.Format("{{type:\"sameAs\",field:\"{1}\",message:\"{0}\"}}", Message, _sameAsField);
        }

        public override bool IsValid(object value)
        {
            var type = Model.GetType();
            var properties = type.GetProperties().ToList();

            var sameAsProperty = properties.Find(p => p.Name == _sameAsField);
            var property = properties.Find(p => p.Name == Field);
            if (sameAsProperty == null || property == null)
                return false;

            if (property.PropertyType != sameAsProperty.PropertyType || property.PropertyType != typeof(string))
                return false;

            var sameAsValue = sameAsProperty.GetValue(Model, null);

            //only compare string
            return sameAsValue == value;
        }

    }
}
