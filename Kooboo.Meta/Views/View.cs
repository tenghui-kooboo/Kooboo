using Kooboo.Meta.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta.Views
{
    public abstract class View
    {
        public abstract string Name { get; }

        public string Id { get; } = Guid.NewGuid().ToString();

        public List<Hook> Hooks { get; set; } = new List<Hook>();
    }
}
