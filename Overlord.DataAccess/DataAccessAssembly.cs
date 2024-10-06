using System.Reflection;

namespace Overlord.DataAccess
{
    internal static class DataAccessAssembly
    {
        public static Assembly Get() => typeof(DataAccessAssembly).Assembly;
    }
}
