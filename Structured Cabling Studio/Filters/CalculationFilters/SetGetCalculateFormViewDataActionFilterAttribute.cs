using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StructuredCablingStudio.Models.CalculationModels;

namespace StructuredCablingStudio.Filters.CalculationFilters
{
	public class SetGetCalculateFormViewDataActionFilterAttribute : ActionFilterAttribute
	{
		private static readonly string _cablingParametersActionArgumentsKey = "cablingParameters";
		private static readonly string _viewDataDiapasonsKey = "Diapasons";

		public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			await next();

			var cablingParameters = (StructuredCablingStudioParameters)context.ActionArguments[_cablingParametersActionArgumentsKey]!;

			var controller = (Controller)context.Controller;
			controller.ViewData[_viewDataDiapasonsKey] = cablingParameters.Diapasons;
		}
	}
}
