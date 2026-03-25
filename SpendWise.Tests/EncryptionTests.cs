using SpendWise.Core;
using Xunit;

namespace SpendWise.Tests;

public class EncryptionTests
{
    [Fact]
    public void Should_Encrypt_And_Decrypt_Correctly()
    {
        // Arrange (تجهيز)
        var service = new EncryptionService();
        var originalData = "Vodafone Cash";

        // Act (التنفيذ)
        var encrypted = service.Encrypt(originalData);
        var decrypted = service.Decrypt(encrypted);

        // Assert (التأكد)
        Assert.NotEqual(originalData, encrypted); // لازم النص يتغير
        Assert.Equal(originalData, decrypted);    // لازم يرجع لأصله بعد فك التشفير
    }

    [Fact]
    public void Encrypted_Data_Should_Be_Base64()
    {
        var service = new EncryptionService();
        var encrypted = service.Encrypt("123.45");

        // التأكد أن النتيجة نص مشفر بتنسيق Base64 (مش أرقام واضحة)
        bool isBase64 = (encrypted.Length % 4 == 0) && System.Text.RegularExpressions.Regex.IsMatch(encrypted, @"^[a-zA-Z0-9\+/]*={0,3}$");
        Assert.True(isBase64);
    }
}