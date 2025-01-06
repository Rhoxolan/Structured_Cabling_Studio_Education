using Microsoft.AspNetCore.Mvc.Filters;
using StructuredCablingStudio.DTOs.CalculationDTOs;
using StructuredCablingStudio.Extensions.StructuredCablingStudioParametersExtensions;
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
			model = model.FromCablingConfigurationParameters(structuredCablingStudioParameters, configurationCalculateParameters, calculateDTO);

			context.ActionArguments[_modelKey] = model;

			context.ModelState.SetModelValue(nameof(model.IsCableHankMeterageAvailability), model.IsCableHankMeterageAvailability, default);
			context.ModelState.SetModelValue(nameof(model.CableHankMeterage), model.CableHankMeterage, default);
			context.ModelState.SetModelValue(nameof(model.TechnologicalReserve), model.TechnologicalReserve, default);
			context.ModelState.SetModelValue(nameof(model.IsStrictComplianceWithTheStandart), model.IsStrictComplianceWithTheStandart, default);
			context.ModelState.SetModelValue(nameof(model.IsAnArbitraryNumberOfPorts), model.IsAnArbitraryNumberOfPorts, default);
			context.ModelState.SetModelValue(nameof(model.IsTechnologicalReserveAvailability), model.IsTechnologicalReserveAvailability, default);
			context.ModelState.SetModelValue(nameof(model.IsRecommendationsAvailability), model.IsRecommendationsAvailability, default);
			context.ModelState.SetModelValue(nameof(model.IsCableRouteRunOutdoors), model.IsCableRouteRunOutdoors, default);
			context.ModelState.SetModelValue(nameof(model.IsConsiderFireSafetyRequirements), model.IsConsiderFireSafetyRequirements, default);
			context.ModelState.SetModelValue(nameof(model.IsCableShieldingNecessity), model.IsCableShieldingNecessity, default);
			context.ModelState.SetModelValue(nameof(model.HasTenBase_T), model.HasTenBase_T, default);
			context.ModelState.SetModelValue(nameof(model.HasFastEthernet), model.HasFastEthernet, default);
			context.ModelState.SetModelValue(nameof(model.HasGigabitBASE_T), model.HasGigabitBASE_T, default);
			context.ModelState.SetModelValue(nameof(model.HasGigabitBASE_TX), model.HasGigabitBASE_TX, default);
			context.ModelState.SetModelValue(nameof(model.HasTwoPointFiveGBASE_T), model.HasTwoPointFiveGBASE_T, default);
			context.ModelState.SetModelValue(nameof(model.HasFiveGBASE_T), model.HasFiveGBASE_T, default);
			context.ModelState.SetModelValue(nameof(model.HasTenGE), model.HasTenGE, default);

			await next();
		}
	}
}
