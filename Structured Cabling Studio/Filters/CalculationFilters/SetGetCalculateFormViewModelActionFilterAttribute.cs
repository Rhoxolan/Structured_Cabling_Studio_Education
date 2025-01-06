using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StructuredCablingStudio.ViewModels.CalculationViewModels;
using StructuredCablingStudio.DTOs.CalculationDTOs;
using StructuredCablingStudio.Models.CalculationModels;
using StructuredCablingStudio.Extensions.StructuredCablingStudioParametersExtensions;

namespace StructuredCablingStudio.Filters.CalculationFilters
{
	public class SetGetCalculateFormViewModelActionFilterAttribute : ActionFilterAttribute
	{
		private static readonly string _calculateParametersActionArgumentsKey = "calculateParameters";
		private static readonly string _cablingParametersActionArgumentsKey = "cablingParameters";
		private static readonly string _calculateDTOActionArgumentsKey = "calculateDTO";

		public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			await next();

			var cablingParameters = (StructuredCablingStudioParameters)context.ActionArguments[_cablingParametersActionArgumentsKey]!;
			var calculateParameters = (ConfigurationCalculateParameters)context.ActionArguments[_calculateParametersActionArgumentsKey]!;
			var calculateDTO = (CalculateDTO)context.ActionArguments[_calculateDTOActionArgumentsKey]!;

			var viewModel = new CalculateViewModel().FromCablingConfigurationParameters(cablingParameters, calculateParameters, calculateDTO);

			var controller = (Controller)context.Controller;
			controller.ViewData.Model = viewModel;
		}
	}
}
