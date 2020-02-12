using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Kooboo.Sites.Payment.Methods.Alipay.Models
{
    /// <summary>
    /// AlipayTradePagePayModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayTradePagePayModel : AopObject
    {
        /// <summary>
        /// 订单描述
        /// </summary>
        [XmlElement("body")]
        [JsonProperty(PropertyName = "body")]
        public string Body { get; set; }

        /// <summary>
        /// 商户订单号,64个字符以内、可包含字母、数字、下划线；需保证在商户端不重复
        /// </summary>
        [XmlElement("out_trade_no")]
        [JsonProperty(PropertyName = "out_trade_no")]
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 销售产品码，与支付宝签约的产品码名称。  注：目前仅支持FAST_INSTANT_TRADE_PAY
        /// </summary>
        [XmlElement("product_code")]
        [JsonProperty(PropertyName = "product_code")]
        public string ProductCode { get; set; }

        /// <summary>
        /// 商户自定义二维码宽度  注：qr_pay_mode=4时该参数生效
        /// </summary>
        [XmlElement("qrcode_width")]
        [JsonProperty(PropertyName = "qrcode_width")]
        public long QrcodeWidth { get; set; }

        /// <summary>
        /// 订单标题
        /// </summary>
        [XmlElement("subject")]
        [JsonProperty(PropertyName = "subject")]
        public string Subject { get; set; }

        /// <summary>
        /// 订单总金额，单位为元，精确到小数点后两位，取值范围[0.01,100000000]。
        /// </summary>
        [XmlElement("total_amount")]
        [JsonProperty(PropertyName = "total_amount")]
        public string TotalAmount { get; set; }
    }
}
