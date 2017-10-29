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
        /// Resolve will throw an exception if a dependency is missing.
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
            Assert.Throws<MissingDependencyException>(()=>container.Resolve<Component6>());
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
        /// If a binding exists and it's Unbound, then Unbind will return true.
        /// </summary>
        [Fact]
        public void Unbind_Existing_Bind_Returns_True()
        {
            var container = new Container();
            container.Bind<Interface1, Component1>();
            Assert.True(container.Unbind<Interface1>());
        }

        /// <summary>
        /// If a binding does not exist, then Unbind will return false.
        /// </summary>
        [Fact]
        public void Unbind_NonExisting_Bind_Returns_False()
        {
            var container = new Container();
            Assert.False(container.Unbind<Interface1>());
        }

        /// <summary>
        /// Rebind successfully unbinds and binds the new concrete.
        /// </summary>
        [Fact]
        public void Rebind_Does_Resolves_Valid_Object()
        {
            var container = new Container();

            container.Bind<Interface1, Component2>();
            container.Rebind<Interface1, Component1>();
            container.Bind<Interface2, Component2>();
            container.Bind<Interface3, Component3>();
            container.Bind<Interface4, Component4>();
            var comp = container.Resolve<Component5>();
            Assert.True(comp.IsValid());
        }

        /// <summary>
        /// Container should prevent binding an abstract to an abstract by throwing an exception.
        /// </summary>
        [Fact]
        public void Binding_To_Abstract_Throws_Exception()
        {
            var container = new Container();
            Assert.Throws<BindToAbstractException>(()=>container.Bind<Abstract2, Abstract1>());
        }

        /// <summary>
        /// Abstract bound to a concrete. Testing that abstract classes can also be used in binding.
        /// </summary>
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

        /// <summary>
        /// Abstract bound to a concrete. Testing that abstract classes can also be used in binding. The nested dependency version.
        /// </summary>
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

        /// <summary>
        /// Resolve will return an object using the first valid constructor with the most arguments.
        /// </summary>
        [Fact]
        public void Multiple_Constructor_Resolves_First_Valid_Object()
        {
            var container = new Container();
            container.Bind<Interface1, Component1>();
            container.Bind<Interface2, Component2>();
            container.Bind<Interface3, Component3>();
            var comp = container.Resolve<Component9>();
            Assert.True(comp.IsValid());
        }

        /// <summary>
        /// Resolve will return an object using the first valid constructor with the most arguments.
        /// </summary>
        [Fact]
        public void Multiple_Constructor_Resolves_Valid_Object_With_Optional_Dependency()
        {
            var container = new Container();
            container.Bind<Interface1, Component1>();
            container.Bind<Interface2, Component2>();
            container.Bind<Interface3, Component3>();
            container.Bind<Interface4, Component4>();
            var comp = container.Resolve<Component9>();
            Assert.True(comp.IsValid());
        }

        /// <summary>
        /// Resolve will return an object using the first valid constructor with the most arguments.
        /// </summary>
        [Fact]
        public void Multiple_Constructors_Resolves_Valid_Object()
        {
            var container = new Container();
            container.Bind<Interface1, Component1>();
            container.Bind<Interface2, Component2>();
            container.Bind<Interface3, Component3>();
            var comp1 = container.Resolve<Component10>();

            container.Bind<Interface5, Component5>();
            var comp2 = container.Resolve<Component10>();

            // comp1 resolved before interface 5 had a binding.
            Assert.True(comp1.IsValid() && comp1._dep4 == null);

            // comp2 resolved after interface 5 had a binding.
            Assert.True(comp2.IsValid() && comp2._dep4 != null);
        }

    }
}
