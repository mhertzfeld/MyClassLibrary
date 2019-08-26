using System;
using System.Collections.Generic;
using System.Linq;

namespace MyClassLibrary.Printing.ZPL
{
    // Code sourced from Dysnomian's post from https://stackoverflow.com/questions/13040822/unicode-characters-on-zpl-printer
    public static class ZPL_Encoder
    {
        // From the wikipedia page on utf8 encoding - https://en.wikipedia.org/wiki/UTF-8
        private const int _Last1ByteCodePointByte1 = 0x7F;
        private const int _First2ByteCodePointByte1 = 0xC0;
        private const int _Last2ByteCodePointByte1 = 0xDF;
        private const int _Last3ByteCodePointByte1 = 0xEF;
        private const int _Last4ByteCodePointByte1 = 0xF7;
        private const int _FirstMultiByteCodePointByte2 = 0x80;
        private const int _LastMultiByteCodePointByte2 = 0xBF;
        private const char _ZplMultiByteEscapeCharacter = '_';

        /// <summary>
        /// Encodes a sequence of utf8 bytes for printing with the ZPL language, this means escaping multi-byte characters with an underscore ('_') followed by the hex code
        /// for each byte in the multi-byte characters.
        /// </summary>
        /// <param name="utf8Bytes">The bytes that make up the entire string, including bytes that need to be encoded and bytes that can be printed as-is.</param>
        /// <returns>A string for printing with the ZPL language. Ie all multi-byte characters escaped with an underscore ('_') followed by the hex code for each byte.</returns>
        /// <throws><see cref="ArgumentException"/> when <paramref name="utf8Bytes"/> isn't a valid utf8 encoding of a string.</throws>
        /// <remarks>
        /// Plan is to figure out how many bytes this character (code point) takes up, and if it's a 1 byte character, just use the character, but otherwise since it's a multi-byte 
        /// character then use an underscore ('_') followed by the hex encoded byte and each other byte in this code point will also be encoded. If we start the loop but have bytes 
        /// remaining in the current code point we know to hex encode this byte and continue.
        /// </remarks>
        public static string EncodeUtf8BytesForZPLIIPrinting(byte[] utf8Bytes)
        {
            var contentWithMultiByteCharsEscaped = new List<char>();

            var multiByteCodePoint = new List<char>();
            var remainingBytesInCurrentCodePoint = 0;
            string errorMessage = null;

            foreach (byte utf8Byte in utf8Bytes)
            {
                if (remainingBytesInCurrentCodePoint > 0)
                {
                    if (utf8Byte < _FirstMultiByteCodePointByte2 || utf8Byte > _LastMultiByteCodePointByte2)
                    {
                        errorMessage = $"The byte {utf8Byte.ToString("X2")} is not a valid as the second or later byte of a multi-byte utf8 character (codepoint).";
                        break;
                    }

                    multiByteCodePoint.Add(_ZplMultiByteEscapeCharacter);
                    AddHexValuesToListFromByte(multiByteCodePoint, utf8Byte);
                    remainingBytesInCurrentCodePoint--;
                    continue; // continue since we've dealt with this byte and don't want to flow on.
                }

                if (multiByteCodePoint.Any())
                {
                    foreach (char c in multiByteCodePoint) contentWithMultiByteCharsEscaped.Add(c);
                    multiByteCodePoint.Clear();
                    // flow on to loop to see what to do with the current byte.
                }

                if (utf8Byte <= _Last1ByteCodePointByte1)
                {
                    // 1 byte - no escaping
                    contentWithMultiByteCharsEscaped.Add((char)utf8Byte);
                }
                else if (utf8Byte >= _First2ByteCodePointByte1 && utf8Byte <= _Last2ByteCodePointByte1)
                {
                    // 2 bytes
                    multiByteCodePoint.Add(_ZplMultiByteEscapeCharacter);
                    AddHexValuesToListFromByte(multiByteCodePoint, utf8Byte);
                    remainingBytesInCurrentCodePoint = 1;
                }
                else if (utf8Byte <= _Last3ByteCodePointByte1)
                {
                    // 3 bytes
                    multiByteCodePoint.Add(_ZplMultiByteEscapeCharacter);
                    AddHexValuesToListFromByte(multiByteCodePoint, utf8Byte);
                    remainingBytesInCurrentCodePoint = 2;
                }
                else if (utf8Byte <= _Last4ByteCodePointByte1)
                {
                    // 4 bytes
                    multiByteCodePoint.Add(_ZplMultiByteEscapeCharacter);
                    AddHexValuesToListFromByte(multiByteCodePoint, utf8Byte);
                    remainingBytesInCurrentCodePoint = 3;
                }
                else
                {
                    errorMessage = $"The byte {utf8Byte.ToString("X2")} is not a valid as the first byte of a utf8 character.";
                    break;
                }
            }

            // if the last char was multiByte add it now.
            if (multiByteCodePoint.Any())
            {
                foreach (var c in multiByteCodePoint) contentWithMultiByteCharsEscaped.Add(c);
                multiByteCodePoint.Clear();
            }

            if (remainingBytesInCurrentCodePoint != 0 && errorMessage == null)
            {
                errorMessage = $"The last character didn't have enough bytes to finish the codepoint. It was a multi-byte character that needed {remainingBytesInCurrentCodePoint}" +
                    $" more byte{(remainingBytesInCurrentCodePoint == 1 ? null : "s")}.";
            }

            if (errorMessage != null)
            {
                throw new ArgumentException($"The byte array was not a valid byte array for a utf8 string: {errorMessage}", nameof(utf8Bytes));
            }

            return new string(contentWithMultiByteCharsEscaped.ToArray());


            void AddHexValuesToListFromByte(List<char> list, byte @byte)
            {
                // A byte is <= 255 so will always fit in a 2-digit hex number, hence the 2 in "X2". The X means hex.
                foreach (char c in @byte.ToString("X2"))
                {
                    list.Add(c);
                }
            }
        }
    }
}