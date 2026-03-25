using System.Text.RegularExpressions;

namespace SpendWise.Core;

public class SmsParser
{
    private readonly EncryptionService _encryptionService;
    private const string CibPattern = @"Purchased\s+(?<amount>[\d\.]+)\s+(?<currency>\w+)\s+at\s+(?<vendor>.+?)\s+on";

    // بنمرر خدمة التشفير للمحلل عند إنشائه
    public SmsParser(EncryptionService encryptionService)
    {
        _encryptionService = encryptionService;
    }

    public (string encryptedAmount, string encryptedVendor) ParseAndEncrypt(string message)
    {
        var match = Regex.Match(message, CibPattern);
        if (match.Success)
        {
            string rawAmount = match.Groups["amount"].Value;
            string rawVendor = match.Groups["vendor"].Value.Trim();

            // هنا بيحصل السحر: التشفير قبل العودة بالبيانات
            return (
                _encryptionService.Encrypt(rawAmount),
                _encryptionService.Encrypt(rawVendor)
            );
        }
        throw new Exception("Format not recognized!");
    }
}