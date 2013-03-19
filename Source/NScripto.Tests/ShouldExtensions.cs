using System.Collections.Generic;
using System.Linq;
using Should;

namespace NScripto.Tests
{
    public static class ShouldExtensions
    {
        public static T ShouldContainExactlyOne<T>(this IEnumerable<object> objs)
        {
            objs.ShouldNotBeNull();
            objs.Count().ShouldEqual(1);
            return objs.First().ShouldBeType<T>();
        }         

        public static T ShouldContainExactlyOne<T>(this IEnumerable<T> objs)
        {
            objs.ShouldNotBeNull();
            objs.Count().ShouldEqual(1);
            return objs.First();
        }         
    }
}