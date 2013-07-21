using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Accounting.Areas.Accounting.Models;

namespace Accounting.Areas.Accounting.Controllers
{
    public class IncomesController : Controller
    {
        AccountingRepository repository = new AccountingRepository();

        public ActionResult Index()
        {
            return View(repository.GetIncomes().ToList());
        }

        public ActionResult Details(int id)
        {
            return View(repository.GetIncome(id));
        }

        public ActionResult Create()
        {
            var incomeTypes = repository.GetIncomeTypes();
            ViewData["IncomeTypes"] = new SelectList(incomeTypes, "IncomeTypeId", "Description");

            return View();
        }

        [HttpPost]
        public ActionResult Create(Income Income)
        {
            var incomeTypes = repository.GetIncomeTypes();
            ViewData["IncomeTypes"] = new SelectList(incomeTypes, "IncomeTypeId", "Description");

            if (ModelState.IsValid)
            {
                repository.Add(Income);
                repository.Save();

                return RedirectToAction("Index");
            }

            return View(Income);
        }
 
        public ActionResult Edit(int id)
        {
            var incomeTypes = repository.GetIncomeTypes();
            ViewData["IncomeTypes"] = new SelectList(incomeTypes, "IncomeTypeId", "Description");

            return View(repository.GetIncome(id));
        }


        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var Income = repository.GetIncome(id);

            var incomeTypes = repository.GetIncomeTypes();
            ViewData["IncomeTypes"] = new SelectList(incomeTypes, "IncomeTypeId", "Description");

            if (TryUpdateModel(Income))
            {
                repository.Save();

                return RedirectToAction("Index");
            }
            else return View(Income);
        }

 
        public ActionResult Delete(int id)
        {
            return View(repository.GetIncome(id));
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var Income = repository.GetIncome(id);
            try
            {
                repository.Remove(Income);
                repository.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(Income);
            }
        }
    }
}
