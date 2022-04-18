using Microsoft.AspNetCore.Mvc;

namespace RentACar.Controllers
{
    public class HesapController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Giris()
        {
            return View();
        }
        public IActionResult Kayit()
        {
            return View();
        }
        public IActionResult SifreSifirla()
        {
            return View();
        }
        public IActionResult HukumveKosullar()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Kayit(string UserNamee)
        {
            string a = "Ali" + UserNamee;
            return View(a);
        }
    }
}
