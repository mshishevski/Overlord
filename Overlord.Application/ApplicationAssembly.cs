using System.Reflection;

namespace Overlord.Application
{
    public static class ApplicationAssembly
    {
        public static Assembly Get() => typeof(ApplicationAssembly).Assembly;
    }
}
