using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Models;
using RentACar.ViewModel;
using System.Security.Claims;

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
            UserViewModel userViewModel = new UserViewModel();
            return View(userViewModel);
        }

        [HttpPost]

        public async Task<IActionResult> Giris([Bind("Email", "Password")] UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                ClaimsIdentity identity = null;
                bool isAuthenticated = false;
                User userr = await _rentACarDBContext.Users.Include(k => k.Role).FirstOrDefaultAsync(m => m.Email == userViewModel.Email && m.Password == userViewModel.Password);

                if (userr == null)
                {
                    ModelState.AddModelError("Hata2", "Kullanıcı Bulunamadı.");
                    return View();
                }

                identity = new ClaimsIdentity //çerez oluşturma - şu an böyle bir user var .
                (new[]
                        {
                            new Claim(ClaimTypes.Sid,userr.UserID.ToString()), //git istenen i userId ye at....//cache gibi bir yöntem// jwt gibi güvenli bir sistem uygulama.
                            new Claim(ClaimTypes.Email,userr.Email),
                            new Claim(ClaimTypes.Role,userr.Role.RoleName),
                        },
                        CookieAuthenticationDefaults.AuthenticationScheme
                );
                isAuthenticated = true;
                if (isAuthenticated)
                {
                    var claims = new ClaimsPrincipal(identity);
                    var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claims,

                        new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTime.Now.AddMinutes(60) //60 dk boyunca çerezde dursun, 60 dk sonra oturumdan atsın.
                        }

                        );
                    if (userr.Role.RoleName == "Pasif Kullanıcı")
                    {
                        return Redirect("~/Hesap/AktivasyonBilgilendirmesi");
                    }
                    else if (userr.Role.RoleName == "Aktif Kullanıcı")
                    {
                        return Redirect("~/Home/Index");
                    }

                    else if (userr.Role.RoleName == "Admin")
                    {
                        return Redirect("~/AdminAnasayfa/Index");
                    }
                    else
                    {
                        return Redirect("~/Home/ErrorPage");
                    }
                }
            }
            return View(userViewModel);
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
            User x = await _rentACarDBContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);  //x girilen e postaya ait kullanıcı
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
            if (user != null)
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
