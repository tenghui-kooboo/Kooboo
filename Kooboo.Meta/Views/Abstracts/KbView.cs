using Kooboo.Meta.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta.Views.Abstracts
{
    public abstract class KbView
    {
        public abstract string Name { get; }

        public virtual string Id { get; set; } = $"_{Guid.NewGuid().ToString("N")}";

        public List<KbHook> Hooks { get; private set; } = new List<KbHook>();
    }
}
