using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Xml.Linq;
using ATM.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ATM.Data
{
    public class ATMContext : IdentityDbContext
    {
        public ATMContext(DbContextOptions<ATMContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            // Default values
            builder.Entity<Customer>().HasIndex(u => u.CustomerGuid).IsUnique();
            builder.Entity<Customer>().Property(x => x.CustomerGuid).HasDefaultValueSql("NEWID()");
            builder.Entity<Customer>().Property(b => b.CreationTime).HasDefaultValueSql("getdate()");
            builder.Entity<Customer>().Property(b => b.UpdatedTime).ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Save);

            builder.Entity<BankAccount>().HasIndex(u => u.BankAccountGuid).IsUnique();
            builder.Entity<BankAccount>().Property(x => x.BankAccountGuid).HasDefaultValueSql("NEWID()");
            builder.Entity<BankAccount>().Property(b => b.CreationTime).HasDefaultValueSql("getdate()");
            builder.Entity<BankAccount>().Property(b => b.UpdatedTime).ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Save);

            builder.Entity<BankCard>().HasIndex(u => u.BankCardGuid).IsUnique();
            builder.Entity<BankCard>().Property(x => x.BankCardGuid).HasDefaultValueSql("NEWID()");
            builder.Entity<BankCard>().Property(b => b.CreationTime).HasDefaultValueSql("getdate()");
            builder.Entity<BankCard>().Property(b => b.UpdatedTime).ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Save);

            builder.Entity<BalanceHistory>().HasIndex(u => u.BalanceHistoryGuid).IsUnique();
            builder.Entity<BalanceHistory>().Property(x => x.BalanceHistoryGuid).HasDefaultValueSql("NEWID()");
            builder.Entity<BalanceHistory>().Property(b => b.CreationTime).HasDefaultValueSql("getdate()");
            builder.Entity<BalanceHistory>().Property(b => b.UpdatedTime).ValueGeneratedOnAddOrUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Save);
        }

        // Added by TR on 06-Feb-2024 11:38 PM
        // custom entity, override identity user with new columns
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<BankCard> BankCards { get; set; }
        public DbSet<BalanceHistory> BalanceHistories { get; set; }
    }
}
