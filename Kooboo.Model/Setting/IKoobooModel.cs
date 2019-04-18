using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Model
{
    public interface IKoobooModel
    {
        ModelType ModelType { get; }

        string ModelName { get; }
    }
    public enum ModelType
    {
        KTable,
        KForm,
        KMenu
    }
}
