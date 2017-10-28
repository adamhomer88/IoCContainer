using Xunit;
using IoCContainer;
using IoCContainerTests.Dependencies.Contracts;
using IoCContainerTests.Dependencies;
using IoCContainer.Exceptions;

namespace IoCContainerTests
{
    public class ContainerTests
    {
        /// <summary>
        /// Resolving a flat dependency graph returns a valid object.
        /// </summary>
        [Fact]
        public void Dependency_Graph_Flat()
        {
            var container = new Container();
            container.Bind<Interface1, Component1>();
            container.Bind<Interface2, Component2>();
            container.Bind<Interface3, Component3>();
            container.Bind<Interface4, Component4>();
            var dep = container.Resolve<Component5>();

            Assert.True(dep.IsValid());
        }
        
        /// <summary>
        /// Resolving a nested dependency graph returns a valid object.
        /// </summary>
        [Fact]
        public void Dependency_Graph_Nested()
        {
            var container = new Container();
            container.Bind<Interface1, Component1>();
            container.Bind<Interface2, Component2>();
            container.Bind<Interface3, Component3>();
            container.Bind<Interface4, Component4>();
            container.Bind<Interface5, Component5>();
            var dep = container.Resolve<Component6>();

            Assert.True(dep.IsValid());
        }

        /// <summary>
        /// Test throws an exception if a dependency is missing.
        /// Interface 1 is a missing.
        /// </summary>
        [Fact]
        public void Resolve_To_Abstract_Throws_Exception()
        {
            var container = new Container();
            container.Bind<Interface2, Component2>();
            container.Bind<Interface3, Component3>();
            container.Bind<Interface4, Component4>();
            container.Bind<Interface5, Component5>();
            Assert.Throws<ResolveToAbstractException>(()=>container.Resolve<Component6>());
        }

        [Fact]
        public void Bind_To_Abstract_Throws_Exception()
        {
            var container = new Container();
            Assert.Throws<BindToAbstractException>(()=>container.Bind<Interface1, Interface2>());
        }

        [Fact]
        public void Duplicate_Bind_Throws_Exception()
        {
            var container = new Container();
            container.Bind<Interface1, Component1>();
            Assert.Throws<DuplicateBindingKeyException>(()=>container.Bind<Interface1, Component2>());
        }

        [Fact]
        public void Remove_Existing_Bind_Returns_True()
        {
            var container = new Container();
            container.Bind<Interface1, Component1>();
            Assert.True(container.RemoveBind<Interface1>());
        }

        [Fact]
        public void Remove_NonExisting_Bind_Returns_False()
        {
            var container = new Container();
            Assert.False(container.RemoveBind<Interface1>());
        }

        [Fact]
        public void Rebind_Does_Not_Throw_Exception()
        {
            var container = new Container();

            container.Bind<Interface1, Component2>();
            container.ReBind<Interface1, Component1>();
            container.Bind<Interface2, Component2>();
            container.Bind<Interface3, Component3>();
            container.Bind<Interface4, Component4>();
            var dep = container.Resolve<Component5>();
        }
    }
}
