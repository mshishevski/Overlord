using System.Reflection;

namespace Overlord.Domain
{
    public static class DomainAssembly
    {
        public static Assembly Get() => typeof(DomainAssembly).Assembly;
    }
}
