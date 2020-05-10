using System;
using System.Collections.Generic;
using System.Text;

namespace Scrambler
{
    /// <summary>
    /// Class that holds the parameters for the encoding/decoding mechanism.
    /// </summary>
    public class ScramblerOptions
    {
        private const int XorMinValue = 0;
        private const int XorMaxValue = 0x7fffffff;
        private const int ShiftMinValue = 1;
        private const int ShiftMaxValue = 31;

        public static ScramblerOptions Default = new ScramblerOptions(
            xor1: 0x25a5a5a5,
            shift1: 7,
            xor2: 0x58230a67,
            shift2: 23,
            xor3: 0x71eba836,
            shift3: 13,
            hashXor: 0x10264fe8,
            hashShift: 9,
            hashPrefixLength: 3
        );

        public ScramblerOptions(int xor1, int shift1, int xor2, int shift2, int xor3,
            int shift3, int hashXor, int hashShift, int hashPrefixLength)
        {
            if (xor1 < XorMinValue || xor1 > XorMaxValue) throw new ArgumentException($"'{nameof(xor1)}' must be a value between { XorMinValue } and {XorMaxValue}");
            if (xor2 < XorMinValue || xor2 > XorMaxValue) throw new ArgumentException($"'{nameof(xor2)}' must be a value between { XorMinValue } and {XorMaxValue}");
            if (xor3 < XorMinValue || xor3 > XorMaxValue) throw new ArgumentException($"'{nameof(xor3)}' must be a value between { XorMinValue } and {XorMaxValue}");
            if (shift1 < ShiftMinValue || shift1 > ShiftMaxValue) throw new ArgumentException($"'{nameof(shift1)}' must be a value between { ShiftMinValue } and {ShiftMaxValue}");
            if (shift2 < ShiftMinValue || shift2 > ShiftMaxValue) throw new ArgumentException($"'{nameof(shift2)}' must be a value between { ShiftMinValue } and {ShiftMaxValue}");
            if (shift3 < ShiftMinValue || shift3 > ShiftMaxValue) throw new ArgumentException($"'{nameof(shift3)}' must be a value between { ShiftMinValue } and {ShiftMaxValue}");
            if (hashShift < ShiftMinValue || hashShift > ShiftMaxValue) throw new ArgumentException($"'{nameof(hashShift)}' must be a value between { ShiftMinValue } and {ShiftMaxValue}");

            _xor1 = xor1;
            _xor2 = xor2;
            _xor3 = xor3;
            _shift1 = shift1;
            _shift2 = shift2;
            _shift3 = shift3;
            _hashXor = hashXor;
            _hashShift = hashShift;
            _hashPrefixLength = hashPrefixLength;
        }

        private readonly int _xor1;
        private readonly int _xor2;
        private readonly int _xor3;

        private readonly int _shift1;
        private readonly int _shift2;
        private readonly int _shift3;

        private readonly int _hashXor;
        private readonly int _hashShift;
        private readonly int _hashPrefixLength;

        public int Xor1 => _xor1;
        public int Shift1 => _shift1;
        public int Xor2 => _xor2;
        public int Shift2 => _shift2;
        public int Xor3 => _xor3;
        public int Shift3 => _shift3;
        public int HashXor => _hashXor;
        public int HashShift => _hashShift;
        public int HashPrefixLength => _hashPrefixLength;
    }
}
