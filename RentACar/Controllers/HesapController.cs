using Microsoft.AspNetCore.Mvc;
using RentACar.Data;
using RentACar.Models;

namespace RentACar.Controllers
{
    public class HesapController : Controller
    {
        private readonly RentACarDBContext _rentACarDBContext; // database ile ilgili her şeyi bilen class dbcontext ve database işlemlerini bilen bize ait obje.
        //çok sık kullanılan büyük classlar için yapılır dependency injection, dbcontext gibi classlar. Çok kişi tarafından, çok kez kullanılan işlemlerde, kimse sıra beklemesin diye
        //kullandığımız bir metodolojidir. _rentACarDBContext bu sayfada global açıldı, bu nedenle bu sayfadaki/ controllerdaki tüm actionlarda, her yerde kullanılabilir.

        public HesapController(RentACarDBContext rentACarDBContext) // ramde buna ait bir yer açtık, ortak objeyi bana ait objeye eşitledik.
        {
            _rentACarDBContext = rentACarDBContext;
        }

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
            User user = new User();
            return View(user);
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
        public async Task<IActionResult> Kayit([Bind("UserID", "Email", "Password", "PasswordRepeat", "FullName", "Surname", "MobileNO", "RoleID")] User user)
        {
            user.RoleID = 1;
            if (ModelState.IsValid)
            {
                await _rentACarDBContext.AddAsync(user);
                await _rentACarDBContext.SaveChangesAsync();
                return RedirectToAction("Giris", "Hesap");
            }
            return View(user); //bir girdisi hatalıysa sayfayı tamamen boş döndürmüyor, girdiği bilgilerle döndürüyor.
        }
    }
}
