using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Accounting.Areas.Accounting.Models;

namespace Accounting.Areas.Accounting.Controllers
{
    public class ExpensesController : Controller
    {
        AccountingRepository repository = new AccountingRepository();

        public ActionResult Index()
        {
            return View(repository.GetExpenses().ToList());
        }

        public ActionResult Details(int id)
        {
            return View(repository.GetExpense(id));
        }

        public ActionResult Create()
        {
            var expenseTypes = repository.GetExpenseTypes();
            ViewData["ExpenseTypes"] = new SelectList(expenseTypes, "ExpenseTypeId", "Description");

            return View();
        }

        [HttpPost]
        public ActionResult Create(Expense expense)
        {
            var expenseTypes = repository.GetExpenseTypes();
            ViewData["ExpenseTypes"] = new SelectList(expenseTypes, "ExpenseTypeId", "Description");

            if (ModelState.IsValid)
            {
                repository.Add(expense);
                repository.Save();

                return RedirectToAction("Index");
            }

            return View(expense);
        }
 
        public ActionResult Edit(int id)
        {
            var expenseTypes = repository.GetExpenseTypes();
            ViewData["ExpenseTypes"] = new SelectList(expenseTypes, "ExpenseTypeId", "Description");

            return View(repository.GetExpense(id));
        }


        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var expense = repository.GetExpense(id);

            var expenseTypes = repository.GetExpenseTypes();
            ViewData["ExpenseTypes"] = new SelectList(expenseTypes, "ExpenseTypeId", "Description");

            if (TryUpdateModel(expense))
            {
                repository.Save();

                return RedirectToAction("Index");
            }
            else return View(expense);
        }

 
        public ActionResult Delete(int id)
        {
            return View(repository.GetExpense(id));
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var expense = repository.GetExpense(id);
            try
            {
                repository.Remove(expense);
                repository.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(expense);
            }
        }
    }
}
