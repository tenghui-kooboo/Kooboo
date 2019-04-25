using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Meta
{
    public class ActionMeta
    {
        public ActionType Type { get; set; }

        public string Confirm { get; set; }

        public string Uri { get; set; }

        public static ActionMeta Post(string url, string confirm = null)
        {
            return new ActionMeta { Type = ActionType.Post, Uri = url, Confirm = confirm };
        }

        public static ActionMeta Popup(string url)
        {
            return new ActionMeta { Type = ActionType.Popup, Uri = url };
        }

        public static ActionMeta NewWindow(string url = null)
        {
            return new ActionMeta { Type = ActionType.NewWindow, Uri = url };
        }

        public static ActionMeta Redirect(string url = null)
        {
            return new ActionMeta { Type = ActionType.Redirect, Uri = url };
        }
    }

    public enum ActionType
    {
        Redirect,

        NewWindow,

        Popup,

        Post
    }
}
