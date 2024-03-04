using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OBS_App.Models;
using OBS_App.ViewsModel;

namespace OBS_App.Controllers.Yönetim
{
	public class AccountController : Controller
	{

		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<AppRole> _roleManager;

		private SignInManager<AppUser> _signInManager;

		public AccountController(UserManager<AppUser> usermanager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager)
		{
			_userManager = usermanager;
			_roleManager = roleManager;
			_signInManager = signInManager;

		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewsModel model)
		{
			if (ModelState.IsValid)
			{
				//kullanıcının emaili alınır
				var user = await _userManager.FindByEmailAsync(model.Email);

				if (user != null)
				{
					//aktif kullanıcı varsa ilk çıkış yapılır.
					await _signInManager.SignOutAsync();

					//Pasword kontrol edilir, eğer ki beni hatırla seçeneği işaretlenmişse true olarak kullanıcı oturumu kaydedilir.
					var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);

					if (result.Succeeded)
					{
						if (User.IsInRole("Admin"))
						{
							return RedirectToAction("Index", "Admin");
						}
						if (User.IsInRole("Ogretmen"))
						{
							return RedirectToAction("Ogrt_Index", "Ogretmen");
						}
						if (User.IsInRole("Ogrenci"))
						{
							return RedirectToAction("Ogr_Index", "Ogrenci");
						}

						ModelState.AddModelError("", "*Lütfen Yöneticiniz İle Görüşünüz");

					}
					else
					{
						ModelState.AddModelError("", "*Parola Yanlış");
					}

				}
				else
				{
					ModelState.AddModelError("", "*Bu Email Adresi İle Bir Hesap Bulunumadı!");
				}

			}
			return View(model);
		}

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
