using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StructuredCablingStudio.Controllers
{
	public class Account : Controller
	{
		[AllowAnonymous]
		public IActionResult SignIn(string returnUrl)
		{
			throw new NotImplementedException();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[AllowAnonymous]
		public IActionResult SignInWithGoogle(string returnUrl)
		{
			throw new NotImplementedException();
		}

		[AllowAnonymous]
		public async Task<IActionResult> GoogleLoginCallback(string returnUrl)
		{
			throw new NotImplementedException();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout(string returnUrl)
		{
			throw new NotImplementedException();
		}
	}
}
