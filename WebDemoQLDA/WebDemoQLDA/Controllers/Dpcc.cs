using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemoQLDA.Controllers
{
    public class Dpcc : Controller
    {
        // GET: Dpcc
        public ActionResult Index()
        {
            return View();
        }

        // GET: Dpcc/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Dpcc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dpcc/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Dpcc/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Dpcc/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Dpcc/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Dpcc/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
