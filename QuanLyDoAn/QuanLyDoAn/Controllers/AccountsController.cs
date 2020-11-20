using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyDoAn.Models;
using System.Web.Security;

namespace QuanLyDoAn.Controllers
{
    public class AccountsController : Controller
    {
        private QLDADbContext db = new QLDADbContext();

        // GET: Accounts
        public ActionResult Index()
        {
            return View(db.Accounts.ToList());
        }

        // GET: Accounts/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accounts account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: Accounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserName,Password,ConfirmPassWord,Email,CreationTime,EmailConfirmed,IsDelete,DeleteTime,RoleID")] Accounts account)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(account);
        }

        // GET: Accounts/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accounts account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserName,Password,Email,CreationTime,EmailConfirmed,IsDelete,DeleteTime,RoleID")] Accounts account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // GET: Accounts/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accounts account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Accounts account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (CheckSession() == 1)
            {
                return RedirectToAction("Index", "Home_Ad", new { Areas = "Admins" });
            }
            else if (CheckSession() == 2)
            {
                return RedirectToAction("Index", "Home_Le", new { Areas = "Lectures" });
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [AllowAnonymous]
        [HttpPost]  
        public ActionResult Login(Accounts acc, string returnUrl)
        {
            StringProcess strPro = new StringProcess();
            try
            {
                if (!string.IsNullOrEmpty(acc.UserName) && !string.IsNullOrEmpty(acc.Password))
                {
                    
                    using (var db = new QLDADbContext())
                    {
                        var passToMD5 = strPro.GetMD5(acc.Password);
                        var account = db.Accounts.Where(m => m.UserName.Equals(acc.UserName) && m.Password.Equals(passToMD5)).Count();
                        if (account == 1)
                        {
                            FormsAuthentication.SetAuthCookie(acc.UserName, false);
                            Session["idUser"] = acc.UserName;
                            Session["roleUser"] = acc.RoleID;
                            Response.Cookies.Add(new HttpCookie("userCookie", acc.UserName));
                            Response.Cookies.Add(new HttpCookie("roleCookie", acc.RoleID));
                            return RedirectToLocal(returnUrl);
                        }
                        ModelState.AddModelError("", "Thông tin đăng nhập chưa chính xác");
                    }
                }
                ModelState.AddModelError("", "Username and password is required.");
            }
            catch
            {
                ModelState.AddModelError("", "Hệ thống đang được bảo trì, vui lòng liên hệ với quản trị viên");
            }
            return View(acc);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["idUser"] = null;
            return RedirectToAction("Login, Account");
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || returnUrl == "/")
            {
                if (CheckSession() == 1)
                {
                    return RedirectToAction("Index", "Home_Ad", new { Areas = "Admins" });
                }
                else if (CheckSession() == 2)
                {
                    return RedirectToAction("Index", "Home_Le", new { Areas = "Lectures" });
                }
            }
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home_Ad");
            }
        }
        private int CheckSession()
        {
            using (var db = new QLDADbContext())
            {
                var user = HttpContext.Session["idUser"];
                if (user != null)
                {
                    var role = db.Accounts.Find(user.ToString()).RoleID;
                    if (role != null)
                    {
                        if (role.ToString() == "Admin")
                        {
                            return 1;
                        }
                        else if (role.ToString() == "Lecture")
                        {
                            return 2;
                        }
                    }
                }
            }
            return 0;
        }
        public ActionResult Demoling()
        {
            var listAcc = (from acc in db.Accounts
                           join role in db.Roles
                           on acc.RoleID equals role.RoleID
                           select new
                           {
                               acc.UserName,
                               role.RoleName
                           }).ToList();
            return View(listAcc);
        }
    }
}
