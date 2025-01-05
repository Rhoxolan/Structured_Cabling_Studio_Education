using Microsoft.AspNetCore.Mvc.Filters;
using StructuredCablingStudio.Extensions.StructuredCablingStudioParametersExtensions;
using StructuredCablingStudio.Models.CalculationModels;
using StructuredCablingStudio.ViewModels.CalculationViewModels;

namespace StructuredCablingStudio.Filters.CalculationFilters
{
	public class SetTechnologicalReserveAvailabilityActionFilterAttribute : ActionFilterAttribute
	{
		private static readonly string _modelKey = "calculateVM";

		public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var model = (CalculateViewModel)context.ActionArguments[_modelKey]!;
			StructuredCablingStudioParameters structuredCablingStudioParameters = model.ToStructuredCablingStudioParameters();

			if (model.TechnologicalReserve != structuredCablingStudioParameters.TechnologicalReserve)
			{
				model.TechnologicalReserve = structuredCablingStudioParameters.TechnologicalReserve;
				context.ModelState.SetModelValue(nameof(model.TechnologicalReserve), model.TechnologicalReserve, default);
			}

			await next();
		}
	}
}
