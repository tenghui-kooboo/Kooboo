using System;
using System.Collections.Generic;

using Kooboo.Dom;

namespace Kooboo.Model.Render
{
    public class TextWrap : CharacterDataWrap
    {
        public Text _inner;

        public TextWrap(DocumentWrap doc, Text text)
            : base(doc)
        {
            _inner = text;
        }

        public override Node Node => _inner;
    }
}
