using System;
using System.Linq;
using Kooboo.Meta;
using Kooboo.Meta.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kooboo.MetaTests.Views
{
    [TestClass]
    public class KbButtonTests
    {
        [TestMethod]
        public void PullRightTest()
        {
            var btn = new KbButton();
            btn.PullRight();
            Assert.AreEqual(btn.Hooks.Count, 1);
            Assert.IsTrue(btn.Hooks.First().Name.StartsWith("load"));
            Assert.AreEqual(btn.Hooks.First().Execute, "k.self.$el.classList.add(`pull-right`)");
        }
    }
}
