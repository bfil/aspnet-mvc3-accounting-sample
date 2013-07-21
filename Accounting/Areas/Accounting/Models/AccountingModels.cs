using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Accounting.Areas.Accounting.Models
{
    public class Expense
    {
        [ScaffoldColumn(false)]
        public int ExpenseId { get; set; }
        [Required(ErrorMessage="The Description field is required.")]
        public string Description { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Date { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F}")]
        public decimal Amount { get; set; }

        public virtual ExpenseType ExpenseType { get; set; }
        [DisplayName("Expense Type")]
        [Required(ErrorMessage="The ExpenseType field is required.")]
        public int ExpenseTypeId { get; set; }
    }

    public class ExpenseType
    {
        [ScaffoldColumn(false)]
        public int ExpenseTypeId { get; set; }
        [Required(ErrorMessage = "The Description field is required.")]
        public string Description { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }
    }

    public class Income
    {
        [ScaffoldColumn(false)]
        public int IncomeId { get; set; }
        [Required(ErrorMessage = "The Description field is required.")]
        public string Description { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Date { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F}")]
        public decimal Amount { get; set; }

        public virtual IncomeType IncomeType { get; set; }
        [DisplayName("Income Type")]
        [Required(ErrorMessage = "The IncomeType field is required.")]
        public int IncomeTypeId { get; set; }
    }

    public class IncomeType
    {
        [ScaffoldColumn(false)]
        public int IncomeTypeId { get; set; }
        [Required(ErrorMessage = "The Description field is required.")]
        public string Description { get; set; }

        public virtual ICollection<Income> Incomes { get; set; }
    }


    public class AccountingContext : DbContext
    {
        public AccountingContext() : base("AccountingDB") { }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<IncomeType> IncomeTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExpenseType>().Property(e => e.Description).IsRequired();
            modelBuilder.Entity<IncomeType>().Property(i => i.Description).IsRequired();
            modelBuilder.Entity<Expense>().MapSingleType().ToTable("Expenses");
            modelBuilder.Entity<ExpenseType>().MapSingleType().ToTable("ExpenseTypes");
            modelBuilder.Entity<Income>().MapSingleType().ToTable("Incomes");
            modelBuilder.Entity<IncomeType>().MapSingleType().ToTable("IncomeTypes");

            modelBuilder.Entity<Expense>().HasRequired(e => e.ExpenseType).WithMany(e => e.Expenses);
            modelBuilder.Entity<Income>().HasRequired(i => i.IncomeType).WithMany(i => i.Incomes);

        }
    }

    public class AccountingInitializer : RecreateDatabaseIfModelChanges<AccountingContext>
    {
        protected override void Seed(AccountingContext context)
        {
            var expenses = new List<Expense>
            {
                new Expense { Description = "Chinese Lunch", Amount = 10.00M, Date = new DateTime(2010,07,4),
                              ExpenseType = new ExpenseType { Description = "Lunch" } },

                new Expense { Description = "Car Repair", Amount = 320.00M, Date = new DateTime(2010,07,23),
                              ExpenseType = new ExpenseType { Description = "Car" } }

            };

            expenses.ForEach(e => context.Expenses.Add(e));

            var incomes = new List<Income>
            {
                new Income { Description = "July Remuneration", Amount = 1800.00M, Date = new DateTime(2010,07,15),
                              IncomeType = new IncomeType { Description = "Remuneration" } }

            };

            incomes.ForEach(i => context.Incomes.Add(i));

        }
    }


}