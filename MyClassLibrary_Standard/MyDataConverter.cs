using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MyClassLibrary
{
    public static class MyDataConverter
    {
        private static String ToAscii128(String _ToConvertString)
        {
            if (String.IsNullOrWhiteSpace(_ToConvertString))
            { return null; }
            else
            { return _ToConvertString.Normalize(NormalizationForm.FormKD).Where(x => x < 128).ToArray().ToString(); }
        }

        public static Boolean ToBoolean(Object _Object)
        {
            if (DBNull.Value.Equals(_Object))
            { return false; }
            else
            {
                String _ObjectAsString = _Object.ToString();

                if (String.IsNullOrWhiteSpace(_ObjectAsString))
                { return false; }
                else
                {
                    Boolean _Result;
                    if (Boolean.TryParse(_ObjectAsString, out _Result))
                    { return _Result; }
                    else
                    {
                        if (_ObjectAsString.Equals("0", StringComparison.InvariantCultureIgnoreCase))
                        { return false; }
                        else if (_ObjectAsString.Equals("1", StringComparison.InvariantCultureIgnoreCase))
                        { return true; }
                        else if (_ObjectAsString.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                        { return false; }
                        else if (_ObjectAsString.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                        { return true; }
                        else if (_ObjectAsString.Equals("NO", StringComparison.InvariantCultureIgnoreCase))
                        { return false; }
                        else if (_ObjectAsString.Equals("YES", StringComparison.InvariantCultureIgnoreCase))
                        { return true; }
                        else if (_ObjectAsString.Equals("OFF", StringComparison.InvariantCultureIgnoreCase))
                        { return false; }
                        else if (_ObjectAsString.Equals("ON", StringComparison.InvariantCultureIgnoreCase))
                        { return true; }
                        else
                        { throw new ValueOutOfRangeException("_Object"); }
                    }
                }
            }
        }

        public static Boolean? ToBooleanNullable(Object _Object)
        {
            if (DBNull.Value.Equals(_Object))
            { return null; }
            else
            {
                String _ObjectAsString = _Object.ToString();
                
                if (String.IsNullOrWhiteSpace(_ObjectAsString))
                { return null; }
                else
                {
                    Boolean _Result;
                    if (Boolean.TryParse(_ObjectAsString, out _Result))
                    { return _Result; }
                    else
                    {
                        if (_ObjectAsString.Equals("0", StringComparison.InvariantCultureIgnoreCase))
                        { return false; }
                        else if (_ObjectAsString.Equals("1", StringComparison.InvariantCultureIgnoreCase))
                        { return true; }
                        else if (_ObjectAsString.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                        { return false; }
                        else if (_ObjectAsString.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                        { return true; }
                        else if (_ObjectAsString.Equals("NO", StringComparison.InvariantCultureIgnoreCase))
                        { return false; }
                        else if (_ObjectAsString.Equals("YES", StringComparison.InvariantCultureIgnoreCase))
                        { return true; }
                        else if (_ObjectAsString.Equals("OFF", StringComparison.InvariantCultureIgnoreCase))
                        { return false; }
                        else if (_ObjectAsString.Equals("ON", StringComparison.InvariantCultureIgnoreCase))
                        { return true; }
                        else
                        { throw new ValueOutOfRangeException("_Object"); }
                    }
                }
            }
        }

        public static Byte ToByte(Object _Object, NumberStyles _NumberStyles = NumberStyles.Any, IFormatProvider _FormatProvider = null)
        {
            if (DBNull.Value.Equals(_Object))
            { return 0; }
            else
            {
                String _ObjectAsString = _Object.ToString();

                if (String.IsNullOrWhiteSpace(_ObjectAsString))
                { return 0; }
                else
                { return Byte.Parse(_ObjectAsString, _NumberStyles, _FormatProvider); }
            }
        }

        public static Byte? ToByteNullable(Object _Object, NumberStyles _NumberStyles = NumberStyles.Any, IFormatProvider _FormatProvider = null)
        {
            if (DBNull.Value.Equals(_Object))
            { return null; }
            else
            {
                String _ObjectAsString = _Object.ToString();

                if (String.IsNullOrWhiteSpace(_ObjectAsString))
                { return null; }
                else
                { return Byte.Parse(_ObjectAsString, _NumberStyles, _FormatProvider); }
            }
        }

        public static DateTime ToDateTime(Object _Object, String _Format = null, IFormatProvider _FormatProvider = null)
        {
            if (DBNull.Value.Equals(_Object))
            { return default(DateTime); }
            else
            {
                String _ObjectAsString = _Object.ToString();

                if (String.IsNullOrWhiteSpace(_ObjectAsString))
                { return default(DateTime); }
                else
                {
                    if (_Format == null)
                    { return DateTime.Parse(_ObjectAsString.ToString()); }
                    else
                    {
                        if (_FormatProvider == null)
                        { return DateTime.ParseExact(_ObjectAsString.ToString(), _Format, CultureInfo.InvariantCulture); }
                        else
                        { return DateTime.ParseExact(_ObjectAsString.ToString(), _Format, _FormatProvider); }
                    }
                }
            }
        }

        public static DateTime? ToDateTimeNullable(Object _Object, String _Format = null, IFormatProvider _FormatProvider = null)
        {
            if (DBNull.Value.Equals(_Object))
            { return null; }
            else
            {
                String _ObjectAsString = _Object.ToString();

                if (String.IsNullOrWhiteSpace(_ObjectAsString))
                { return null; }
                else
                {
                    if (_Format == null)
                    { return DateTime.Parse(_ObjectAsString.ToString()); }
                    else
                    {
                        if (_FormatProvider == null)
                        { return DateTime.ParseExact(_ObjectAsString.ToString(), _Format, CultureInfo.InvariantCulture); }
                        else
                        { return DateTime.ParseExact(_ObjectAsString.ToString(), _Format, _FormatProvider); }
                    }
                }
            }
        }

        public static Decimal ToDecimal(Object _Object, NumberStyles _NumberStyles = NumberStyles.Any, IFormatProvider _FormatProvider = null)
        {
            if (DBNull.Value.Equals(_Object))
            { return 0; }
            else
            {
                String _ObjectAsString = _Object.ToString();

                if (String.IsNullOrWhiteSpace(_ObjectAsString))
                { return 0; }
                else
                { return Decimal.Parse(_ObjectAsString, _NumberStyles, _FormatProvider); }
            }
        }

        public static Decimal? ToDecimalNullable(Object _Object, NumberStyles _NumberStyles = NumberStyles.Any, IFormatProvider _FormatProvider = null)
        {
            if (DBNull.Value.Equals(_Object))
            { return null; }
            else
            {
                String _ObjectAsString = _Object.ToString();

                if (String.IsNullOrWhiteSpace(_ObjectAsString))
                { return null; }
                else
                { return Decimal.Parse(_ObjectAsString, _NumberStyles, _FormatProvider); }
            }
        }

        public static Double ToDouble(Object _Object, NumberStyles _NumberStyles = NumberStyles.Any, IFormatProvider _FormatProvider = null)
        {
            if (DBNull.Value.Equals(_Object))
            { return 0; }
            else
            {
                String _ObjectAsString = _Object.ToString();

                if (String.IsNullOrWhiteSpace(_ObjectAsString))
                { return 0; }
                else
                { return Double.Parse(_ObjectAsString, _NumberStyles, _FormatProvider); }
            }
        }

        public static Double? ToDoubleNullable(Object _Object, NumberStyles _NumberStyles = NumberStyles.Any, IFormatProvider _FormatProvider = null)
        {
            if (DBNull.Value.Equals(_Object))
            { return null; }
            else
            {
                String _ObjectAsString = _Object.ToString();

                if (String.IsNullOrWhiteSpace(_ObjectAsString))
                { return null; }
                else
                { return Double.Parse(_ObjectAsString, _NumberStyles, _FormatProvider); }
            }
        }

        public static Int16 ToInt16(Object _Object, NumberStyles _NumberStyles = NumberStyles.Any, IFormatProvider _FormatProvider = null)
        {
            if (DBNull.Value.Equals(_Object))
            { return 0; }
            else
            {
                String _ObjectAsString = _Object.ToString();

                if (String.IsNullOrWhiteSpace(_ObjectAsString))
                { return 0; }
                else
                { return Int16.Parse(_ObjectAsString, _NumberStyles, _FormatProvider); }
            }
        }

        public static Int16? ToInt16Nullable(Object _Object, NumberStyles _NumberStyles = NumberStyles.Any, IFormatProvider _FormatProvider = null)
        {
            if (DBNull.Value.Equals(_Object))
            { return null; }
            else
            {
                String _ObjectAsString = _Object.ToString();

                if (String.IsNullOrWhiteSpace(_ObjectAsString))
                { return null; }
                else
                { return Int16.Parse(_ObjectAsString, _NumberStyles, _FormatProvider); }
            }
        }

        public static Int32 ToInt32(Object _Object, NumberStyles _NumberStyles = NumberStyles.Any, IFormatProvider _FormatProvider = null)
        {
            if (DBNull.Value.Equals(_Object))
            { return 0; }
            else
            {
                String _ObjectAsString = _Object.ToString();

                if (String.IsNullOrWhiteSpace(_ObjectAsString))
                { return 0; }
                else
                { return Int32.Parse(_ObjectAsString, _NumberStyles, _FormatProvider); }
            }
        }

        public static Int32? ToInt32Nullable(Object _Object, NumberStyles _NumberStyles = NumberStyles.Any, IFormatProvider _FormatProvider = null)
        {
            if (DBNull.Value.Equals(_Object))
            { return null; }
            else
            {
                String _ObjectAsString = _Object.ToString();

                if (String.IsNullOrWhiteSpace(_ObjectAsString))
                { return null; }
                else
                { return Int32.Parse(_ObjectAsString, _NumberStyles, _FormatProvider); }
            }
        }

        public static Int64 ToInt64(Object _Object, NumberStyles _NumberStyles = NumberStyles.Any, IFormatProvider _FormatProvider = null)
        {
            if (DBNull.Value.Equals(_Object))
            { return 0; }
            else
            {
                String _ObjectAsString = _Object.ToString();

                if (String.IsNullOrWhiteSpace(_ObjectAsString))
                { return 0; }
                else
                { return Int64.Parse(_ObjectAsString, _NumberStyles, _FormatProvider); }
            }
        }

        public static Int64? ToInt64Nullable(Object _Object, NumberStyles _NumberStyles = NumberStyles.Any, IFormatProvider _FormatProvider = null)
        {
            if (DBNull.Value.Equals(_Object))
            { return null; }
            else
            {
                String _ObjectAsString = _Object.ToString();

                if (String.IsNullOrWhiteSpace(_ObjectAsString))
                { return null; }
                else
                { return Int64.Parse(_ObjectAsString, _NumberStyles, _FormatProvider); }
            }
        }

        public static String ToString(Object _Object, Boolean _Trim = true)
        {
            if (DBNull.Value.Equals(_Object))
            { return null; }
            else
            {
                String _ObjectAsString = _Object.ToString();

                if (String.IsNullOrWhiteSpace(_ObjectAsString))
                { return null; }
                { return (_Trim ? _ObjectAsString.Trim(): _ObjectAsString); }
            }
        }

        public static TimeSpan ToTimeSpan(Object _Object, IFormatProvider _FormatProvider = null)
        {
            if (DBNull.Value.Equals(_Object))
            { return default(TimeSpan); }
            else
            {
                String _ObjectAsString = _Object.ToString();

                if (String.IsNullOrWhiteSpace(_ObjectAsString))
                { return default(TimeSpan); }
                else
                { return TimeSpan.Parse(_ObjectAsString, _FormatProvider); }
            }
        }

        public static TimeSpan? ToTimeSpanNullable(Object _Object, IFormatProvider _FormatProvider = null)
        {
            if (DBNull.Value.Equals(_Object))
            { return null; }
            else
            {
                String _ObjectAsString = _Object.ToString();

                if (String.IsNullOrWhiteSpace(_ObjectAsString))
                { return null; }
                else
                { return TimeSpan.Parse(_ObjectAsString, _FormatProvider); }
            }
        }

        public static UInt16 ToUInt16(Object _Object, NumberStyles _NumberStyles = NumberStyles.Any, IFormatProvider _FormatProvider = null)
        {
            if (DBNull.Value.Equals(_Object))
            { return 0; }
            else
            {
                String _ObjectAsString = _Object.ToString();

                if (String.IsNullOrWhiteSpace(_ObjectAsString))
                { return 0; }
                else
                { return UInt16.Parse(_ObjectAsString, _NumberStyles, _FormatProvider); }
            }
        }

        public static UInt16? ToUInt16Nullable(Object _Object, NumberStyles _NumberStyles = NumberStyles.Any, IFormatProvider _FormatProvider = null)
        {
            if (DBNull.Value.Equals(_Object))
            { return null; }
            else
            {
                String _ObjectAsString = _Object.ToString();

                if (String.IsNullOrWhiteSpace(_ObjectAsString))
                { return null; }
                else
                { return UInt16.Parse(_ObjectAsString, _NumberStyles, _FormatProvider); }
            }
        }

        public static UInt32 ToUInt32(Object _Object, NumberStyles _NumberStyles = NumberStyles.Any, IFormatProvider _FormatProvider = null)
        {
            if (DBNull.Value.Equals(_Object))
            { return 0; }
            else
            {
                String _ObjectAsString = _Object.ToString();

                if (String.IsNullOrWhiteSpace(_ObjectAsString))
                { return 0; }
                else
                { return UInt32.Parse(_ObjectAsString, _NumberStyles, _FormatProvider); }
            }
        }

        public static UInt32? ToUInt32Nullable(Object _Object, NumberStyles _NumberStyles = NumberStyles.Any, IFormatProvider _FormatProvider = null)
        {
            if (DBNull.Value.Equals(_Object))
            { return null; }
            else
            {
                String _ObjectAsString = _Object.ToString();

                if (String.IsNullOrWhiteSpace(_ObjectAsString))
                { return null; }
                else
                { return UInt32.Parse(_ObjectAsString, _NumberStyles, _FormatProvider); }
            }
        }

        public static UInt64 ToUInt64(Object _Object, NumberStyles _NumberStyles = NumberStyles.Any, IFormatProvider _FormatProvider = null)
        {
            if (DBNull.Value.Equals(_Object))
            { return 0; }
            else
            {
                String _ObjectAsString = _Object.ToString();

                if (String.IsNullOrWhiteSpace(_ObjectAsString))
                { return 0; }
                else
                { return UInt64.Parse(_ObjectAsString, _NumberStyles, _FormatProvider); }
            }
        }

        public static UInt64? ToUInt64Nullable(Object _Object, NumberStyles _NumberStyles = NumberStyles.Any, IFormatProvider _FormatProvider = null)
        {
            if (DBNull.Value.Equals(_Object))
            { return null; }
            else
            {
                String _ObjectAsString = _Object.ToString();

                if (String.IsNullOrWhiteSpace(_ObjectAsString))
                { return null; }
                else
                { return UInt64.Parse(_ObjectAsString, _NumberStyles, _FormatProvider); }
            }
        }
    }
}