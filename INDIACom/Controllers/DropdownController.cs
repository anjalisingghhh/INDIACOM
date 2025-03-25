using INDIACom.App_Cude;
using System.Collections.Generic;
using System.Web.Mvc;

namespace INDIACom.Controllers
{
    public class DropdownController : Controller
    {
        [HttpPost]
        public JsonResult getEvent(string type = "")
        {
            List<SelectListItem> list = new List<SelectListItem>();
            CommonMethod.bindDropDownHnGrid("Proc_Common", list, "EVENT", "", "", "", "", type);
            return Json(list, 0);

        }

        public JsonResult getCountries(string type = "")
        {
            List<SelectListItem> list = new List<SelectListItem>();
            CommonMethod.bindDropDownHnGrid("Proc_Common", list, "COUNTRY", "", "", "", "", type);
            return Json(list, 0);

        }

        public JsonResult getStates(string countryId,string type = "")
        {
            List<SelectListItem> list = new List<SelectListItem>();
            CommonMethod.bindDropDownHnGrid("Proc_Common", list, "STATE", countryId, "", "", "", type);
            return Json(list, 0);

        }

        public JsonResult getCities(string countryId,string stateId, string type = "")
        {
            List<SelectListItem> list = new List<SelectListItem>();
            CommonMethod.bindDropDownHnGrid("Proc_Common", list, "CITY", countryId, stateId, "", "", type);
            return Json(list, 0);

        }
    }
}