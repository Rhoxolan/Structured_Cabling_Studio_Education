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
				IsCableHankMeterageAvailability = calculateParameters.IsCableHankMeterageAvailability.GetValueOrDefault(),
				CableHankMeterage = calculateParameters.CableHankMeterage,
				MinPermanentLink = calculateDTO.MinPermanentLink,
				MaxPermanentLink = calculateDTO.MaxPermanentLink,
				NumberOfPorts = calculateDTO.NumberOfPorts,
				NumberOfWorkplaces = calculateDTO.NumberOfWorkplaces,
				IsCableRouteRunOutdoors = cablingParameters.RecommendationsArguments.IsolationType == IsolationType.Outdoor,
				IsConsiderFireSafetyRequirements = cablingParameters.RecommendationsArguments.IsolationMaterial == IsolationMaterial.LSZH,
				IsCableShieldingNecessity = cablingParameters.RecommendationsArguments.ShieldedType == ShieldedType.FTP,
				HasTenBase_T = cablingParameters.RecommendationsArguments.ConnectionInterfaces.Contains(ConnectionInterfaceStandard.TenBASE_T),
				HasFastEthernet = cablingParameters.RecommendationsArguments.ConnectionInterfaces.Contains(ConnectionInterfaceStandard.FastEthernet),
				HasGigabitBASE_T = cablingParameters.RecommendationsArguments.ConnectionInterfaces.Contains(ConnectionInterfaceStandard.GigabitBASE_T),
				HasGigabitBASE_TX = cablingParameters.RecommendationsArguments.ConnectionInterfaces.Contains(ConnectionInterfaceStandard.GigabitBASE_TX),
				HasTwoPointFiveGBASE_T = cablingParameters.RecommendationsArguments.ConnectionInterfaces.Contains(ConnectionInterfaceStandard.TwoPointFiveGBASE_T),
				HasFiveGBASE_T = cablingParameters.RecommendationsArguments.ConnectionInterfaces.Contains(ConnectionInterfaceStandard.FiveGBASE_T),
				HasTenGE = cablingParameters.RecommendationsArguments.ConnectionInterfaces.Contains(ConnectionInterfaceStandard.TenGE)
			};

			var controller = (Controller)context.Controller;
			controller.ViewData.Model = viewModel;
		}
	}
}
