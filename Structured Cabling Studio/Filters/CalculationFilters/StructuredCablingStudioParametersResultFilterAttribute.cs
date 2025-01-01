using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StructuredCablingStudio.Extensions.ISessionExtension;
using StructuredCablingStudio.Extensions.StructuredCablingStudioParametersExtensions;
using StructuredCablingStudio.Services.CalculationServices.CalculationService;
using StructuredCablingStudio.ViewModels.CalculationViewModels;

namespace StructuredCablingStudio.Filters.CalculationFilters
{
	public class StructuredCablingStudioParametersResultFilterAttribute(ICalculationService calculationService) : ActionFilterAttribute
	{
		private static readonly string _viewDataDiapasonsKey = "Diapasons";

		public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
		{
			var controller = (Controller)context.Controller;
			var model = (CalculateViewModel?)controller.ViewData.Model;
			if (model != null)
			{
				var structuredCablingStudioParameters = model.ToStructuredCablingStudioParameters();

				structuredCablingStudioParameters.Diapasons
					= await calculationService.SetStructuredCablingStudioDiapasonsAsync(structuredCablingStudioParameters);

				controller.ViewData[_viewDataDiapasonsKey] = structuredCablingStudioParameters.Diapasons;

				context.HttpContext.Session.SetStructuredCablingStudioParameters(structuredCablingStudioParameters);
			}

			await next();
		}
	}
}
