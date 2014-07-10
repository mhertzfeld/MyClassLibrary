using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;


namespace MyClassLibrary
{
    public static class MyDataConverter
    {
        public static Boolean CheckForNull(Object obj)
        {
            if (obj == null)
            { return true; }
            else
            { return (obj == DBNull.Value); }
        }

        private static string ToAscii128(String toConevertString)
        {
            if (toConevertString == null)
            { return null; }
            else
            { return toConevertString.Normalize(NormalizationForm.FormKD).Where(x => x < 128).ToArray().ToString(); }
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

        public static Boolean? ToBooleanNullable(Object obj, NumberStyles numberStyles = NumberStyles.Any)
        {
            Boolean? output;

            if (CheckForNull(obj))
            { return null; }
            else
            {
                Byte? temp = ToByteNullable(obj, numberStyles);

                if (temp == null)
                { output = null; }
                else
                { output = ToBoolean(temp); }
            }

            return output;
        }

        public static Byte ToByte(Object obj, NumberStyles numberStyles = NumberStyles.Any)
        {
            Byte output;

            if (CheckForNull(obj))
            { output = 0; }
            else
            {
                try
                {
                    output = Byte.Parse(obj.ToString(), numberStyles);
                }
                catch (Exception exception)
                {
                    Trace.WriteLine(exception.ToString());

                    output = 0;
                }
            }

            return output;
        }

        public static Byte? ToByteNullable(Object obj, NumberStyles numberStyles = NumberStyles.Any)
        {
            Byte? output;

            if (CheckForNull(obj))
            { output = null; }
            else
            {
                try
                {
                    output = Byte.Parse(obj.ToString(), numberStyles);
                }
                catch (Exception exception)
                {
                    Trace.WriteLine(exception.ToString());

                    output = null;
                }
            }

            return output;
        }

        public static DateTime ToDateTime(Object obj, String format = null)
        {
            DateTime output;

            if (CheckForNull(obj))
            { output = default(DateTime); }
            else
            {
                try
                {
                    if (format == null)
                    {
                        output = DateTime.Parse(obj.ToString());
                    }
                    else
                    {
                        output = DateTime.ParseExact(obj.ToString(), format, CultureInfo.InvariantCulture);
                    }
                }
                catch (Exception exception)
                {
                    Trace.WriteLine(exception.ToString());

                    output = default(DateTime);
                }
            }

            return output;
        }

        public static DateTime? ToDateTimeNullable(Object obj, String format = null)
        {
            DateTime? output;

            if (MyDataConverter.CheckForNull(obj))
            { output = null; }
            else
            {
                try
                {
                    if (format == null)
                    {
                        output = DateTime.Parse(obj.ToString());
                    }
                    else
                    {
                        output = DateTime.ParseExact(obj.ToString(), format, CultureInfo.InvariantCulture);
                    }
                }
                catch (Exception exception)
                {
                    Trace.WriteLine(exception.ToString());

                    output = null;
                }
            }

            return output;
        }

        public static Decimal ToDecimal(Object obj, NumberStyles numberStyles = NumberStyles.Any)
        {
            Decimal output;

            if (CheckForNull(obj))
            { output = 0; }
            else
            {
                try
                {
                    output = Decimal.Parse(obj.ToString(), numberStyles);
                }
                catch (Exception exception)
                {
                    Trace.WriteLine(exception.ToString());

                    output = 0;
                }
            }

            return output;
        }

        public static Decimal? ToDecimalNullable(Object obj, NumberStyles numberStyles = NumberStyles.Any)
        {
            Decimal? output;

            if (CheckForNull(obj))
            { output = null; }
            else
            {
                try
                {
                    output = Decimal.Parse(obj.ToString(), numberStyles);
                }
                catch (Exception exception)
                {
                    Trace.WriteLine(exception.ToString());

                    output = null;
                }
            }

            return output;
        }

        public static Int16 ToInt16(Object obj, NumberStyles numberStyles = NumberStyles.Any)
        {
            Int16 output;

            if (CheckForNull(obj))
            { output = 0; }
            else
            {
                try
                {
                    output = Int16.Parse(obj.ToString(), numberStyles);
                }
                catch (Exception exception)
                {
                    Trace.WriteLine(exception.ToString());

                    output = 0;
                }
            }

            return output;
        }

        public static Int16? ToInt16Nullable(Object obj, NumberStyles numberStyles = NumberStyles.Any)
        {
            Int16? output;

            if (CheckForNull(obj))
            { output = null; }
            else
            {
                try
                {
                    output = Int16.Parse(obj.ToString(), numberStyles);
                }
                catch (Exception exception)
                {
                    Trace.WriteLine(exception.ToString());

                    output = null;
                }
            }

            return output;
        }

        public static Int32 ToInt32(Object obj, NumberStyles numberStyles = NumberStyles.Any)
        {
            Int32 output;

            if (CheckForNull(obj))
            { output = 0; }
            else
            {
                try
                {
                    output = Int32.Parse(obj.ToString(), numberStyles);
                }
                catch (Exception exception)
                {
                    Trace.WriteLine(exception.ToString());

                    output = 0;
                }
            }

            return output;
        }

        public static Int32? ToInt32Nullable(Object obj, NumberStyles numberStyles = NumberStyles.Any)
        {
            Int32? output;

            if (CheckForNull(obj))
            { output = null; }
            else
            {
                try
                {
                    output = Int32.Parse(obj.ToString(), numberStyles);
                }
                catch (Exception exception)
                {
                    Trace.WriteLine(exception.ToString());

                    output = null;
                }
            }

            return output;
        }

        public static Int64 ToInt64(Object obj, NumberStyles numberStyles = NumberStyles.Any)
        {
            Int64 output;

            if (CheckForNull(obj))
            { output = 0; }
            else
            {
                try
                {
                    output = Int64.Parse(obj.ToString(), numberStyles);
                }
                catch (Exception exception)
                {
                    Trace.WriteLine(exception.ToString());

                    output = 0;
                }
            }

            return output;
        }

        public static Int64? ToInt64Nullable(Object obj, NumberStyles numberStyles = NumberStyles.Any)
        {
            Int64? output;

            if (CheckForNull(obj))
            { output = null; }
            else
            {
                try
                {
                    output = Int64.Parse(obj.ToString(), numberStyles);
                }
                catch (Exception exception)
                {
                    Trace.WriteLine(exception.ToString());

                    output = null;
                }
            }

            return output;
        }

        public static String ToString(Object obj, Boolean trim = true)
        {
            String output;

            if (CheckForNull(obj))
            { output = null; }
            else
            {
                output = (trim ? obj.ToString().Trim() : obj.ToString());

                if (output.Length == 0)
                { output = null; }                
            }

            return output;
        }

        public static TimeSpan ToTimeSpan(Object obj, String format = null)
        {
            TimeSpan output;

            if (CheckForNull(obj))
            { output = default(TimeSpan); }
            else
            {
                try
                {
                    output = TimeSpan.Parse(obj.ToString());
                }
                catch (Exception exception)
                {
                    Trace.WriteLine(exception.ToString());

                    output = default(TimeSpan);
                }
            }

            return output;
        }

        public static TimeSpan? ToTimeSpanNullable(Object obj)
        {
            TimeSpan? output;

            if (CheckForNull(obj))
            { output = null; }
            else
            {
                try
                {
                    output = TimeSpan.Parse(obj.ToString());
                }
                catch (Exception exception)
                {
                    Trace.WriteLine(exception.ToString());

                    output = null;
                }
            }

            return output;
        }

        public static UInt16 ToUInt16(Object obj, NumberStyles numberStyles = NumberStyles.Any)
        {
            UInt16 output;

            if (CheckForNull(obj))
            { output = 0; }
            else
            {
                try
                {
                    output = UInt16.Parse(obj.ToString(), numberStyles);
                }
                catch (Exception exception)
                {
                    Trace.WriteLine(exception.ToString());

                    output = 0;
                }
            }

            return output;
        }

        public static UInt16? ToUInt16Nullable(Object obj, NumberStyles numberStyles = NumberStyles.Any)
        {
            UInt16? output;

            if (CheckForNull(obj))
            { output = null; }
            else
            {
                try
                {
                    output = UInt16.Parse(obj.ToString(), numberStyles);
                }
                catch (Exception exception)
                {
                    Trace.WriteLine(exception.ToString());

                    output = null;
                }
            }

            return output;
        }

        public static UInt32 ToUInt32(Object obj, NumberStyles numberStyles = NumberStyles.Any)
        {
            UInt32 output;

            if (CheckForNull(obj))
            { output = 0; }
            else
            {
                try
                {
                    output = UInt32.Parse(obj.ToString(), numberStyles);
                }
                catch (Exception exception)
                {
                    Trace.WriteLine(exception.ToString());

                    output = 0;
                }
            }

            return output;
        }

        public static UInt32? ToUInt32Nullable(Object obj, NumberStyles numberStyles = NumberStyles.Any)
        {
            UInt32? output;

            if (CheckForNull(obj))
            { output = null; }
            else
            {
                try
                {
                    output = UInt32.Parse(obj.ToString(), numberStyles);
                }
                catch (Exception exception)
                {
                    Trace.WriteLine(exception.ToString());

                    output = null;
                }
            }

            return output;
        }

        public static UInt64 ToUInt64(Object obj, NumberStyles numberStyles = NumberStyles.Any)
        {
            UInt64 output;

            if (CheckForNull(obj))
            { output = 0; }
            else
            {
                try
                {
                    output = UInt64.Parse(obj.ToString(), numberStyles);
                }
                catch (Exception exception)
                {
                    Trace.WriteLine(exception.ToString());

                    output = 0;
                }
            }

            return output;
        }

        public static UInt64? ToUInt64Nullable(Object obj, NumberStyles numberStyles = NumberStyles.Any)
        {
            UInt64? output;

            if (CheckForNull(obj))
            { output = null; }
            else
            {
                try
                {
                    output = UInt64.Parse(obj.ToString(), numberStyles);
                }
                catch (Exception exception)
                {
                    Trace.WriteLine(exception.ToString());

                    output = null;
                }
            }

            return output;
        }
    }
}