using Microsoft.AspNetCore.Mvc.Filters;
using StructuredCablingStudio.DTOs.CalculationDTOs;
using StructuredCablingStudio.Models.CalculationModels;
using StructuredCablingStudio.Services.CalculationServices.CalculationService;
using StructuredCablingStudio.ViewModels.CalculationViewModels;

namespace StructuredCablingStudio.Filters.CalculationFilters
{
	public class RestoreDefaultsCalculateFormActionFilterAttribute(ICalculationService calculationService) : ActionFilterAttribute
	{
		private static readonly string _modelKey = "calculateVM";

		public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			StructuredCablingStudioParameters structuredCablingStudioParameters
				= await calculationService.GetStructuredCablingStudioParametersDefaultAsync();
			ConfigurationCalculateParameters configurationCalculateParameters = calculationService.GetConfigurationCalculateParametersDefault();
			CalculateDTO calculateDTO = calculationService.GetCalculateDTODefault();
			
			var model = (CalculateViewModel)context.ActionArguments[_modelKey]!;

			await next();
		}
	}
}
