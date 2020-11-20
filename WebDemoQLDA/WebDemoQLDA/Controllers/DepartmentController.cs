using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebDemoQLDA.IService;
using WebDemoQLDA.Models;

namespace WebDemoQLDA.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IHttpClientService _iHttpClient;
        public DepartmentController(IHttpClientService iHttpClient)
        {
            _iHttpClient = iHttpClient;
        }

        //HttpClient client = new HttpClient();
        public async Task<IActionResult>  Index()
        {
            //try
            //{
            //    HttpClient client = new HttpClient();
            //    client.BaseAddress = new Uri("http://localhost:81/api/");
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    HttpResponseMessage response = client.GetAsync("Department/GetAll").Result;
            //    var data = await response.Content.ReadAsStringAsync();
            //    //var product = !response.IsSuccessStatusCode ? default : ParseResponse<Department>(data);
            //    var product = JsonConvert.DeserializeObject<List<Department>>(data);
            //    return View(product);

            //}
            //catch (Exception ex)
            //{

            //    return View(ex);
            //}
           
            var a = await _iHttpClient.GetAsync<List<Department>>("http://localhost:81/api/Department/GetAll");
            return View(a);

        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection department, string iddepartment)
        {
            string adresbase = "Department/"+ iddepartment.ToString();
            var deparment = await _iHttpClient.PostAsync<long>(adresbase, department);
            return View(deparment);
        }
        private static T ParseResponse<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }
       
    }
}
