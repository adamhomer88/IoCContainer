using IoCContainerTests.Dependencies.Abstract;
using IoCContainerTests.Dependencies.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoCContainerTests.Dependencies
{
    public class Component7 : Abstract7
    {
        private Abstract1 _dep1;
        private Abstract2 _dep2;
        private Abstract3 _dep3;

        public Component7(Abstract1 dep1, Abstract2 dep2, Abstract3 dep3)
        {
            _dep1 = dep1;
            _dep2 = dep2;
            _dep3 = dep3;
        }

        public override bool IsValid()
        {
            return _dep1.IsValid() && _dep2.IsValid() && _dep3.IsValid();
        }
    }
}
