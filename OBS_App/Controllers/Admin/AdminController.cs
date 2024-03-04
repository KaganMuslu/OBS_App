using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OBS_App.Controllers.Admin
{
	public class AdminController : Controller
	{
        [Authorize(Roles = "Admin")]
		public IActionResult Index()
		{
			return View();
		}
	}
}
