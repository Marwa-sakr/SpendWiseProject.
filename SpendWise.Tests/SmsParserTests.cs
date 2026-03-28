using Xunit;
using SpendWise.Core;

namespace SpendWise.Tests
{
    public class SmsParserTests
    {
        [Fact]
        public void Should_Parse_And_Return_Encrypted_Data()
        {
            // Arrange
            var encryption = new EncryptionService();
            var parser = new SmsParser(encryption);
            var sms = "Purchased 550.75 EGP at Amazon on 25/03/2026";

            // Act
            var (encAmount, encVendor) = parser.ParseAndEncrypt(sms);

            // Assert
            Assert.NotEqual("550.75", encAmount);
            Assert.NotEqual("Amazon", encVendor);

            Assert.Equal("550.75", encryption.Decrypt(encAmount));
            Assert.Equal("Amazon", encryption.Decrypt(encVendor));
        }
    }
}