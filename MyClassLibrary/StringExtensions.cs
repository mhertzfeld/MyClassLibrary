using System;
using System.Text.RegularExpressions;


namespace MyClassLibrary
{
    public static class StringExtensions
    {
        public static Boolean Contains(this String str, String value, StringComparison stringComparison)
        {
            return (str.IndexOf(value, stringComparison) >= 0);
        }

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



//MyClassLibrary
//Copyright (C) 2013 Matthew Hertzfeld https://github.com/mhertzfeld
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
//You should have received a copy of the GNU General Public License along with this program.  If not, see <http://www.gnu.org/licenses/>.