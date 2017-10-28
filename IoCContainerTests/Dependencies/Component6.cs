using IoCContainerTests.Dependencies.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoCContainerTests.Dependencies
{
    class Component6 : IDependency
    {
        private Interface5 dep1;

        public Component6(Interface5 dep1)
        {
            this.dep1 = dep1;
        }

        public bool IsValid()
        {
            return dep1.IsValid();
        }
    }
}
