using IoCContainerTests.Dependencies.Abstract;
using IoCContainerTests.Dependencies.Contracts;

namespace IoCContainerTests.Dependencies
{
    class Component1 : Abstract1, Interface1
    {
        public override bool IsValid()
        {
            return true;
        }
    }
}
