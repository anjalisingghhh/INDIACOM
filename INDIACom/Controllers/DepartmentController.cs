using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using INDIACom.App_Cude;
using INDIACom.Models;

namespace INDIACom.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        public ActionResult DepartmentADD1()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddDepartment(Department dept)
        {
            string str = "";
            try
            {

                if (string.IsNullOrEmpty(dept.DeptName))
                {
                    return Json("Please enterdepartment name", JsonRequestBehavior.AllowGet);
                }


                str = new DAL().AddDepartment(dept);
            }
            catch (Exception e)
            {
                str = "Something Went Wrong";
            }
            return Json(str, JsonRequestBehavior.AllowGet);
        }

    }
}