﻿using Kooboo.Data.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Sites.Payment.Methods.UnionPay
{
    public class UnionPaySetting : IPaymentSetting
    {
        public string Name => "UnionPay";

        public string FrontTransactionUrl { get; set; }

        /// <summary>
        /// 前台通知地址  need kooboo create a page to show pay result
        /// </summary>
        public string FrontUrl { get; set; }

        /// <summary>
        /// 后台通知地址
        /// </summary>
        public string BackUrl { get; set; }

        public string MerchantID { get; set; }

        public SettingFile SignCertPFX { get; set; }

        public string SignCertPasswrod { get; set; }
    }
}