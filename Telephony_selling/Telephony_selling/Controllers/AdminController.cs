using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Telephony_selling.Controllers
{
    public class AdminController : Controller
    {
        Telephony_selling.Models.TelephonyContext db = new Models.TelephonyContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View(db.Roles.Find(db.Client.First(s => s.Login == User.Identity.Name).RoleID));
        }
    }
}