using System;
using System.Linq;
using Kooboo.Meta;
using Kooboo.Meta.Views.Abstracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kooboo.MetaTests.Views.Abstracts
{
    [TestClass]
    public class KbClickableTests
    {
        class TestKbClickable : KbClickable
        {
            public override string Name => nameof(KbClickable);
        }


        [TestMethod]
        public void NewWindowTest()
        {
            var kbView = new TestKbClickable();
            kbView.NewWindow("/about");

            Assert.AreEqual(kbView.Hooks.Count, 1);
            Assert.IsTrue(kbView.Hooks.First().Name.StartsWith("click"));
            Assert.AreEqual(kbView.Hooks.First().Execute, "window.open(`/about`)");
        }

        [TestMethod]
        public void AddHook_name_id_Test()
        {
            var kbView = new TestKbClickable();
            kbView.Redirect("/about");

            Assert.AreEqual(kbView.Hooks.Count, 1);
            Assert.IsTrue(kbView.Hooks.First().Name.StartsWith("click"));
            Assert.AreEqual(kbView.Hooks.First().Execute, "location.href=`/about`");
        }

        [TestMethod]
        public void LoadDataTest()
        {
            var kbView = new TestKbClickable();
            kbView.Execute("console.log('hello')");

            Assert.AreEqual(kbView.Hooks.Count, 1);
            Assert.IsTrue(kbView.Hooks.First().Name.StartsWith("click"));
            Assert.AreEqual(kbView.Hooks.First().Execute, "console.log('hello')");
        }
    }
}
