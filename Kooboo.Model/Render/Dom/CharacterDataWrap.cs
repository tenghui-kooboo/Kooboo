using System;
using System.Collections.Generic;
using System.Text;
using Kooboo.Dom;

namespace Kooboo.Model.Render
{
    public abstract class CharacterDataWrap : NodeWrap
    {
        public CharacterDataWrap(DocumentWrap doc)
            : base(doc)
        {
        }

        internal override int Output(StringBuilder sb)
        {
            var data = Node as CharacterData;
            sb.Append(data.data);

            return EndIndex + 1;
        }
    }
}
