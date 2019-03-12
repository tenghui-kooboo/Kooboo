using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model.Components.Table
{
    ////todo rewrite
    public class CellManager
    {
        private static CellManager _instance;
        private static object _lockObj = new object();
        public static CellManager Instance
        {
            get
            {

                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                            _instance = new CellManager();
                    }

                }
                return _instance;
            }
        }
        public List<ICell> Cells { get; set; } = new List<ICell>();
        public CellManager()
        {
            foreach (var type in Lib.Reflection.AssemblyLoader.LoadTypeByInterface(typeof(ICell)))
            {
                var instance = Activator.CreateInstance(type) as ICell;
                Cells.Add(instance);
            }
        }

        public ICell Get(CellType cellType)
        {
            var cell = Cells.Find(c => c.CellType == cellType);

            if (cell != null)
            {
                var instance = Activator.CreateInstance(cell.GetType()) as ICell;

                return instance;
            }

            return null;
        }
    }
}
