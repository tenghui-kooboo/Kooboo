using Kooboo.Meta;
using Kooboo.Meta.Views;
using Kooboo.Meta.Views.Abstracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.MetaTests.Views.Abstracts
{
    [TestClass]
    public class KbContainerTests
    {
        class TestKbContainer : KbContainer
        {
            public override string Name => nameof(TestKbContainer);
        }

        [TestMethod]
        public void AddViewTest()
        {
            var kbContainer = new TestKbContainer();
            var kbButton = new KbButton();
            kbContainer.AddView(kbButton);

            Assert.AreEqual(kbContainer.Views.Count, 1);
        }

    }
}
