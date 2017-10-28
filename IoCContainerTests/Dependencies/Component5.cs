using IoCContainerTests.Dependencies.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoCContainerTests.Dependencies
{
    class Component5 : Interface5, IDependency
    {
        private Interface1 dep2;
        private Interface2 dep3;
        private Interface3 dep4;
        private Interface4 dep5;

        public Component5(Interface1 dep2, Interface2 dep3, Interface3 dep4, Interface4 dep5)
        {
            this.dep2 = dep2;
            this.dep3 = dep3;
            this.dep4 = dep4;
            this.dep5 = dep5;
        }

        public bool IsValid()
        {
            return dep2 != null && dep3 != null && dep4 != null && dep5 != null;
        }
    }
}
