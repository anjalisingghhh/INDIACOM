using INDIACom.App_Cude;
using INDIACom.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System;
using System.Linq;

// Ensure DAL is inside this namespace

namespace INDIACom.Controllers
{
    public class NewsController : Controller
    {
        private DAL dal = new DAL();  // Now, DAL is recognized

        [HttpGet]
        public ActionResult NewsCreation()
        {
            return View();
        }

        [HttpPost]
        public JsonResult InsertNews(NewsModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                                           .SelectMany(v => v.Errors)
                                           .Select(e => e.ErrorMessage)
                                           .ToList();
                    return Json(new { success = false, message = "Validation failed.", errors }, JsonRequestBehavior.AllowGet);
                }

                string result = dal.InsertNews(model);

                if (result == "Success")
                {
                    return Json(new { success = true, message = "News submitted successfully!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = result }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public JsonResult GetNews()
        {
            try
            {
                List<NewsModel> newsList = dal.GetNews();
                ViewBag.News = newsList;
                return Json(new { success = true, data = newsList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}