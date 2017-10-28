using IoCContainer;
using IoCContainerTests.Dependencies;
using IoCContainerTests.Dependencies.Contracts;
using System.Threading;
using Xunit;

namespace IoCContainerTests
{
    public class LifeCycleTests
    {
        [Fact]
        public void Transient_Lifecycle_Objects_Are_Distinct()
        {
            var container = new Container();
            container.Bind<Interface1, Component1>();
            container.Bind<Interface2, Component2>();
            container.Bind<Interface3, Component3>();
            container.Bind<Interface4, Component4>();
            container.Bind<Interface5, Component5>();

            var comp1 = container.Resolve<Component6>();
            var comp2 = container.Resolve<Component6>();
            Assert.NotEqual(comp1, comp2);
        }

        [Fact]
        public void Singleton_Lifecycle_Objects_Are_Not_Distinct()
        {
            var container = new Container();
            container.Bind<Interface1, Component1>();
            container.Bind<Interface2, Component2>();
            container.Bind<Interface3, Component3>();
            container.Bind<Interface4, Component4>();
            container.Bind<Interface5, Component5>(LifeCycle.Singleton);

            var comp1 = container.Resolve<Component6>();
            var comp2 = container.Resolve<Component6>();

            Assert.Equal(comp1.dep, comp2.dep);
        }

        [Fact]
        public void Thread_Static_Lifecycle_Objects_Are_Distinct()
        {
            Component6 comp1 = null;
            Component6 comp2 = null;
            Component6 comp3 = null;
            Component6 comp4 = null;

            var container = new Container();
            container.Bind<Interface1, Component1>();
            container.Bind<Interface2, Component2>();
            container.Bind<Interface3, Component3>();
            container.Bind<Interface4, Component4>();
            container.Bind<Interface5, Component5>(LifeCycle.Thread);

            var thread1 = new Thread(() =>
            {
                comp1 = container.Resolve<Component6>();
                comp2 = container.Resolve<Component6>();
            });
            thread1.Start();
            thread1.Join();

            var thread2 = new Thread(() =>
            {
                comp3 = container.Resolve<Component6>();
                comp4 = container.Resolve<Component6>();
            });
            thread2.Start();
            thread2.Join();

            // From thread 1
            Assert.Equal(comp1.dep, comp2.dep);

            // From thread 2
            Assert.Equal(comp3.dep, comp4.dep);
        }

        [Fact]
        public void Thread_Static_Lifecycle_Objects_Are_Not_Distinct_In_Separate_Threads()
        {
            Component6 comp1 = null;
            Component6 comp2 = null;
            var container = new Container();
            container.Bind<Interface1, Component1>();
            container.Bind<Interface2, Component2>();
            container.Bind<Interface3, Component3>();
            container.Bind<Interface4, Component4>();
            container.Bind<Interface5, Component5>(LifeCycle.Thread);

            var thread1 = new Thread(() => comp1 = container.Resolve<Component6>());
            var thread2 = new Thread(() => comp2 = container.Resolve<Component6>());
            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();
            Assert.NotEqual(comp1.dep, comp2.dep);
        }
    }
}
