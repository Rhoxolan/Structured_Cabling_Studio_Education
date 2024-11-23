using Microsoft.AspNetCore.Mvc;
using StructuredCablingStudio.Filters.LocalizationFilters;

namespace StructuredCablingStudio.Controllers
{
	public class Localization : Controller
	{
		[SetLocalizationCookiesFilter]
		public IActionResult SetLanguage(string culture, string returnUrl)
		{
			return LocalRedirect(returnUrl);
		}
	}
}
