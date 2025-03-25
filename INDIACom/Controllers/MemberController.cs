using INDIACom.App_Cude;
using System.Web.Mvc;
using System;
using System.Linq;
using INDIACom.Models;

namespace INDIACom.Controllers
{
    public class MemberController : Controller
    {
        private DAL dal = new DAL();

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

     [HttpPost]
        public JsonResult SubmitRegister(MemberModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                return Json(new { success = false, message = "Password and Confirm Password do not match!" });
            }


            try
            {
                // Insert register user_details into DB
                string result = dal.InsertUserDetails(model);

                if (result == "Success")
                {
                    return Json(new { success = true, message = "Feedback submitted successfully!" });
                }
                else
                {
                    return Json(new { success = false, message = "Data insertion failed!" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred: " + ex.Message });
            }
        }


    }
}