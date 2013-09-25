using System;


namespace MyClassLibrary
{
    public static class MyDataConverter
    {
        public static Boolean CheckForNull(Object obj)
        {
            if (obj == null)
            {
                return true;
            }
            else
            {
                return (obj == DBNull.Value);
            }
        }

        public static Boolean ToBoolean(Object obj)
        {
            Boolean result = false;

            if (!CheckForNull(obj))
            {
                String toConvertString = obj.ToString().Trim().ToUpper();

                switch (toConvertString)
                {
                    case "1":

                        result = true;

                        break;

                    case "TRUE":

                        result = true;

                        break;

                    case "Y":

                        result = true;

                        break;

                    case "YES":

                        result = true;

                        break;
                }
            }

            return result;
        }

        public static Boolean? ToBooleanNullable(Object obj)
        {
            if (CheckForNull(obj))
            {
                return null;
            }
            else
            {
                return ToBoolean(obj);
            }
        }

        public static Byte ToByte(Object obj)
        {
            if (CheckForNull(obj))
            {
                return 0;
            }
            else
            {
                return Convert.ToByte(obj);
            }
        }

        public static Byte? ToByteNullable(Object obj)
        {
            if (CheckForNull(obj))
            {
                return null;
            }
            else
            {
                return Convert.ToByte(obj);
            }
        }

        public static DateTime ToDateTime(Object obj)
        {
            if (CheckForNull(obj))
            {
                return default(DateTime);
            }
            else
            {
                return Convert.ToDateTime(obj);
            }
        }

        public static DateTime? ToDateTimeNullable(Object obj)
        {
            if (CheckForNull(obj))
            {
                return null;
            }
            else
            {
                return Convert.ToDateTime(obj);
            }
        }

        public static DateTime ToDateTimeFromOADate(Object obj)
        {
            if (CheckForNull(obj))
            {
                return default(DateTime);
            }
            else
            {
                return DateTime.FromOADate(Convert.ToDouble(obj));
            }
        }

        public static DateTime? ToDateTimeNullableFromOADate(Object obj)
        {
            if (CheckForNull(obj))
            {
                return null;
            }
            else
            {
                return DateTime.FromOADate(Convert.ToDouble(obj));
            }
        }

        public static Decimal ToDecimal(Object obj)
        {
            if (CheckForNull(obj))
            {
                return 0;
            }
            else
            {
                return Convert.ToDecimal(obj);
            }
        }

        public static Decimal? ToDecimalNullable(Object obj)
        {
            if (CheckForNull(obj))
            {
                return null;
            }
            else
            {
                return Convert.ToDecimal(obj);
            }
        }

        public static Int16 ToInt16(Object obj)
        {
            if (CheckForNull(obj))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt16(obj);
            }
        }

        public static Int16? ToInt16Nullable(Object obj)
        {
            if (CheckForNull(obj))
            {
                return null;
            }
            else
            {
                return Convert.ToInt16(obj);
            }
        }

        public static Int32 ToInt32(Object obj)
        {
            if (CheckForNull(obj))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public static Int32? ToInt32Nullable(Object obj)
        {
            if (CheckForNull(obj))
            {
                return null;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public static Int64 ToInt64(Object obj)
        {
            if (CheckForNull(obj))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt64(obj);
            }
        }

        public static Int64? ToInt64Nullable(Object obj)
        {
            if (CheckForNull(obj))
            {
                return null;
            }
            else
            {
                return Convert.ToInt64(obj);
            }
        }

        public static String ToString(Object obj)
        {
            if (CheckForNull(obj))
            {
                return null;
            }
            else
            {
                String objString = obj.ToString().Trim();

                if (objString.Length > 0)
                {
                    return objString;
                }
                else
                {
                    return null;
                }
            }
        }

        public static TimeSpan ToTimeSpan(Object obj)
        {
            if (CheckForNull(obj))
            {
                return default(TimeSpan);
            }
            else
            {
                TimeSpan timeSpan;

                if (TimeSpan.TryParse(obj.ToString(), out timeSpan))
                {
                    return timeSpan;
                }
                else
                {
                    return default(TimeSpan);
                }
            }
        }

        public static TimeSpan? ToTimeSpanNullable(Object obj)
        {
            if (CheckForNull(obj))
            {
                return null;
            }
            else
            {
                TimeSpan timeSpan;

                if (TimeSpan.TryParse(obj.ToString(), out timeSpan))
                {
                    return timeSpan;
                }
                else
                {
                    return null;
                }
            }
        }

        public static UInt16 ToUInt16(Object obj)
        {
            if (CheckForNull(obj))
            {
                return 0;
            }
            else
            {
                return Convert.ToUInt16(obj);
            }
        }

        public static UInt16? ToUInt16Nullable(Object obj)
        {
            if (CheckForNull(obj))
            {
                return null;
            }
            else
            {
                return Convert.ToUInt16(obj);
            }
        }

        public static UInt32 ToUInt32(Object obj)
        {
            if (CheckForNull(obj))
            {
                return 0;
            }
            else
            {
                return Convert.ToUInt32(obj);
            }
        }

        public static UInt32? ToUInt32Nullable(Object obj)
        {
            if (CheckForNull(obj))
            {
                return null;
            }
            else
            {
                return Convert.ToUInt32(obj);
            }
        }

        public static UInt64 ToUInt64(Object obj)
        {
            if (CheckForNull(obj))
            {
                return 0;
            }
            else
            {
                return Convert.ToUInt64(obj);
            }
        }

        public static UInt64? ToUInt64Nullable(Object obj)
        {
            if (CheckForNull(obj))
            {
                return null;
            }
            else
            {
                return Convert.ToUInt64(obj);
            }
        }
    }
}



//MyClassLibrary
//Copyright (C) 2013 Matthew Hertzfeld https://github.com/mhertzfeld
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
//You should have received a copy of the GNU General Public License along with this program.  If not, see <http://www.gnu.org/licenses/>.