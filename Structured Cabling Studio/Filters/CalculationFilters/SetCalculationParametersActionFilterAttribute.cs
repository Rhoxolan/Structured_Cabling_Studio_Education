using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StructuredCablingStudio.DTOs.CalculationDTOs;
using StructuredCablingStudio.Extensions.StructuredCablingStudioParametersExtensions;
using StructuredCablingStudio.Models.CalculationModels;
using StructuredCablingStudio.Services.CalculationServices.CalculationService;
using StructuredCablingStudio.ViewModels.CalculationViewModels;

namespace StructuredCablingStudio.Filters.CalculationFilters
{
	public class SetCalculationParametersActionFilterAttribute(ICalculationService calculationService) : ActionFilterAttribute
	{
		private static readonly string _structuredCablingStudioParametersKey = "structuredCablingStudioParametersKey";
		private static readonly string _configurationCalculateParametersKey = "configurationCalculateParameters";
		private static readonly string _calculateModelKey = "calculateModel";

		public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			await next();

			var controller = (Controller)context.Controller;
			var model = (CalculateViewModel)controller.ViewData.Model!;

			StructuredCablingStudioParameters structuredCablingStudioParameters = model.ToStructuredCablingStudioParameters();
			structuredCablingStudioParameters.Diapasons
				= await calculationService.SetStructuredCablingStudioDiapasonsAsync(structuredCablingStudioParameters);

			ConfigurationCalculateParameters configurationCalculateParameters = model.ToConfigurationCalculateParameters();

			CalculateDTO calculateModel = model.ToCalculateDTO();

			context.HttpContext.Items[_structuredCablingStudioParametersKey] = structuredCablingStudioParameters;
			context.HttpContext.Items[_configurationCalculateParametersKey] = configurationCalculateParameters;
			context.HttpContext.Items[_calculateModelKey] = calculateModel;
		}
	}
}
