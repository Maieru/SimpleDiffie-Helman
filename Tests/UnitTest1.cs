namespace Tests
{
    public class Tests
    {
        public class CeaserEncoderTest
        {
            [Theory]
            [InlineData(1, "abc", "bcd")]
            [InlineData(1, "xyz", "yza")]
            [InlineData(1, "a b c", "b c d")]
            [InlineData(7, "abc", "hij")]
            [InlineData(7, "xyz", "efg")]
            [InlineData(7, "A b C", "H i J")]
            [InlineData(12, "This is a full text i hope it works", "Ftue ue m rgxx fqjf u tabq uf iadwe")]
            [InlineData(-1, "abc", "zab")]
            [InlineData(-1, "a b c", "z a b")]
            [InlineData(-7, "abc", "tuv")]
            [InlineData(-12, "This is a full text i hope it works", "Hvwg wg o tizz hslh w vcds wh kcfyg")]
            [InlineData(0, "abc", "abc")]
            [InlineData(0, "xyz", "xyz")]
            public void Decode_GivenPlainText_ReturnsDecodedText(int shift, string plainText, string expectedResult)
            {
                var encoder = new CeaserCypherEncoder(shift);
                var result = encoder.Encode(plainText);

                Assert.Equal(expectedResult, result);
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData(" ")]
            public void Encode_WhenCalledWithInvalidPlainText_ThrowsArgumentException(string plainText)
            {
                var encoder = new CeaserCypherEncoder(3);
                Assert.Throws<ArgumentException>(() => encoder.Encode(plainText));
            }

            [Theory]
            [InlineData("123")]
            [InlineData("!@#")]
            [InlineData("abc123")]
            public void Encode_WhenCalledWithNonLetterPlainText_ThrowsArgumentException(string plainText)
            {
                var encoder = new CeaserCypherEncoder(3);
                Assert.Throws<ArgumentException>(() => encoder.Encode(plainText));
            }
        }
}