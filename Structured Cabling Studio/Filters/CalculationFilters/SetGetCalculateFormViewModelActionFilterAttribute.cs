using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StructuredCablingStudio.API.ViewModels.CalculationViewModels;
using StructuredCablingStudio.DTOs.CalculationDTOs;
using StructuredCablingStudio.Models.CalculationModels;

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
