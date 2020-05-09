using System;
using System.Collections.Generic;
using System.Text;

namespace Scrambler
{
    public class Scrambler
    {
        private readonly ScramblerOptions _options;

        public Scrambler(ScramblerOptions options = null)
        {
            _options = options ?? ScramblerOptions.Default;
        }

        private int BitRotate(int value, int direction)
        {
            if (direction < 0)
                direction = 31 + direction;

            int ms = value;
            value <<= direction;
            value |= ((ms >> (31 - direction)) & ((1 << direction) - 1));
            value &= 0x7fffffff;

            return value;
        }

        private int ByteSwap(int value)
        {
            int b = value;
            value = ((b >> 8) & 0x00ff);
            value |= ((b & 0x00ff) << 8);
            value |= ((b >> 7) & 0x00fe0000);
            value |= ((b & 0x00fe0000) << 7);
            value |= (b & 0x010000);
            return value;
        }

        private string HashNumToChar(int value, int length)
        {
            string hash = String.Empty;
            value ^= _options.HashXor;

            do
            {
                value ^= _options.HashXor;
                value = BitRotate(value, _options.HashShift);
                int c = ((value >> 24) & 0x1ff) + ((value >> 16) & 0x1ff) + ((value >> 8) & 0x1ff) + (value & 0x1ff);
                c %= 26;
                c += 65;
                char newChar = (char)c;
                hash += newChar;
                length--;
            } while (length > 0);

            return hash;
        }

        public string Encode(int number) {
            int value = number;

            int ls = (value & 0x0f) + 1;
            value ^= (ls << ((21 % ls) + 4));
            value ^= (ls << ((19 % ls) + 4));
            value ^= (ls << ((17 % ls) + 4));
            value ^= (ls << ((13 % ls) + 4));

            value ^= _options.Xor1;
            value = BitRotate(value, _options.Shift1);
            value ^= _options.Xor2;
            value = BitRotate(value, _options.Shift2);
            value ^= _options.Xor3;
            value = BitRotate(value, _options.Shift3);

            value = ByteSwap(value);

            return String.Format("{0}-{1}", HashNumToChar(value, _options.HashPrefixLength), value);
        }

        public int Decode (string encodedText) {
            string prefixo = encodedText.Substring(0, _options.HashPrefixLength);
            string valor = encodedText.Substring(_options.HashPrefixLength + 1);
            int convertedValue;
            Int32.TryParse(valor, out convertedValue);

            if (!String.IsNullOrEmpty(prefixo) && (HashNumToChar(convertedValue, _options.HashPrefixLength).Equals(prefixo, StringComparison.CurrentCultureIgnoreCase)))
            {
                convertedValue = ByteSwap(convertedValue);

                convertedValue = BitRotate(convertedValue, -_options.Shift3);
                convertedValue ^= _options.Xor3;
                convertedValue = BitRotate(convertedValue, -_options.Shift2);
                convertedValue ^= _options.Xor2;
                convertedValue = BitRotate(convertedValue, -_options.Shift1);
                convertedValue ^= _options.Xor1;

                int ls = (convertedValue & 0x0f) + 1;
                convertedValue ^= (ls << ((13 % ls) + 4));
                convertedValue ^= (ls << ((17 % ls) + 4));
                convertedValue ^= (ls << ((19 % ls) + 4));
                convertedValue ^= (ls << ((21 % ls) + 4));
            }

            return convertedValue;
        }
    }
}
