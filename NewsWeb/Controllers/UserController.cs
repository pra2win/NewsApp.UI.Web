using NewsWeb.Comman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsWeb.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [HttpPost]
        public ActionResult GetUsers()
        {
            var Users = ServerBAL.GetUsers();
            return PartialView("_UserList",Users);
        }
    }
}