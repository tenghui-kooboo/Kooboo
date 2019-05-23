using System;
using System.Collections.Generic;

using Kooboo.Dom;

namespace Kooboo.Model.Render
{
    public class CommentWrap : CharacterDataWrap
    {
        public Comment _inner;

        public CommentWrap(DocumentWrap doc, Comment comment)
            : base(doc)
        {
            _inner = comment;
        }

        public override Node Node => _inner;
    }
}
