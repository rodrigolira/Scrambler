using System;
using Xunit;

namespace Scrambler.Tests
{
    public class ScramblerTests
    {
        private const int NumberOfExecutions = 1000000;

        [Fact]
        public void EncodedTextEqualsToDecodedNumber()
        {
            Random random = new Random();
            Scrambler scrambler = new Scrambler();

            int timesExecuted = 0;

            do
            {

                int originalNumber = random.Next();
                originalNumber &= 0x7fffffff;

                string encodedText = scrambler.Encode(originalNumber);
                int decodedNumber = scrambler.Decode(encodedText);

                Assert.Equal(decodedNumber, originalNumber);

            } while (++timesExecuted < NumberOfExecutions);
        }

        [Fact]
        public void EncodedTextEqualsToDecodedNumberWhenPassingOptions()
        {
            Random random = new Random();
            Scrambler scrambler = new Scrambler(new ScramblerOptions(
                xor1: 0x2a31eb88,
                shift1: 22,
                xor2: 0x4cde82ed,
                shift2: 5,
                xor3: 0x1bd4f2dc,
                shift3: 16,
                hashXor: 0x6fc70a4d,
                hashShift: 4,
                hashPrefixLength: 3
            ));

            int timesExecuted = 0;

            do
            {

                int originalNumber = random.Next();
                originalNumber &= 0x7fffffff;

                string encodedText = scrambler.Encode(originalNumber);
                int decodedNumber = scrambler.Decode(encodedText);

                Assert.Equal(decodedNumber, originalNumber);

            } while (++timesExecuted < NumberOfExecutions);
        }
    }
}
