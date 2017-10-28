using IoCContainerTests.Dependencies.Contracts;

namespace IoCContainerTests.Dependencies
{
    public class Component6 : IDependency
    {
        public Interface5 dep { get; set; }

        public Component6(Interface5 dep)
        {
            this.dep = dep;
        }

        public bool IsValid()
        {
            return dep.IsValid();
        }
    }
}
