using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Collections.Generic;
using NScripto.Raw;

namespace NScripto.Cache
{
    internal static class ScriptCache
    {
        private static readonly ConcurrentDictionary<string, IRawScript> Cache = new ConcurrentDictionary<string, IRawScript>(); 
 
        public static bool TryGetCachedScript(string scriptText, Type[] environmentTypes, out IRawScript script)
        {
            var key = BuildKey(scriptText, environmentTypes);
            return Cache.TryGetValue(key, out script);
        }

        public static void AddCachedScript(string scriptText, Type[] environmentTypes, IRawScript script)
        {
            var key = BuildKey(scriptText, environmentTypes);
            Cache.AddOrUpdate(key, x => script, (x, y) => script);
        }

        private static string BuildKey(string scriptText, IEnumerable<Type> environmentTypes)
        {
            var envNames = string.Join(", ", environmentTypes.Select(x => x.AssemblyQualifiedName));
            return string.Format("{0} - {1}", envNames, scriptText);
        }
    }
}