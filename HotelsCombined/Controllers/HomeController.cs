
using Microsoft.AspNetCore.Mvc;

namespace HotelsCombined
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Redirect("/default.htm");
        }
    }

}