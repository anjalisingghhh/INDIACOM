using INDIACom.App_Cude;
using INDIACom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

         [HttpGet]
           public JsonResult GetUserDetails(int memberId)
           {
               UserModel user;
               string result = new DAL().GetUserDetails(memberId, out user); // Call DAL method

               if (result == "Success" && user != null)
               {
                   return Json(new { MemberID = user.MemberID, BioDataPath = user.BioDataPath }, JsonRequestBehavior.AllowGet);
               }
               else
               {
                   return Json(new { error = result }, JsonRequestBehavior.AllowGet);
               }
           }

           

     /*   [HttpGet]
        public JsonResult GetMemberIds()
        {
            DAL dal = new DAL();
            List<UserModel> users;
            string result = dal.GetAllUsers(out users);

            if (result == "Success")
            {
                var response = users.Select(u => new
                {
                    u.MemberID,
                    u.BioDataPath
                }).ToList();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Message = result }, JsonRequestBehavior.AllowGet);
            }
        }
     */






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