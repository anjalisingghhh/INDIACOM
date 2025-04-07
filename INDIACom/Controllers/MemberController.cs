using INDIACom.App_Cude;
using System.Web.Mvc;
using System;
using System.Linq;
using INDIACom.Models;
using System.Data;
using System.IO;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

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
        public JsonResult SubmitRegister(MembersModel model, HttpPostedFileBase file)
        {
            if(model == null)
            {
                return Json(new { success = false, message = "Enter Details" });
            }

            if (model.Password != model.ConfirmPassword)
            {
                return Json(new { success = false, message = "Password and Confirm Password do not match!" });
            }


            try
            {
               string Email = model.Email;
               string Name = model.Name;
                long Mobile = model.Mobile;
                 
            if(model.OrganisationName == null)
            {
                string org = AddOrganisation(model);

                if (org == "Success") model.OrganisationName = model.OrgName;
                else model.OrganisationName = null;
            }
                // Insert register user_details into DB
                string result = dal.InsertUserDetails(model);

                if (result == "Success")
                {
                    long id = dal.GetMemberID(Email,Mobile);
                    if (id > 0)
                    {
                       return UploadFile(file, id, Name);
                    }
                    else
                    {
                        return Json(new { success = false, message = "Member ID retrieval failed!" });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Registration failed!" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred: " + ex.Message });
            }
        }

        public JsonResult UploadFile(HttpPostedFileBase file, long userId, string name)
        {
            if (file == null || file.ContentLength <= 0)
            {
                return Json(new { success = false, message = "Please select a file." });
            }

            try
            {
                string documentType = "Resume"; // Hardcoded for now
                string extension = Path.GetExtension(file.FileName);

                if (extension.ToLower() != ".pdf")
                {
                    return Json(new { success = false, message = "Only PDF files are allowed." });
                }

                // Creating the filename
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                string newFileName = $"{userId}_{name}_{documentType}{extension}";
                string uploadPath = Server.MapPath("~/UploadedFiles/");

                // Ensure the directory exists
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // Full file path
                string filePath = Path.Combine(uploadPath, newFileName);
                file.SaveAs(filePath);

                // Save file path to DB
                DAL dal = new DAL();
                MemberDocumentModel doc = new MemberDocumentModel
                {
                    UserID = userId,
                    Name = name,
                    DocumentType = documentType,
                    FilePath = filePath
                };
                string saveResult = dal.SaveFilePath(doc);

                if (saveResult == "Success")
                {
                    return Json(new { success = true, message = "File uploaded successfully!", filePath = "/UploadedFiles/" + newFileName });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to save file path in database." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }


        public string AddOrganisation(MembersModel org)
        {
            if (org == null)
            {
                return "failed";
            }
            
            string result =  dal.AddOrganisation(org);

            if (result == "Success")
            {
                return result;
            }
            else return null;
        }

    }
}