using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Kooboo.Model.Meta.Validation
{
    public abstract class ValidationRule : Attribute
    {
        protected ValidationRule(string type)
        {
            Type = type;
        }

        protected ValidationRule()
        {
            var typeName = GetType().Name;
            Type = typeName.Substring(typeName.Length - 4).ToLower();
        }
        [JsonIgnore]
        public override object TypeId { get; }

        public string Type { get; set; }

        public Localizable Message { get; set; }
    }

    public static class ValidationRuleExtension
    {
        public static string ToJson(this ValidationRule rule)
        {
            var dic = new Dictionary<string, string>();
            dic.Add("type", rule.Type);
            dic.Add("message", rule.Message);

            return Kooboo.Lib.Helper.JsonHelper.Serialize(dic);
        }
    }
}
