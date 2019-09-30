using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta.Models
{
    public class KbHook
    {
        public string Name { get; set; }

        /// <summary>
        /// 一段js代码块，此代码块会在钩子触发时执行,并且可通过k对象来获取上下文，关于k对象的详情可见文档
        /// 
        /// 例：
        /// console.log(k.self)  //在控制台打印出处理钩子的dom对象
        /// console.log(k.target)  //在控制台打印出触发钩子的dom对象
        /// k.self.style.color="red" //在监听到钩子触发时将自身的文字颜色设置为红色
        /// 
        /// </summary>
        public string Execute { get; set; }
    }
}
