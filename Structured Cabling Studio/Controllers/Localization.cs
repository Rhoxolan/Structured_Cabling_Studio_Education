using Microsoft.AspNetCore.Mvc;
using StructuredCablingStudio.Filters.LocalizationFilters;

namespace StructuredCablingStudio.Controllers
{
	public class Localization : Controller
	{
		[SetLocalizationCookiesFilter]
		public async Task<IActionResult> SetLanguage(string culture, string returnUrl)
		{
			return LocalRedirect(returnUrl);
		}
	}
}
