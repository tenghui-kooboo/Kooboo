using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Model.Meta.Definition
{
    public class MenuBtn
    {
        public MenuBtnType Type { get; set; }

        public string DisplayName { get; set; }

        public string Class { get; set; }

        public EnumActionType Action { get; set; }

        public string Url { get; set; }

        public string Popup { get; set; }

        public string Visible { get; set; }

        public VisibleCondition VisibleCondition { get; set; }

        //dropdown
        public string DataUrl { get; set; }

        public List<ModelOptions> Options { get; set; }

        public string IconClass { get; set; }

    }

    public class VisibleCondition
    {
        public VisibleCompare Compare { get; set; }

        public int Value { get; set; }
    }
    public enum VisibleCompare
    {
        eq = 0,
        gt = 1,
        egt = 2,
        lt = 3,
        elt = 4
    }
    public enum MenuBtnType
    {
        Btn = 0,
        Dropdown = 1
    }
}
