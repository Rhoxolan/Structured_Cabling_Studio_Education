using Microsoft.AspNetCore.Mvc.Filters;
using StructuredCablingStudio.Extensions.ISessionExtension;
using StructuredCablingStudio.Services.CalculationServices.CalculationService;

namespace StructuredCablingStudio.Filters.CalculationFilters
{
	public class SetConfigurationCalculateParametersActionFilterAttribute(ICalculationService calculationService) : ActionFilterAttribute
	{
		private static readonly string _actionArgumentsKey = "calculateParameters";

		public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			if (context.ActionArguments[_actionArgumentsKey] == null)
			{
				var parameters = calculationService.GetConfigurationCalculateParametersDefault();
				context.HttpContext.Session?.SetConfigurationCalculateParameters(parameters);
				context.ActionArguments[_actionArgumentsKey] = parameters;
			}

			await next();
		}
	}
}
