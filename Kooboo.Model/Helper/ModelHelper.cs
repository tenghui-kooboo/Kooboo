using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model
{
    public static class StringBuilderExtension
    {
        public static StringBuilder AppendTabs(this StringBuilder builder,int n,string content)
        {
            builder.Append(new String(' ', n*3));
            builder.AppendLine(content);
            return builder;
        }
    }
}
