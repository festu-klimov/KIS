using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Telephony_selling.Models;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using System.Data.Entity;

namespace Telephony_selling.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private TelephonyContext db = new TelephonyContext();
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Client model, string returnUrl)
        {
            string d = GetMd5Hash(MD5.Create(), model.Password);
            IQueryable<Client> user = db.Client.Where(s => s.Login == model.Login && s.Password == d);
            if (user.Count() != 0)
            {
                FormsAuthentication.SetAuthCookie(model.Login, model.RememberMe);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                ModelState.AddModelError("", "Неверный логин или пароль");
            }
            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        private string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));
            return sBuilder.ToString();
        }
        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "ClientID,Login,Password,PasswordHelp,Family,Name,Second,RoleID,Phone,Mail,RememberMe,Repeat")] Client client)
        {
            if (ModelState.IsValid)
            {
                client.Repeat = client.Password = GetMd5Hash(MD5.Create(), client.Password);
                client.RoleID = 2;
                db.Client.Add(client);
                db.SaveChanges();
                FormsAuthentication.SetAuthCookie(client.Login, true);
                return RedirectToAction("index", "items");
            }
            return View();
        }

        //
        // POST: /Account/LogOff
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "items");
        }
        [AllowAnonymous]
        public JsonResult CheckLogin(string login)
        {
            if (db.Client.Where(s => s.Login == login).Count() == 0)
                return Json(true, JsonRequestBehavior.AllowGet);
            else return Json("Указанный логин уже существует", JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("index", "items");
            }
        }
        [Authorize]
        public ActionResult UserProfile()
        {
            return View(db.Client.First(s => s.Login == User.Identity.Name));
        }
        public ActionResult UserProfileSave([Bind(Include = "ClientID,Login,Password,PasswordHelp,Family,Name,Second,RoleID,Phone,Mail,Repeat")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/Account/UserProfile/");
            }
            else
            {
                return HttpNotFound();
            }
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        public JsonResult ChangePassw(string oldpass, string newpass, string helppass)
        {
            Client c = db.Client.First(s => s.Login == User.Identity.Name);
            if (GetMd5Hash(MD5.Create(), oldpass) == c.Password)
            {
                c.Password = c.Repeat = GetMd5Hash(MD5.Create(), newpass);
                c.PasswordHelp = helppass;
                db.Entry(c).State = EntityState.Modified;
                db.SaveChanges();
                return Json(true);
            }
            else return Json(false);
        }
        public ActionResult HistoryOfLoad()
        {
            int clientid = db.Client.First(s => s.Login == User.Identity.Name).ClientID;
            var load = db.Load.Where(s => s.ClientID == clientid);
            var loadlist = db.LoadList.Where(s => load.Any(k => k.LoadID == s.LoadID)).Include(i => i.Items).Include(i => i.Items.RateT).Include(i => i.Load);
            ViewBag.loadlist = loadlist.ToList();
            return View();
        }
        public ActionResult UserList()
        {
            ViewBag.client = db.Client.Include(i => i.Role).ToList();
            return View();
        }
        public ActionResult ClientDetail(int? id)
        {
            ViewBag.role = db.Roles.ToList();
            if (id == null)
                return View(new Client());
            var client = db.Client.Find((int)id);
            if (client == null)
                return HttpNotFound();

            return View(client);
        }
        public ActionResult SaveClient([Bind(Include = "ClientID,Login,Password,PasswordHelp,Family,Name,Second,RoleID,Phone,Mail,Repeat")] Client client)
        {
            if (client == null)
                return HttpNotFound();
            if (db.Client.Find(client.ClientID) == null)
            {
                client.Password = client.Repeat = GetMd5Hash(MD5.Create(), client.Password);
                db.Client.Add(client);
            }
            else
            {
                if (client.Password == null)
                {
                    var password = db.Client.Find(client.ClientID);
                    client.Password = client.Repeat = password.Password;
                    client.PasswordHelp = password.PasswordHelp;
                }
                var c = db.Client.Find(client.ClientID);
                c.Login = client.Login;
                c.Family = client.Family;
                c.Mail = client.Mail;
                c.Name = client.Name;
                c.Password = GetMd5Hash(MD5.Create(), client.Password);
                c.Repeat = GetMd5Hash(MD5.Create(), client.Password);
                c.PasswordHelp = client.PasswordHelp;
                c.Phone = client.Phone;
                c.RoleID = client.RoleID;
                c.Second = client.Second;
            }
            db.SaveChanges();
            return Redirect("/Account/UserList/");
        }
        public ActionResult DeleteClient(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var a = db.Client.Find(id);
            if (a == null)
                return HttpNotFound();
            db.Client.Remove(a);
            db.SaveChanges();
            return Redirect("/Account/UserList/");
        }
    }
}