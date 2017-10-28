using IoCContainerTests.Dependencies.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoCContainerTests.Dependencies
{
    class Dependency_1
    {
        private Interface_2 dep2;
        private Interface_3 dep3;
        private Interface_4 dep4;
        private Interface_5 dep5;

        public Dependency_1(Interface_2 dep2, Interface_3 dep3, Interface_4 dep4, Interface_5 dep5)
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
