using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta.Models
{
    public class KbHook
    {
        KbHook() { }

        public KbHook(string name, JsCode execute)
        {
            Name = name;
            Execute = execute.ToString();
        }

        public KbHook(Action<KbHook> action)
        {
            action(this);
        }

        public string Name { get; private set; }

        public string Execute { get; private set; }
    }
}
