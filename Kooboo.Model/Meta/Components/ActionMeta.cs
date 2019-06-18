using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Kooboo.Model.Meta
{
    public class ActionMeta
    {
        public ActionType Type { get; set; }

        public string Confirm { get; set; }

        public ConditionUrl ConditionUrl { get; set; }

        public string Url { get; set; }

        public object Meta { get; set; }

        public static ActionMeta Post(string url, string confirm = null)
        {
            return new ActionMeta { Type = ActionType.Post, Url = url, Confirm = confirm };
        }

        public static ActionMeta EmbeddedPopup(object meta, string url = "")
        {
            return new ActionMeta { Type = ActionType.Popup, Meta = meta,Url=url };
        }

        public static ActionMeta Popup(string url)
        {
            return new ActionMeta { Type = ActionType.Popup, Url = url };
        }

        public static ActionMeta NewWindow(string url = null)
        {
            return new ActionMeta { Type = ActionType.NewWindow, Url = url };
        }

        public static ActionMeta Redirect(string url = null)
        {
            return new ActionMeta { Type = ActionType.Redirect, Url = url };
        }
    }

    public class ConditionUrl
    {
       public List<ConditionUrlItem> Conditions { get; set; } = new List<ConditionUrlItem>();
       public static ConditionUrlBuild<T> Build<T>()
        {
            return new ConditionUrlBuild<T>()
            {
                ConditionUrl = new ConditionUrl()
            };
        }
    }

    public class ConditionUrlItem
    {
        public string Condition { get; set; }

        public string Url { get; set; }
    }

    public class ConditionUrlBuild<T>
    {
        public ConditionUrl ConditionUrl { get; set; }

        public ConditionUrlBuild<T> If(Expression<Func<T,object>> func, string url)
        {
            ConditionUrl.Conditions.Add(new ConditionUrlItem { Condition=func.Condition(),Url=url});
            return this;
        }

        public ConditionUrlBuild<T> ElseIf(Expression<Func<T, object>> func, string url)
        {
            ConditionUrl.Conditions.Add(new ConditionUrlItem { Condition = func.Condition(), Url = url });
            return this;
        }

        public ConditionUrlBuild<T> Else(string url)
        {
            ConditionUrl.Conditions.Add(new ConditionUrlItem { Condition = "", Url = url });
            return this;
        }

    }

    public enum ActionType
    {
        Redirect,

        NewWindow,

        Popup,

        Post,

        Event
    }
}
