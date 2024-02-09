using Microsoft.AspNetCore.Mvc;

namespace SuccessiveCart.Controllers
{
    public class ForgotPasswordController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
