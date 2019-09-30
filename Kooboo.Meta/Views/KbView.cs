using Kooboo.Meta.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta.Views
{
    public abstract class KbView
    {
        public abstract string Name { get; }

        public string Id { get; } = Guid.NewGuid().ToString();

        public List<KbHook> Hooks { get; set; } = new List<KbHook>();
    }
}
