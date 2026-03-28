using System.ComponentModel.DataAnnotations;

namespace SpendWise.Core.Entities
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string EncryptedAmount { get; set; } // المبلغ مشفر (لحماية الخصوصية)

        [Required]
        public string EncryptedVendor { get; set; } // اسم التاجر مشفر

        public string Currency { get; set; } = "EGP";

        public DateTime TransactionDate { get; set; } = DateTime.Now;

        public string RawSmsMessage { get; set; } // بنخزن الرسالة الأصلية عشان لو احتجنا نراجعها

        public string Category { get; set; } = "Uncategorized"; // تصنيف المصروفات
    }
}
