using IoCContainerTests.Dependencies.Abstract;
using IoCContainerTests.Dependencies.Contracts;

namespace IoCContainerTests.Dependencies
{
    public class Component4 : Abstract4, Interface4
    {
        public override bool IsValid()
        {
            return true;
        }
    }
}
