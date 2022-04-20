using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            //çok kayıt varsa 1.sini al, hiç kayıt yoksa default al yani " " boştur, null değil. Kesinlikle bir kayıt getirir.
            User x= await _rentACarDBContext.Users.FirstOrDefaultAsync(u=>u.Email == user.Email);  //x girilen e postaya ait kullanıcı
            if (x != null)
            {
                ModelState.AddModelError("Hata1", "Bu E-posta Adresine ait bir kullanıcı bulunmaktadır.");
            }
            if (ModelState.IsValid)
            {
                await _rentACarDBContext.AddAsync(user);
                await _rentACarDBContext.SaveChangesAsync();
                Helpers.EPostaIslemleri.AktivasyonMailiGonder(user.Email);
                return RedirectToAction("AktivasyonBilgilendirmesi", "Hesap");
               
            }
            return View(user); //bir girdisi hatalıysa sayfayı tamamen boş döndürmüyor, girdiği bilgilerle döndürüyor.
        }
        public IActionResult Aktivasyon(string kkk)
        {
            string eposta = Helpers.Sifreleme.SifreyiCoz(kkk);
            var user = _rentACarDBContext.Users.FirstOrDefault(m => m.Email == eposta);
            if(user != null)
            {
                user.RoleID = 2;
                _rentACarDBContext.Users.Update(user);
                _rentACarDBContext.SaveChanges();
                return View();
            }
            return View();  
        }
        public IActionResult AktivasyonBilgilendirmesi()
        {
            return View();
        }
    }
}
