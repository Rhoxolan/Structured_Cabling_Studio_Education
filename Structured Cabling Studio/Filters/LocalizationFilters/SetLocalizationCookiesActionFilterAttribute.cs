using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StructuredCablingStudio.Filters.LocalizationFilters
{
	public class SetLocalizationCookiesActionFilterAttribute : ActionFilterAttribute
	{
		private static readonly string _cultureActionArgumentsKey = "culture";

		public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var culture = (string)context.ActionArguments[_cultureActionArgumentsKey]!;

			context.HttpContext.Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
				CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
				new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
			
			await next();
		}
	}
}
