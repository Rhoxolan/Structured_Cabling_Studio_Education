using Microsoft.AspNetCore.Mvc.Filters;
using StructuredCablingStudio.Extensions.StructuredCablingStudioParametersExtensions;
using StructuredCablingStudio.Models.CalculationModels;
using StructuredCablingStudio.Services.CalculationServices.CalculationService;
using StructuredCablingStudio.ViewModels.CalculationViewModels;

namespace StructuredCablingStudio.Filters.CalculationFilters
{
	public class SetCableHankMeterageAvailabilityActionFilterAttribute(ICalculationService calculationService) : ActionFilterAttribute
	{
		private static readonly string _modelKey = "calculateVM";

		public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var model = (CalculateViewModel)context.ActionArguments[_modelKey]!;
			ConfigurationCalculateParameters configurationCalculateParameters = model.ToConfigurationCalculateParameters();

			if (configurationCalculateParameters.CableHankMeterage is null
				&& Equals(configurationCalculateParameters.IsCableHankMeterageAvailability, true))
			{
				configurationCalculateParameters.CableHankMeterage
					= calculationService.GetConfigurationCalculateParametersDefault().CableHankMeterage;
			}

			if (model.CableHankMeterage != configurationCalculateParameters.CableHankMeterage)
			{
				model.CableHankMeterage = configurationCalculateParameters.CableHankMeterage;
				context.ModelState.SetModelValue(nameof(model.CableHankMeterage), model.CableHankMeterage, default);
			}

			var ceiledAveragePermanentLink = await calculationService.GetCeiledAveragePermanentLink(model.MinPermanentLink,
				model.MaxPermanentLink, model.TechnologicalReserve);

			if (model.CableHankMeterage < ceiledAveragePermanentLink)
			{
				model.CableHankMeterage = ceiledAveragePermanentLink;
				context.ModelState.SetModelValue(nameof(model.CableHankMeterage), model.CableHankMeterage, default);
			}

			await next();
		}
	}
}
