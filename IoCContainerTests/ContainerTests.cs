using Xunit;
using IoCContainer;
using IoCContainerTests.Dependencies.Contracts;
using IoCContainerTests.Dependencies;
using IoCContainer.Exceptions;
using IoCContainerTests.Dependencies.Abstract;

namespace IoCContainerTests
{
    public class ContainerTests
    {
        /// <summary>
        /// Resolving a flat dependency graph returns a valid object.
        /// </summary>
        [Fact]
        public void Dependency_Graph_Flat_Resolved_Valid_Object()
        {
            var container = new Container();
            container.Bind<Interface1, Component1>();
            container.Bind<Interface2, Component2>();
            container.Bind<Interface3, Component3>();
            container.Bind<Interface4, Component4>();
            var comp = container.Resolve<Component5>();

            Assert.True(comp.IsValid());
        }
        
        /// <summary>
        /// Resolving a nested dependency graph returns a valid object.
        /// </summary>
        [Fact]
        public void Dependency_Graph_Nested_Resolved_Valid_Object()
        {
            var container = new Container();
            container.Bind<Interface1, Component1>();
            container.Bind<Interface2, Component2>();
            container.Bind<Interface3, Component3>();
            container.Bind<Interface4, Component4>();
            container.Bind<Interface5, Component5>();
            var comp = container.Resolve<Component6>();

            Assert.True(comp.IsValid());
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

        /// <summary>
        /// Binding an abstract to a concrete will throw an exception.
        /// </summary>
        [Fact]
        public void Bind_To_Abstract_Throws_Exception()
        {
            var container = new Container();
            Assert.Throws<BindToAbstractException>(()=>container.Bind<Interface1, Interface2>());
        }

        /// <summary>
        /// Each binding must only have one concrete class.
        /// </summary>
        [Fact]
        public void Duplicate_Bind_Throws_Exception()
        {
            var container = new Container();
            container.Bind<Interface1, Component1>();
            Assert.Throws<DuplicateBindingKeyException>(()=>container.Bind<Interface1, Component2>());
        }

        /// <summary>
        /// If a binding exists and it's Unbinded, then it will return true.
        /// </summary>
        [Fact]
        public void Unbind_Existing_Bind_Returns_True()
        {
            var container = new Container();
            container.Bind<Interface1, Component1>();
            Assert.True(container.Unbind<Interface1>());
        }

        /// <summary>
        /// If a binding does not exist, then Unbind return false.
        /// </summary>
        [Fact]
        public void Unbind_NonExisting_Bind_Returns_False()
        {
            var container = new Container();
            Assert.False(container.Unbind<Interface1>());
        }

        [Fact]
        public void Rebind_Does_Not_Throw_Exception()
        {
            var container = new Container();

            container.Bind<Interface1, Component2>();
            container.Rebind<Interface1, Component1>();
            container.Bind<Interface2, Component2>();
            container.Bind<Interface3, Component3>();
            container.Bind<Interface4, Component4>();
            var comp = container.Resolve<Component5>();
        }

        [Fact]
        public void Binding_To_Abstract_Throws_Exception()
        {
            var container = new Container();
            Assert.Throws<BindToAbstractException>(()=>container.Bind<Abstract2, Abstract1>());
        }

        [Fact]
        public void Abstract_Dependency_Graph_Flat_Resolved_Valid_Object()
        {
            var container = new Container();
            container.Bind<Abstract1, Component1>();
            container.Bind<Abstract2, Component2>();
            container.Bind<Abstract3, Component3>();
            container.Bind<Abstract4, Component4>();
            container.Bind<Abstract5, Component5>();
            var comp = container.Resolve<Component7>();
            Assert.True(comp.IsValid());
        }

        [Fact]
        public void Abstract_Dependency_Graph_Nested_Resolved_Valid_Object()
        {
            var container = new Container();
            container.Bind<Abstract1, Component1>();
            container.Bind<Abstract2, Component2>();
            container.Bind<Abstract3, Component3>();
            container.Bind<Abstract4, Component4>();
            container.Bind<Abstract7, Component7>();
            var comp = container.Resolve<Component8>();
            Assert.True(comp.IsValid());
        }

    }
}
