using Microsoft.AspNetCore.Mvc.Filters;
using StructuredCablingStudio.ViewModels.CalculationViewModels;

namespace StructuredCablingStudio.Filters.CalculationFilters
{
	public class SetCableHankMeterageAvailabilityActionFilterAttribute : ActionFilterAttribute
	{
		private static readonly string _modelKey = "calculateVM";

		public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var model = (CalculateViewModel)context.ActionArguments[_modelKey]!;

			await next();
		}
	}
}
