using INDIACom.App_Cude;
using INDIACom.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace INDIACom.Controllers
{
    public class SpecialSessionController : Controller
    {
        private DAL dal = new DAL();
        [HttpGet]
        public ActionResult SpecialSession()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SpecialSession(SpecialSessionModel ss)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                                            .SelectMany(v => v.Errors)
                                            .Select(e => e.ErrorMessage)
                                            .ToList();
                    return Json(new { success = false, message = "Validation failed.", errors });

                }

                string result = dal.InsertSession(ss);
                if (result == "Success")
                {
                    return Json(new { success = true, message = "Session Inserted Successfully!!" });
                }
                else
                {
                    return Json(new { success = false, message = "Data Insertion failed!!" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred:" + ex.Message });
            }
        }
    }
}