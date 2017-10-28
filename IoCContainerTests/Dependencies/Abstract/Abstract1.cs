using IoCContainerTests.Dependencies.Contracts;

namespace IoCContainerTests.Dependencies.Abstract
{
    public abstract class Abstract1 : Abstract2, IDependency
    {
        public override abstract bool IsValid();
    }
}
