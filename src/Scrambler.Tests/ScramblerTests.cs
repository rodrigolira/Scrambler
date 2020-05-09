using System;
using Xunit;

namespace Scrambler.Tests
{
    public class ScramblerTests
    {
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

            } while (++timesExecuted < 100000);
        }
    }
}
