using System;
using System.Linq;
using Kooboo.Meta;
using Kooboo.Meta.Views.Abstracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kooboo.MetaTests.Views.Abstracts
{
    [TestClass]
    public class KbViewTests
    {
        class TestKbView : KbView
        {
            public override string Name => nameof(TestKbView);
        }


        [TestMethod]
        public void AddHookTest()
        {
            var kbView = new TestKbView();
            kbView.AddHook("a", "console.log('hello')");

            Assert.AreEqual(kbView.Hooks.Count, 1);
            Assert.AreEqual(kbView.Hooks.First().Name, "a");
            Assert.AreEqual(kbView.Hooks.First().Execute, "console.log('hello')");
        }

        [TestMethod]
        public void AddHook_name_id_Test()
        {
            var kbView = new TestKbView();
            kbView.AddHook("a", "1", "console.log('hello')");

            Assert.AreEqual(kbView.Hooks.Count, 1);
            Assert.AreEqual(kbView.Hooks.First().Name, "a_1");
            Assert.AreEqual(kbView.Hooks.First().Execute, "console.log('hello')");
        }

        [TestMethod]
        public void LoadDataTest()
        {
            var kbView = new TestKbView();
            kbView.LoadData("/api");

            Assert.AreEqual(kbView.Hooks.Count, 1);
            Assert.IsTrue(kbView.Hooks.First().Name.StartsWith("load"));
            Assert.AreEqual(kbView.Hooks.First().Execute, $"k.self.$loadData(`/api`)");
        }

        [TestMethod]
        public void AddClassTest()
        {
            var kbView = new TestKbView();
            kbView.AddClass("color");

            Assert.AreEqual(kbView.Hooks.Count, 1);
            Assert.IsTrue(kbView.Hooks.First().Name.StartsWith("load"));
            Assert.AreEqual(kbView.Hooks.First().Execute, $"k.self.$el.classList.add(`color`)");
        }

    }
}
