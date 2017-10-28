using System;
using Xunit;
using IoCContainer;
using System.Collections;
using System.IO;
using IoCContainerTests.Dependencies.Contracts;
using IoCContainerTests.Dependencies;

namespace IoCContainerTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var container = new Container();
            container.Bind<Interface_2, Dependency_2>();
            container.Bind<Interface_3, Dependency_3>();
            container.Bind<Interface_4, Dependency_4>();
            container.Bind<Interface_5, Dependency_5>();
            var dep = container.Resolve<Dependency_1>();

            Assert.True(dep.IsValid());
        }
    }
}
