using System;
using System.Collections.Generic;
using System.Text;
using Kooboo.Sites.Payment.Methods.Alipay.Models;

namespace Kooboo.Sites.Payment.Methods.Alipay.lib
{
    /// <summary>
    /// AOP客户端。
    /// </summary>
    public interface IAopClient
    {
        /// <summary>
        /// 执行AOP隐私API请求。
        /// </summary>
        /// <typeparam name="T">领域对象</typeparam>
        /// <param name="request">具体的AOP API请求</param>
        /// <param name="session">用户会话码(accessToken)</param>
        /// <param name="reqMethod"></param>
        /// <returns>领域对象</returns>
        string PageExecute<T>(IAopRequest<T> request, string session, string reqMethod) where T : AopResponse;
    }
}
