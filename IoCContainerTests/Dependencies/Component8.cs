using IoCContainerTests.Dependencies.Abstract;
using IoCContainerTests.Dependencies.Contracts;

namespace IoCContainerTests.Dependencies
{
    public class Component8 : IDependency
    {
        private Abstract7 _dep1;
        private Abstract1 _dep2;
        private Abstract2 _dep3;
        private Abstract3 _dep4;
        private Abstract4 _dep5;

        public Component8(Abstract7 dep1, Abstract1 dep2, Abstract2 dep3, Abstract3 dep4, Abstract4 dep5)
        {
            _dep1 = dep1;
            _dep2 = dep2;
            _dep3 = dep3;
            _dep4 = dep4;
            _dep5 = dep5;
        }

        public bool IsValid()
        {
            return _dep1.IsValid() && _dep2.IsValid() && _dep3.IsValid() && _dep4.IsValid() && _dep5.IsValid();
        }
    }
}
