using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StructuredCablingStudio.Extensions.ISessionExtension;
using StructuredCablingStudio.Models.CalculationModels;

namespace StructuredCablingStudio.Filters.CalculationFilters
{
	public class StructuredCablingStudioParametersResultFilterAttribute : ActionFilterAttribute
	{
		private static readonly string _structuredCablingStudioParametersKey = "structuredCablingStudioParametersKey";
		private static readonly string _viewDataDiapasonsKey = "Diapasons";

		public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
		{
			var controller = (Controller)context.Controller;
			var structuredCablingStudioParameters = (StructuredCablingStudioParameters)context.HttpContext.Items[_structuredCablingStudioParametersKey]!;

			controller.ViewData[_viewDataDiapasonsKey] = structuredCablingStudioParameters.Diapasons;
			context.HttpContext.Session.SetStructuredCablingStudioParameters(structuredCablingStudioParameters);

			await next();
		}
	}
}
