using IoCContainerTests.Dependencies.Abstract;
using IoCContainerTests.Dependencies.Contracts;

namespace IoCContainerTests.Dependencies
{
    public class Component5 : Abstract5, Interface5
    {
        public Interface1 dep2 { get; set; }
        public Interface2 dep3 { get; set; }
        public Interface3 dep4 { get; set; }
        public Interface4 dep5 { get; set; }

        public Component5(Interface1 dep2, Interface2 dep3, Interface3 dep4, Interface4 dep5)
        {
            this.dep2 = dep2;
            this.dep3 = dep3;
            this.dep4 = dep4;
            this.dep5 = dep5;
        }

        public override bool IsValid()
        {
            return dep2 != null && dep3 != null && dep4 != null && dep5 != null;
        }
    }
}
