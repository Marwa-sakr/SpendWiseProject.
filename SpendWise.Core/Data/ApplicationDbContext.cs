using Microsoft.EntityFrameworkCore;
using SpendWise.Core.Entities;

namespace SpendWise.Core.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // هنا بنعرف الجدول اللي هيتكريه في الداتابيز
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // نقطة احترافية (10/10): تأمين البيانات
            // بنحدد طول الحقول المشفرة عشان الـ Database Performance
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.EncryptedAmount).IsRequired();
                entity.Property(e => e.EncryptedVendor).IsRequired();
                entity.Property(e => e.Currency).HasMaxLength(10);
            });
        }
    }
}