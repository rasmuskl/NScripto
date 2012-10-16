using System.Linq;
using System.Collections.Generic;
using System;

namespace NScripto.Extensions
{
    internal static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
    }
}