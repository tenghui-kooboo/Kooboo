using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Kooboo.Data.Attributes;
using Kooboo.Data.Context;
using Kooboo.Sites.Payment.Methods.Alipay.lib;
using Kooboo.Sites.Payment.Methods.Alipay.Models;
using Kooboo.Sites.Payment.Response;

namespace Kooboo.Sites.Payment.Methods
{
    public class AlipayForm : IPaymentMethod<AlipayFormSetting>
    {
        public AlipayFormSetting Setting { get; set; }

        string IPaymentMethod.Name { get => "AlipayForm"; }

        string IPaymentMethod.DisplayName
        {
            get => Data.Language.Hardcoded.GetValue("Alipay");
        }

        public string Icon { get => "/_Admin/View/Market/Images/payment-alipay.jpg"; }

        public string IconType => "img";

        public List<string> supportedCurrency
        {
            get
            {
                var list = new List<string>();
                list.Add("CNY");
                list.Add("GBP");
                list.Add("EUR");
                list.Add("USD");
                list.Add("AUD");
                list.Add("CAD");
                list.Add("HKD");
                list.Add("SGD");
                list.Add("JPY");
                return list;
            }
        }

        public RenderContext Context { get; set; }

        [Description(@"<script engine='kscript'>
    var charge = {};
    charge.total = 1.50; 
charge.currency='USD';
charge.name = 'green tea order'; 
charge.description = 'The best tea from Xiamen';  
var resForm = k.payment.alipayForm.charge(charge); 
</script>
 
<div class='jumbotron'>
	<div k-content='resForm.html'></div>
</div>'")]
        [KDefineType(Return = typeof(HiddenFormResponse))]
        public IPaymentResponse Charge(PaymentRequest request)
        {
            HiddenFormResponse res = new HiddenFormResponse();

            DefaultAopClient client = new DefaultAopClient(Config.gatewayUrl, Config.app_id, Config.private_key, "json", "1.0", Config.sign_type, Config.alipay_public_key, Config.charset, false);

            AlipayTradePagePayModel model = new AlipayTradePagePayModel();
            model.Body = request.Description;
            model.Subject = request.Name;
            model.TotalAmount = request.TotalAmount.ToString();
            model.OutTradeNo = request.Id.ToString("N");
            model.ProductCode = "FAST_INSTANT_TRADE_PAY";

            AlipayTradePagePayRequest alipayTradePagePayRequestRequest = new AlipayTradePagePayRequest();
            alipayTradePagePayRequestRequest.SetReturnUrl("");
            alipayTradePagePayRequestRequest.SetNotifyUrl("");
            alipayTradePagePayRequestRequest.SetBizModel(model);

            res.html = client.PageExecute(alipayTradePagePayRequestRequest, null, "post");

            return res;
        }

        public PaymentStatusResponse checkStatus(PaymentRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
