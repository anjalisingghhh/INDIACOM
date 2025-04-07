using INDIACom.App_Cude;
using System.Web.Mvc;
using System;
using System.Linq;
using INDIACom.Models;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace INDIACom.Controllers
{
    public class AccountController : Controller
    {
        private DAL dal = new DAL();

        static string Email = "";

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Check_Details(LoginUserModel user)
        {
            string str = "";
            try
            {
                DataTable dt = new DAL().checkCredentials(user);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        user.LoggedInUser = dt.AsEnumerable().Select(m => new MemberModel
                        {
                            MemberID = long.Parse(m["member_id"].ToString()),
                            Name = m["Name"].ToString(),
                            Email = m["Email"].ToString(),
                            Category = m["Category"].ToString(),
                            IEEE_No = m["IEEE_No"].ToString(),
                            CSI_No = m["CSI_No"].ToString()
                        }).FirstOrDefault();
                        str = "success";
                    }
                    else
                    {
                        str = dt.Rows[0]["Msg"].ToString().Trim();
                    }
                    return Json(new{success = true}, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                str = "Error ! Something went Wrong";
            }
            return Json(str, JsonRequestBehavior.AllowGet);
        }

    }
}