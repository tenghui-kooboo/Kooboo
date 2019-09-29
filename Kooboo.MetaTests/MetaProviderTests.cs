using System;
using System.Reflection;
using Kooboo.Meta;
using Kooboo.Meta.Tests.Infrastructure;
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
        }
    }
}

