using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StructuredCablingStudio.Extensions.ISessionExtension;
using StructuredCablingStudio.Extensions.StructuredCablingStudioParametersExtensions;
using StructuredCablingStudio.ViewModels.CalculationViewModels;

namespace StructuredCablingStudio.Filters.CalculationFilters
{
	public class ConfigurationCalculateParametersResultFilterAttribute : ActionFilterAttribute
	{
		public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
		{
			var controller = (Controller)context.Controller;
			var model = (CalculateViewModel?)controller.ViewData.Model;
			if (model != null)
			{
				var configurationCalculateParameters = model.ToConfigurationCalculateParameters();

				context.HttpContext.Session.SetConfigurationCalculateParameters(configurationCalculateParameters);
			}

			await next();
		}
	}
}
