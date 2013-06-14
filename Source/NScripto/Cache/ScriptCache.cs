using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Collections.Generic;

namespace NScripto.Cache
{
    internal static class ScriptCache
    {
        private static readonly ConcurrentDictionary<string, object> Cache = new ConcurrentDictionary<string, object>();

        public static bool TryGetCachedScript(string scriptText, Type[] environmentTypes, out object script)
        {
            var key = BuildKey(scriptText, environmentTypes);
            return Cache.TryGetValue(key, out script);
        }

        public static void AddCachedScript(string scriptText, Type[] environmentTypes, object genericScript)
        {
            var key = BuildKey(scriptText, environmentTypes);
            Cache.AddOrUpdate(key, x => genericScript, (x, y) => genericScript);
        }

        private static string BuildKey(string scriptText, IEnumerable<Type> environmentTypes)
        {
            var envNames = string.Join(", ", environmentTypes.Select(x => x.AssemblyQualifiedName));
            return string.Format("{0} - {1}", envNames, scriptText);
        }
    }
}