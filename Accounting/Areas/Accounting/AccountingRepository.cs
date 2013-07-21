using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accounting.Areas.Accounting.Models;

namespace Accounting.Areas.Accounting
{
    public class AccountingRepository
    {
        AccountingContext context = new AccountingContext();

        public IQueryable<Expense> GetExpenses()
        {
            var expenses = from e in context.Expenses
                           select e;

            return expenses;
        }

        public Expense GetExpense(int id)
        {
            return context.Expenses.SingleOrDefault(e => e.ExpenseId == id);
        }

        internal IQueryable<ExpenseType> GetExpenseTypes()
        {
            var expenseTypes = from e in context.ExpenseTypes
                           select e;

            return expenseTypes;
        }


        public IQueryable<Income> GetIncomes()
        {
            var incomes = from i in context.Incomes
                           select i;

            return incomes;
        }

        public Income GetIncome(int id)
        {
            return context.Incomes.SingleOrDefault(i => i.IncomeId == id);
        }

        internal IQueryable<IncomeType> GetIncomeTypes()
        {
            var incomeTypes = from i in context.IncomeTypes
                               select i;

            return incomeTypes;
        }


        internal void Add(Expense expense)
        {
            context.Expenses.Add(expense);
        }

        internal void Add(Income income)
        {
            context.Incomes.Add(income);
        }

        internal void Remove(Expense expense)
        {
            context.Expenses.Remove(expense);
        }

        internal void Remove(Income income)
        {
            context.Incomes.Remove(income);
        }

        public void Save()
        {
            context.SaveChanges();
        }

    }
}