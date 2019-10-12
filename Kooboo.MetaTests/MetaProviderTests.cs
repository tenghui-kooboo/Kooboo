using System;
using System.Linq;
using System.Reflection;
using Kooboo.Meta;
using Kooboo.Meta.Tests.Infrastructure;
using Kooboo.Meta.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kooboo.Meta.Tests
{
    [TestClass()]
    public class MetaProviderTests
    {

        [TestMethod()]
        public void GetMetaTest()
        {

            var assemblies = new[] { Assembly.GetExecutingAssembly() };
            var provider = new MetaProvider(assemblies);
            var meta = provider.GetMeta("pages");
            Assert.IsNotNull(meta);
            Assert.AreEqual(meta.Views.Count, 1);
            Assert.IsInstanceOfType(meta.Views.First(), typeof(KbButton));
        }
    }
}

