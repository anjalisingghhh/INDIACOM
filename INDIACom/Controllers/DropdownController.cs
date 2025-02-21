using INDIACom.App_Cude;
using System.Collections.Generic;
using System.Web.Mvc;

namespace INDIACom.Controllers
{
    public class DropdownController : Controller
    {
        [HttpPost]
        public JsonResult getDepartment(string type = "")
        {
            List<SelectListItem> list = new List<SelectListItem>();
            CommonMethod.bindDropDownHnGrid("Proc_Common", list, "DEPT", "", "", "", "", type);
            return Json(list, 0);

        }
    }
}