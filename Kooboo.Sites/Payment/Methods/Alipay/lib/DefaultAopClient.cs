using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Kooboo.Sites.Payment.Methods.Alipay.Models;
using Newtonsoft.Json;

namespace Kooboo.Sites.Payment.Methods.Alipay.lib
{
    public class DefaultAopClient : IAopClient
    {
        public const string APP_ID = "app_id";
        public const string FORMAT = "format";
        public const string METHOD = "method";
        public const string TIMESTAMP = "timestamp";
        public const string VERSION = "version";
        public const string SIGN_TYPE = "sign_type";
        public const string ACCESS_TOKEN = "auth_token";
        public const string SIGN = "sign";
        public const string TERMINAL_TYPE = "terminal_type";
        public const string TERMINAL_INFO = "terminal_info";
        public const string PROD_CODE = "prod_code";
        public const string NOTIFY_URL = "notify_url";
        public const string CHARSET = "charset";
        public const string ENCRYPT_TYPE = "encrypt_type";
        public const string BIZ_CONTENT = "biz_content";
        public const string APP_AUTH_TOKEN = "app_auth_token";
        public const string RETURN_URL = "return_url";
        private readonly string alipayPublicKey;
        private string charset;

        private string format;
        private readonly bool keyFromFile;

        public string notify_url;
        private readonly string privateKeyPem;
        public string return_url;
        private readonly string serverUrl;
        private readonly string signType = "RSA";

        private string version;

        private readonly WebUtils webUtils;

        public string Version
        {
            get => version ?? "1.0";
            set => version = value;
        }

        public string AppId { get; set; }

        public DefaultAopClient(string serverUrl, string appId, string privateKeyPem, string format, string version, string signType, string alipayPulicKey, string charset, bool keyFromFile)
        {
            this.serverUrl = serverUrl;
            AppId = appId;
            this.privateKeyPem = privateKeyPem;
            this.format = format;
            this.version = version;
            this.signType = signType;
            alipayPublicKey = alipayPulicKey;
            this.charset = charset;
            this.keyFromFile = keyFromFile;
            webUtils = new WebUtils();
        }

        public string PageExecute<T>(IAopRequest<T> request, string accessToken, string reqMethod) where T : AopResponse
        {
            if (string.IsNullOrEmpty(charset))
                charset = "utf-8";

            string apiVersion = !string.IsNullOrEmpty(request.GetApiVersion()) ? request.GetApiVersion() : Version;
            var txtParams = new AopDictionary(request.GetParameters());

            // 序列化BizModel
            txtParams = SerializeBizModel(txtParams, request);

            // 添加协议级请求参数
            txtParams.Add(METHOD, request.GetApiName());
            txtParams.Add(VERSION, apiVersion);
            txtParams.Add(APP_ID, AppId);
            txtParams.Add(FORMAT, format);
            txtParams.Add(TIMESTAMP, DateTime.Now);
            txtParams.Add(ACCESS_TOKEN, accessToken);
            txtParams.Add(SIGN_TYPE, signType);
            txtParams.Add(TERMINAL_TYPE, request.GetTerminalType());
            txtParams.Add(TERMINAL_INFO, request.GetTerminalInfo());
            txtParams.Add(PROD_CODE, request.GetProdCode());
            txtParams.Add(NOTIFY_URL, request.GetNotifyUrl());
            txtParams.Add(CHARSET, charset);
            //txtParams.Add(RETURN_URL, this.return_url);
            txtParams.Add(RETURN_URL, request.GetReturnUrl());
            //字典排序
            IDictionary<string, string> sortedTxtParams = new SortedDictionary<string, string>(txtParams);
            txtParams = new AopDictionary(sortedTxtParams)
            {
                // 排序返回字典类型添加签名参数
                { SIGN, AopUtils.SignAopRequest(sortedTxtParams, privateKeyPem, charset, keyFromFile, signType) }
            };

            // 是否需要上传文件
            string body;

            if (request is IAopUploadRequest<T> uRequest)
            {
                var fileParams = AopUtils.CleanupDictionary(uRequest.GetFileParameters());
                body = webUtils.DoPost(serverUrl + "?" + CHARSET + "=" + charset, txtParams, fileParams, charset);
            }
            else
            {
                if (reqMethod.Equals("GET"))
                {
                    //拼接get请求的url
                    var tmpUrl = serverUrl;
                    if (txtParams != null && txtParams.Count > 0)
                        tmpUrl = tmpUrl.Contains("?")
                            ? tmpUrl + "&" + WebUtils.BuildQuery(txtParams, charset)
                            : tmpUrl + "?" + WebUtils.BuildQuery(txtParams, charset);
                    body = tmpUrl;
                }
                else
                {
                    //输出post表单
                    body = BuildHtmlRequest(txtParams, reqMethod, reqMethod);
                }
            }

            return body;
        }

        public string BuildHtmlRequest(IDictionary<string, string> sParaTemp, string strMethod, string strButtonValue)
        {
            //待请求参数数组
            IDictionary<string, string> dicPara = new Dictionary<string, string>();
            dicPara = sParaTemp;

            var sbHtml = new StringBuilder();
            //sbHtml.Append("<head><meta http-equiv=\"Content-Type\" content=\"text/html\" charset= \"" + charset + "\" /></head>");

            sbHtml.Append("<form id='alipaysubmit' name='alipaysubmit' action='" + serverUrl + "?charset=" + charset +
                          "' method='" + strMethod + "'>");
            ;
            foreach (var temp in dicPara)
                sbHtml.Append("<input  name='" + temp.Key + "' value='" + temp.Value + "'/>");

            //submit按钮控件请不要含有name属性
            sbHtml.Append("<input type='submit' value='" + strButtonValue + "' style='display:none;'></form>");
            // sbHtml.Append("<input type='submit' value='" + strButtonValue + "'></form></div>");

            //表单实现自动提交
            sbHtml.Append("<script>document.forms['alipaysubmit'].submit();</script>");

            return sbHtml.ToString();
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestParams"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        private AopDictionary SerializeBizModel<T>(AopDictionary requestParams, IAopRequest<T> request) where T : AopResponse
        {
            var result = requestParams;
            var isBizContentEmpty = !requestParams.ContainsKey(BIZ_CONTENT) || string.IsNullOrEmpty(requestParams[BIZ_CONTENT]);
            if (isBizContentEmpty && request.GetBizModel() != null)
            {
                var bizModel = request.GetBizModel();
                var content = Serialize(bizModel);
                result.Add(BIZ_CONTENT, content);
            }
            return result;
        }

        /// <summary>
        ///     AopObject序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private string Serialize(AopObject obj)
        {
            JsonSerializerSettings jsetting = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            return JsonConvert.SerializeObject(obj, Formatting.None, jsetting);
        }
    }
}
