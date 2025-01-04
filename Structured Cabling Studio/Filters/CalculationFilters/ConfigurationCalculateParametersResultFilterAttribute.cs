using Microsoft.AspNetCore.Mvc.Filters;
using StructuredCablingStudio.Extensions.ISessionExtension;
using StructuredCablingStudio.Models.CalculationModels;

namespace StructuredCablingStudio.Filters.CalculationFilters
{
	public class ConfigurationCalculateParametersResultFilterAttribute : ActionFilterAttribute
	{
		private static readonly string _configurationCalculateParametersKey = "configurationCalculateParameters";

		public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
		{
			var configurationCalculateParameters = (ConfigurationCalculateParameters)context.HttpContext.Items[_configurationCalculateParametersKey]!;
			context.HttpContext.Session.SetConfigurationCalculateParameters(configurationCalculateParameters);

			await next();
		}
	}
}
