using System;
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
            if (CheckForNull(obj))
            { return false; }
            else
            {
                String toConvertString = obj.ToString().Trim().ToUpper();

                switch (toConvertString)
                {
                    case "1":

                        return true;

                    case "TRUE":

                        return true;

                    case "Y":

                        return true;

                    case "YES":

                        return true;

                    default:

                        return false;
                }
            }
        }

        public static Boolean? ToBooleanNullable(Object obj, NumberStyles numberStyles = NumberStyles.Any)
        {
            if (CheckForNull(obj))
            { return null; }
            else
            {
                Byte? objectByteValue = ToByteNullable(obj, numberStyles);

                if (objectByteValue == null)
                { return null; }
                else
                { return ToBoolean(objectByteValue); }
            }
        }

        public static Byte ToByte(Object obj, NumberStyles numberStyles = NumberStyles.Any)
        {
            if (CheckForNull(obj))
            { return 0; }
            else
            {
                String objectValue = obj.ToString();

                if (objectValue.Trim().Length == 0)
                { return 0; }
                else
                { return Byte.Parse(objectValue, numberStyles); }
            }
        }

        public static Byte? ToByteNullable(Object obj, NumberStyles numberStyles = NumberStyles.Any)
        {
            if (CheckForNull(obj))
            { return null; }
            else
            {
                String objectValue = obj.ToString();

                if (objectValue.Trim().Length == 0)
                { return null; }
                else
                { return Byte.Parse(objectValue, numberStyles); }
            }
        }

        public static DateTime ToDateTime(Object obj, String format = null)
        {
            if (MyDataConverter.CheckForNull(obj))
            { return default(DateTime); }
            else
            {
                String objectValue = obj.ToString().Trim();

                if (objectValue.Length == 0)
                { return default(DateTime); }
                else
                {
                    if (format == null)
                    { return DateTime.Parse(objectValue.ToString()); }
                    else
                    { return DateTime.ParseExact(objectValue.ToString(), format, CultureInfo.InvariantCulture); }
                }
            }
        }

        public static DateTime? ToDateTimeNullable(Object obj, String format = null)
        {
            if (MyDataConverter.CheckForNull(obj))
            { return null; }
            else
            {
                String objectValue = obj.ToString().Trim();

                if (objectValue.Length == 0)
                { return null; }
                else
                {
                    if (format == null)
                    { return DateTime.Parse(objectValue.ToString()); }
                    else
                    { return DateTime.ParseExact(objectValue.ToString(), format, CultureInfo.InvariantCulture); }
                }
            }
        }

        public static Decimal ToDecimal(Object obj, NumberStyles numberStyles = NumberStyles.Any)
        {
            if (CheckForNull(obj))
            { return 0; }
            else
            {
                String objectValue = obj.ToString();

                if (objectValue.Trim().Length == 0)
                { return 0; }
                else
                { return Decimal.Parse(objectValue, numberStyles); }
            }
        }

        public static Decimal? ToDecimalNullable(Object obj, NumberStyles numberStyles = NumberStyles.Any)
        {
            if (CheckForNull(obj))
            { return null; }
            else
            {
                String objectValue = obj.ToString();

                if (objectValue.Trim().Length == 0)
                { return null; }
                else
                { return Decimal.Parse(objectValue, numberStyles); }
            }
        }

        public static Double ToDouble(Object obj, NumberStyles numberStyles = NumberStyles.Any)
        {
            if (CheckForNull(obj))
            { return 0; }
            else
            {
                String objectValue = obj.ToString();

                if (objectValue.Trim().Length == 0)
                { return 0; }
                else
                { return Double.Parse(objectValue, numberStyles); }
            }
        }

        public static Double? ToDoubleNullable(Object obj, NumberStyles numberStyles = NumberStyles.Any)
        {
            if (CheckForNull(obj))
            { return 0; }
            else
            {
                String objectValue = obj.ToString();

                if (objectValue.Trim().Length == 0)
                { return 0; }
                else
                { return Double.Parse(objectValue, numberStyles); }
            }
        }

        public static Int16 ToInt16(Object obj, NumberStyles numberStyles = NumberStyles.Any)
        {
            if (CheckForNull(obj))
            { return 0; }
            else
            {
                String objectValue = obj.ToString();

                if (objectValue.Trim().Length == 0)
                { return 0; }
                else
                { return Int16.Parse(objectValue, numberStyles); }
            }
        }

        public static Int16? ToInt16Nullable(Object obj, NumberStyles numberStyles = NumberStyles.Any)
        {
            if (CheckForNull(obj))
            { return null; }
            else
            {
                String objectValue = obj.ToString();

                if (objectValue.Trim().Length == 0)
                { return null; }
                else
                { return Int16.Parse(objectValue, numberStyles); }
            }
        }

        public static Int32 ToInt32(Object obj, NumberStyles numberStyles = NumberStyles.Any)
        {
            if (CheckForNull(obj))
            { return 0; }
            else
            {
                String objectValue = obj.ToString();

                if (objectValue.Trim().Length == 0)
                { return 0; }
                else
                { return Int32.Parse(objectValue, numberStyles); }
            }
        }

        public static Int32? ToInt32Nullable(Object obj, NumberStyles numberStyles = NumberStyles.Any)
        {
            if (CheckForNull(obj))
            { return null; }
            else
            {
                String objectValue = obj.ToString();

                if (objectValue.Trim().Length == 0)
                { return null; }
                else
                { return Int32.Parse(objectValue, numberStyles); }
            }
        }

        public static Int64 ToInt64(Object obj, NumberStyles numberStyles = NumberStyles.Any)
        {
            if (CheckForNull(obj))
            { return 0; }
            else
            {
                String objectValue = obj.ToString();

                if (objectValue.Trim().Length == 0)
                { return 0; }
                else
                { return Int64.Parse(objectValue, numberStyles); }
            }
        }

        public static Int64? ToInt64Nullable(Object obj, NumberStyles numberStyles = NumberStyles.Any)
        {
            if (CheckForNull(obj))
            { return null; }
            else
            {
                String objectValue = obj.ToString();

                if (objectValue.Trim().Length == 0)
                { return null; }
                else
                { return Int64.Parse(objectValue, numberStyles); }
            }
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
            if (CheckForNull(obj))
            { return default(TimeSpan); }
            else
            {
                String objectValue = obj.ToString();

                if (objectValue.Trim().Length == 0)
                { return default(TimeSpan); }
                else
                { return TimeSpan.Parse(objectValue); }
            }
        }

        public static TimeSpan? ToTimeSpanNullable(Object obj)
        {
            if (CheckForNull(obj))
            { return null; }
            else
            {
                String objectValue = obj.ToString();

                if (objectValue.Trim().Length == 0)
                { return null; }
                else
                { return TimeSpan.Parse(objectValue); }
            }
        }

        public static UInt16 ToUInt16(Object obj, NumberStyles numberStyles = NumberStyles.Any)
        {
            if (CheckForNull(obj))
            { return 0; }
            else
            {
                String objectValue = obj.ToString();

                if (objectValue.Trim().Length == 0)
                { return 0; }
                else
                { return UInt16.Parse(objectValue, numberStyles); }
            }
        }

        public static UInt16? ToUInt16Nullable(Object obj, NumberStyles numberStyles = NumberStyles.Any)
        {
            if (CheckForNull(obj))
            { return null; }
            else
            {
                String objectValue = obj.ToString();

                if (objectValue.Trim().Length == 0)
                { return null; }
                else
                { return UInt16.Parse(objectValue, numberStyles); }
            }
        }

        public static UInt32 ToUInt32(Object obj, NumberStyles numberStyles = NumberStyles.Any)
        {
            if (CheckForNull(obj))
            { return 0; }
            else
            {
                String objectValue = obj.ToString();

                if (objectValue.Trim().Length == 0)
                { return 0; }
                else
                { return UInt32.Parse(objectValue, numberStyles); }
            }
        }

        public static UInt32? ToUInt32Nullable(Object obj, NumberStyles numberStyles = NumberStyles.Any)
        {
            if (CheckForNull(obj))
            { return null; }
            else
            {
                String objectValue = obj.ToString();

                if (objectValue.Trim().Length == 0)
                { return null; }
                else
                { return UInt32.Parse(objectValue, numberStyles); }
            }
        }

        public static UInt64 ToUInt64(Object obj, NumberStyles numberStyles = NumberStyles.Any)
        {
            if (CheckForNull(obj))
            { return 0; }
            else
            {
                String objectValue = obj.ToString();

                if (objectValue.Trim().Length == 0)
                { return 0; }
                else
                { return UInt64.Parse(objectValue, numberStyles); }
            }
        }

        public static UInt64? ToUInt64Nullable(Object obj, NumberStyles numberStyles = NumberStyles.Any)
        {
            if (CheckForNull(obj))
            { return null; }
            else
            {
                String objectValue = obj.ToString();

                if (objectValue.Trim().Length == 0)
                { return null; }
                else
                { return UInt64.Parse(objectValue, numberStyles); }
            }
        }
    }
}