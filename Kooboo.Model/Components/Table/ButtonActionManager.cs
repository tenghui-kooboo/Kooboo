using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Components.Table
{
    //todo rewrite
    public class ButtonActionManager
    {
        private static ButtonActionManager _instance;
        private static object _lockObj = new object();
        public static ButtonActionManager Instance
        {
            get
            {

                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                            _instance = new ButtonActionManager();
                    }

                }
                return _instance;
            }
        }
        public List<IAction> Cells { get; set; } = new List<IAction>();
        public ButtonActionManager()
        {
            foreach (var type in Lib.Reflection.AssemblyLoader.LoadTypeByInterface(typeof(IAction)))
            {
                var instance = Activator.CreateInstance(type) as IAction;
                Cells.Add(instance);
            }
        }

        public IAction Get(ButtonActionType actionType)
        {
            var cell = Cells.Find(c => c.ActionType == actionType);

            if (cell != null)
            {
                var instance = Activator.CreateInstance(cell.GetType()) as IAction;

                return instance;
            }

            return null;
        }
    }
}
