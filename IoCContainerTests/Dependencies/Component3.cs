using IoCContainerTests.Dependencies.Abstract;
using IoCContainerTests.Dependencies.Contracts;

namespace IoCContainerTests.Dependencies
{
    public class Component3 : Abstract3, Interface3
    {
        public override bool IsValid()
        {
            return true;
        }
    }
}
