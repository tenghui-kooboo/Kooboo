using Kooboo.Meta.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kooboo.Meta.Views.Abstracts
{
    public abstract class KbView
    {
        public abstract string Name { get; }

        public string Id { get; } = Guid.NewGuid().ToString();

        public List<KbHook> Hooks { get; private set; } = new List<KbHook>();

    }
}
