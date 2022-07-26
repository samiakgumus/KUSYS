using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Extensions
{
    public static class StringExtensions
    {
        public static string FirstCharToUpper(this string input) =>
         input switch
         {
             null => throw new ArgumentNullException(nameof(input)),
             "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
             _ => input[0].ToString().ToUpper() + input.Substring(1)
         };

        public static string GetLast(this string source, int tail_length)
        {
            if (tail_length >= source.Length)
                return source;
            return source.Substring(source.Length - tail_length);
        }
    }
}
