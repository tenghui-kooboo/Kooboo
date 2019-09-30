using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta.Views
{
    public class KbMeta : KbContainer
    {
        public new class Hook : KbView.Hook
        {
            public static string DataLoad(string id) => $"{nameof(DataLoad)}_{id}";
        }

        string _name = null;

        public override string Name => _name;

        public string SetRoute(string name)
        {
            _name = name;
            return name;
        }

    }
}
