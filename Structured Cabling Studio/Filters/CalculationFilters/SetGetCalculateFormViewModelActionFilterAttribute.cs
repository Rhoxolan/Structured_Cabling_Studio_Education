using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StructuredCablingStudio.API.ViewModels.CalculationViewModels;
using StructuredCablingStudio.DTOs.CalculationDTOs;
using StructuredCablingStudio.Models.CalculationModels;

namespace StructuredCablingStudio.Filters.CalculationFilters
{
	public class SetGetCalculateFormViewModelActionFilterAttribute : ActionFilterAttribute
	{
		public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			await next();

			var cablingParameters = (StructuredCablingStudioParameters)context.ActionArguments["cablingParameters"]!;
			var calculateParameters = (ConfigurationCalculateParameters)context.ActionArguments["calculateParameters"]!;
			var calculateDTO = (CalculateDTO)context.ActionArguments["calculateDTO"]!;

			var viewModel = new CalculateViewModel
			{
				IsCableHankMeterageAvailability = calculateParameters.IsCableHankMeterageAvailability.GetValueOrDefault()
			};

			var controller = (Controller)context.Controller;
			controller.ViewData.Model = viewModel;
		}
	}
}
