using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OBS_App.Controllers
{
	public class OgrenciController : Controller
	{
		// Admindeki controller ile çakıştığı için ogr diye isimlendirdik
        [Authorize(Roles = "Ogrenci")]
		public IActionResult Ogr_Index()
		{
			return View();
		}
	}
}
