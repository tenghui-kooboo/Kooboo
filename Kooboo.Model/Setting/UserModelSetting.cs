using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.ValidateRules;

namespace Kooboo.Model.Setting
{
    public class UserModelSetting: RuleSetting
    {
        [RequireRule("username can't be empty")]
        [MaxLengthRule(5,"username max Length is {0}")]
        public string UserName { get; set; }

        [RequireRule("password can't be empty")]
        public string Password { get; set; }

        public bool Remember { get; set; }

        public string Returnurl { get; set; }


    }
}
