using System.Security.Cryptography;
using System.Text;

namespace SpendWise.Core;

public class EncryptionService
{
    // مفتاح التشفير (32 حرف = 256 بت) - ده "القفل" بتاعنا
    private static readonly byte[] Key = Encoding.UTF8.GetBytes("A6789012345678901234567890123456"); 
    // متجه التهيئة (16 حرف = 128 بت) - لزيادة الأمان
    private static readonly byte[] Iv = Encoding.UTF8.GetBytes("A123456789012345"); 

    // وظيفة التشفير: تحول النص الواضح لنص غير مفهوم
    public string Encrypt(string plainText)
    {
        if (string.IsNullOrEmpty(plainText)) return plainText;

        using Aes aes = Aes.Create();
        aes.Key = Key;
        aes.IV = Iv;

        var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
        using (var sw = new StreamWriter(cs))
        {
            sw.Write(plainText);
        }
        return Convert.ToBase64String(ms.ToArray());
    }

    // وظيفة فك التشفير: ترجع النص لأصله
    public string Decrypt(string cipherText)
    {
        if (string.IsNullOrEmpty(cipherText)) return cipherText;

        using Aes aes = Aes.Create();
        aes.Key = Key;
        aes.IV = Iv;

        var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        using var ms = new MemoryStream(Convert.FromBase64String(cipherText));
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var sr = new StreamReader(cs);
        
        return sr.ReadToEnd();
    }
}