using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using INDIACom.App_Cude;
using INDIACom.Models;

namespace INDIACom.Controllers
{
    public class FileUploadController : Controller
    {
        [HttpGet]
        public ActionResult UploadResume()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UploadFile(HttpPostedFileBase file, int userId, string name)
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
    }
}
