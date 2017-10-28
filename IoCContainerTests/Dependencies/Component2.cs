using IoCContainerTests.Dependencies.Abstract;
using IoCContainerTests.Dependencies.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoCContainerTests.Dependencies
{
    class Component2 : Abstract2, Interface2
    {
        public override bool IsValid()
        {
            return true;
        }
    }
}
