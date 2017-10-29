using IoCContainerTests.Dependencies.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoCContainerTests.Dependencies
{
    public class Component9 : IDependency
    {
        private Interface1 _dep1;
        private Interface2 _dep2;
        private Interface3 _dep3;
        // This is an optional dependency
        private Interface4 _dep4;

        public Component9(Interface1 dep1, Interface2 dep2, Interface3 dep3)
        {
            _dep1 = dep1;
            _dep2 = dep2;
            _dep3 = dep3;
        }

        public Component9(Interface1 dep1, Interface2 dep2, Interface3 dep3, Interface4 dep4)
        {
            _dep1 = dep1;
            _dep2 = dep2;
            _dep3 = dep3;
            _dep4 = dep4;
        }

        public bool IsValid()
        {
            return _dep1 != null && _dep2 != null && _dep3 != null;
        }
    }
}
