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
    }
}
