using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WebDemoQLDA.IService;
using WebDemoQLDA.Models;

namespace WebDemoQLDA.Controllers
{
    public class FacultyController : Controller
    {
        private readonly IHttpClientService _iHttpClient;
        public FacultyController(IHttpClientService iHttpClient)
        {
            _iHttpClient = iHttpClient;
        }
        // GET: FacultyController
        public async Task<ActionResult> Index()
        {
            var fac = await _iHttpClient.GetAsync<List<Faculty>>("Faculty/GetAll");
            return View(fac);
        }

        // GET: FacultyController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            string url = "Faculty/" + id.ToString();
            var faculty = await _iHttpClient.GetAsync<Faculty>(url);
            return View(faculty);
        }

        // GET: FacultyController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FacultyController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FacultyMeta collection)
        {
            try
            {
                var Result = await _iHttpClient.PostAsync<FacultyMeta>("Faculty", collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

        }

        // GET: FacultyController/Edit/5
        public ActionResult Edit(string id)
        {
            return View();
        }

        // POST: FacultyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, FacultyMeta name)
        {
            try
            {
                string uri = "Faculty/Update/";
                string Url = uri.ToString() + id.ToString()+"/"+ name.NameFaculty.ToString();
                var Result = await _iHttpClient.PutAsync<FacultyMeta>(Url,name);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FacultyController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FacultyController/Delete/5
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
