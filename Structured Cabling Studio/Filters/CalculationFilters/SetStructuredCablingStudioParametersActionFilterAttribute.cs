using Microsoft.AspNetCore.Mvc.Filters;
using StructuredCablingStudio.Extensions.ISessionExtension;
using StructuredCablingStudio.Services.CalculationServices.CalculationService;

namespace StructuredCablingStudio.Filters.CalculationFilters
{
	public class SetStructuredCablingStudioParametersActionFilterAttribute(ICalculationService calculationService) : ActionFilterAttribute
	{
		private static readonly string _actionArgumentsKey = "cablingParameters";

		public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			if (context.ActionArguments[_actionArgumentsKey] == null)
			{
				var parameters = calculationService.GetStructuredCablingStudioParametersDefault();
				parameters.Diapasons = await calculationService.SetStructuredCablingStudioDiapasonsAsync(parameters);
				context.HttpContext.Session?.SetStructuredCablingStudioParameters(parameters);
				context.ActionArguments[_actionArgumentsKey] = parameters;
			}

			await next();
		}
	}
}
