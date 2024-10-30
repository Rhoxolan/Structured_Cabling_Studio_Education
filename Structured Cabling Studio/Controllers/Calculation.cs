using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StructuredCablingStudio.Controllers
{
	public class Calculation : Controller
	{
		public IActionResult Calculate()
		{
			return View();
		}

		[Authorize]
		public IActionResult History()
		{
			return View();
		}

		public IActionResult Information()
		{
			return View();
		}
	}
}
