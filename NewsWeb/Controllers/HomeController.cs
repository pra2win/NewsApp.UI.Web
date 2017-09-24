﻿using NewsWeb.Comman;
using NewsWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NewsWeb.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        [CustomAuthorizer]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        [CustomAuthorizer]
        public NewsDetail AddNews()
        {
            string base64 = string.Empty;
            string CategoryId = Request.Form.GetValues("CategoryId")[0];//"R-P-T";
            string NewsTitle = Request.Form.GetValues("NewsTitle")[0];
            string NewsDescription = Request.Form.GetValues("NewsDescription")[0];
            string FileUrl = Request.Form.GetValues("FileUrl")[0];
            bool IsNotify = Convert.ToBoolean(Request.Form.GetValues("IsNotify")[0]);


            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                string filename = Path.GetFileName(file.FileName);
                if (file.ContentLength > 0)
                {
                    byte[] binaryWriteArray = new byte[file.InputStream.Length];
                    file.InputStream.Read(binaryWriteArray, 0,
                    (int)file.InputStream.Length);
                    base64 = Convert.ToBase64String(binaryWriteArray);
                }
            }
            else
            {
                using (WebClient client = new WebClient())
                {
                    byte[] bytes = client.DownloadData(FileUrl);
                    MemoryStream ms = new MemoryStream(bytes);
                    System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                    bytes = ms.ToArray();
                    base64 = Convert.ToBase64String(bytes);
                }

            }
            AddNewsRequestModel request = new AddNewsRequestModel();
            request.CategoryId = Convert.ToInt32(CategoryId);
            request.NewsDescription = NewsDescription;
            request.NewsPhoto = base64;
            request.NotifyToAll = IsNotify;
            request.NewsTitle = NewsTitle;
            //request.NewsPhoto = request.NewsPhoto.Replace("data:image/jpeg;base64,", string.Empty);
            request.CreatedTs = DateTime.Now;
            request.NewsById = new Guid(Session["UserId"].ToString());
            var result = ServerBAL.AddNews(request);

            return result;
        }

        [Authorize]
        [CustomAuthorizer]
        public ActionResult NewsList()
        {
            var result = ServerBAL.NewsList();
            return PartialView("_NewsList", result);
        }

        [Authorize]
        [CustomAuthorizer]
        public bool ApproveNews(string newsId, bool IsNotify)
        {
            Guid _newsId = new Guid(newsId);
            var result = ServerBAL.ApproveNews(_newsId, IsNotify);
            return result;
        }

        [Authorize]
        [CustomAuthorizer]
        public ActionResult CreateUser()
        {
            return View("_CreateUser");
        }


        [Authorize]
        [CustomAuthorizer]
        [HttpPost]
        public bool CreateNewUser(string UserId, string Password, string FirstName, string LastName, string Email, string Mobile, string Gender, int UserType)
        {
            bool success = false;
            CreateUser req = new Models.CreateUser();
            req.EmailId = Email;
            req.FirstName = FirstName;
            req.LastName = LastName;
            req.UserId = UserId;
            req.Password = Password;
            req.MobileNo = Mobile;
            req.userType = UserType;
            req.Gender = Gender;
            var result = ServerBAL.CreateNewUser(req);
            if (result != null)
            {
                return result.UserId != null;
            }
            return success;
        }

        [Authorize]
        [CustomAuthorizer]
        [HttpPost]
        public ActionResult EditNews(string newsId)
        {
            Guid _newsId = Guid.Parse(newsId);
            generalNewsListReponse newsData = ServerBAL.EditNewsDetails(_newsId);
            return PartialView("_EditNews", newsData);
        }

        [Authorize]
        [CustomAuthorizer]
        [HttpPost]
        public generalNewsListReponse UpdateNews(string newsId, string newsTitle, string newsDesc, int newsCat)
        {
            var res = ServerBAL.UpdateNews(newsId, newsTitle, newsDesc, newsCat);

            return res;

        }
    }
}
