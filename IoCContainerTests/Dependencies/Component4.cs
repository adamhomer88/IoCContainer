using IoCContainerTests.Dependencies.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoCContainerTests.Dependencies
{
    class Component4 : Interface4, IDependency
    {
        public bool IsValid()
        {
            return true;
        }
    }
}
