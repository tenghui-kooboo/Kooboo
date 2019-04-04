using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Model.ValidateRules;
using Kooboo.Data;

namespace Kooboo.Model.Setting
{
    public class UserModelSetting: IKoobooModel
    {
        [RequireRule("username can't be empty")]
        [MaxLengthRule(50,"username max Length is {0}")]
        [MinLengthRule(3,"username min length is {0}")]
        public string UserName { get; set; }

        [RequireRule("password can't be empty")]
        [MaxLengthRule(20, "password max length is {0}")]
        public string Password { get; set; }

        public bool Remember { get; set; }

        public string Returnurl { get; set; }


    }
}
