using System;
using System.Text.RegularExpressions;


namespace MyClassLibrary
{
    public static class StringExtensions
    {
        public static String Remove(this String str, String value)
        {
            return str.Replace(value, null);
        }

        public static String Remove(this String str, String value, StringComparison stringComparison)
        {
            return Regex.Replace(str, Regex.Escape(value), "", RegexOptions.IgnoreCase);
        }

        public static String Replace(this String str, String oldValue, String newValue, StringComparison stringComparison)
        {
            return Regex.Replace(str, Regex.Escape(oldValue), newValue, RegexOptions.IgnoreCase);
        }
    }
}