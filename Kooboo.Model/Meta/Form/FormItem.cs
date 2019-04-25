using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Kooboo.Model.Meta.Form
{
    public class FormItem : INamedMeta
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string Label { get; set; }

        public string Placeholder { get; set; }

        public string Class { get; set; }

        public string Tooltip { get; set; }

        private List<Validation.ValidationRule> _rules;
        public List<Validation.ValidationRule> Rules
        {
            get
            {
                if (_rules == null)
                {
                    _rules = new List<Validation.ValidationRule>();
                }
                return _rules;
            }
        }
    }
}
