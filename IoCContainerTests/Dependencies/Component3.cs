using IoCContainerTests.Dependencies.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoCContainerTests.Dependencies
{
    class Component3 : Interface3, IDependency
    {
        public bool IsValid()
        {
            return true;
        }
    }
}
