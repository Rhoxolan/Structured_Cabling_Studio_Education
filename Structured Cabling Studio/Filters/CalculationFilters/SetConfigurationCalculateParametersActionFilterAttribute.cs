using Microsoft.AspNetCore.Mvc.Filters;
using StructuredCablingStudio.Extensions.ISessionExtension;
using StructuredCablingStudio.Models.CalculationModels;

namespace StructuredCablingStudio.Filters.CalculationFilters
{
	public class SetConfigurationCalculateParametersActionFilterAttribute : ActionFilterAttribute
	{
		private static readonly string _actionArgumentsKey = "calculateParameters";

		public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			if (context.ActionArguments[_actionArgumentsKey] == null)
			{
				var parameters = new ConfigurationCalculateParameters
				{
					IsCableHankMeterageAvailability = true
				};
				context.HttpContext.Session?.SetConfigurationCalculateParameters(parameters);
				context.ActionArguments[_actionArgumentsKey] = parameters;
			}

			await next();
		}
	}
}
