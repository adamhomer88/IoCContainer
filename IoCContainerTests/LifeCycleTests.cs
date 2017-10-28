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
            // Component5 shouldbe thread static.
            Assert.Equal(comp1.dep, comp2.dep);
            
            // All other components are transient, but belong to static object. Should be equal.
            Assert.Equal(((Component5)comp1.dep).dep2, ((Component5)comp1.dep).dep2);
            Assert.Equal(((Component5)comp1.dep).dep3, ((Component5)comp1.dep).dep3);
            Assert.Equal(((Component5)comp1.dep).dep4, ((Component5)comp1.dep).dep4);
            Assert.Equal(((Component5)comp1.dep).dep5, ((Component5)comp1.dep).dep5);

            // From thread 2
            // Component5 shouldbe thread static.
            Assert.Equal(comp3.dep, comp4.dep);

            // All other components are transient, but belong to static object. Should be equal.
            Assert.Equal(((Component5)comp2.dep).dep2, ((Component5)comp2.dep).dep2);
            Assert.Equal(((Component5)comp2.dep).dep3, ((Component5)comp2.dep).dep3);
            Assert.Equal(((Component5)comp2.dep).dep4, ((Component5)comp2.dep).dep4);
            Assert.Equal(((Component5)comp2.dep).dep5, ((Component5)comp2.dep).dep5);
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
            container.Bind<Interface5, Component5>();

            var thread1 = new Thread(() => comp1 = container.Resolve<Component6>());
            var thread2 = new Thread(() => comp2 = container.Resolve<Component6>());

            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();

            Assert.NotEqual(comp1.dep, comp2.dep);
        }

        [Fact]
        public void Deep_Thread_Static_Lifecycle_Objects_Are_Not_Distinct_In_Separate_Threads()
        {
            Component6 comp1 = null;
            Component6 comp2 = null;
            var container = new Container();
            container.Bind<Interface1, Component1>(LifeCycle.Thread);
            container.Bind<Interface2, Component2>(LifeCycle.Thread);
            container.Bind<Interface3, Component3>(LifeCycle.Thread);
            container.Bind<Interface4, Component4>(LifeCycle.Thread);
            container.Bind<Interface5, Component5>(LifeCycle.Thread);

            var thread1 = new Thread(() => comp1 = container.Resolve<Component6>());
            var thread2 = new Thread(() => comp2 = container.Resolve<Component6>());

            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();

            Assert.NotEqual(comp1.dep, comp2.dep);
            Assert.NotEqual(((Component5)comp1.dep).dep2, ((Component5)comp2.dep).dep2);
            Assert.NotEqual(((Component5)comp1.dep).dep3, ((Component5)comp2.dep).dep3);
            Assert.NotEqual(((Component5)comp1.dep).dep4, ((Component5)comp2.dep).dep4);
            Assert.NotEqual(((Component5)comp1.dep).dep5, ((Component5)comp2.dep).dep5);
        }

        [Fact]
        public void Deep_Thread_Static_Lifecycle_Objects_Combination_Of_Lifecycles_Separate_Threads()
        {
            Component6 comp1 = null;
            Component6 comp2 = null;
            var container = new Container();
            container.Bind<Interface1, Component1>(LifeCycle.Singleton);
            container.Bind<Interface2, Component2>(LifeCycle.Singleton);
            container.Bind<Interface3, Component3>();
            container.Bind<Interface4, Component4>(LifeCycle.Thread);
            container.Bind<Interface5, Component5>();

            var thread1 = new Thread(() => comp1 = container.Resolve<Component6>());
            var thread2 = new Thread(() => comp2 = container.Resolve<Component6>());

            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();

            // Component 1 and 2 are singleton lifecycle. Should be equal across threads.
            Assert.Equal(((Component5)comp1.dep).dep2, ((Component5)comp2.dep).dep2);
            Assert.Equal(((Component5)comp1.dep).dep3, ((Component5)comp2.dep).dep3);

            // Component 3 and 5 are transient. Should not be equal across threads.
            Assert.NotEqual(comp1.dep, comp2.dep);
            Assert.NotEqual(((Component5)comp1.dep).dep4, ((Component5)comp2.dep).dep4);

            // Component 4 is thread static. Should not be equal across threads.
            Assert.NotEqual(((Component5)comp1.dep).dep5, ((Component5)comp2.dep).dep5);
        }

        [Fact]
        public void Deep_Thread_Static_Lifecycle_Objects_Combination_Of_Lifecycles_Same_Thread()
        {
            Component6 comp1 = null;
            Component6 comp2 = null;
            Component6 comp3 = null;
            Component6 comp4 = null;

            var container = new Container();
            container.Bind<Interface1, Component1>(LifeCycle.Singleton);
            container.Bind<Interface2, Component2>(LifeCycle.Singleton);
            container.Bind<Interface3, Component3>();
            container.Bind<Interface4, Component4>(LifeCycle.Thread);
            container.Bind<Interface5, Component5>();

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
            // Component 4 is thread static. Should be equal.
            Assert.Equal(((Component5)comp1.dep).dep5, ((Component5)comp2.dep).dep5);


            // Component 1 & 2 are singletons. Should be equal.
            Assert.Equal(((Component5)comp1.dep).dep2, ((Component5)comp2.dep).dep2);
            Assert.Equal(((Component5)comp1.dep).dep3, ((Component5)comp2.dep).dep3);

            // Component 3 and 5 are transient. Should not be equal.
            Assert.NotEqual(comp1.dep, comp2.dep);
            Assert.NotEqual(((Component5)comp1.dep).dep4, ((Component5)comp2.dep).dep4);

            // From thread 2
            // Component 4 is thread static. Should be equal.
            Assert.Equal(((Component5)comp3.dep).dep5, ((Component5)comp4.dep).dep5);


            // Component 1 & 2 are singletons. Should be equal.
            Assert.Equal(((Component5)comp3.dep).dep2, ((Component5)comp4.dep).dep2);
            Assert.Equal(((Component5)comp3.dep).dep3, ((Component5)comp4.dep).dep3);

            // Component 3 and 5 are transient. Should not be equal.
            Assert.NotEqual(comp3.dep, comp4.dep);
            Assert.NotEqual(((Component5)comp3.dep).dep4, ((Component5)comp4.dep).dep4);
        }
    }
}
