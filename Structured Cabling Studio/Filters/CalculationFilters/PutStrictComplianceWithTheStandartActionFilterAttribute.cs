using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StructuredCablingStudio.ViewModels.CalculationViewModels;

namespace StructuredCablingStudio.Filters.CalculationFilters
{
	public class PutStrictComplianceWithTheStandartActionFilterAttribute : ActionFilterAttribute
	{
		public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var controller = (Controller)context.Controller;
			var model = (CalculateViewModel?)controller.ViewData.Model;
			if (model != null)
			{
				if (!model.IsStrictComplianceWithTheStandart)
				{
					model.IsAnArbitraryNumberOfPorts = true;
					context.ModelState.SetModelValue(nameof(model.IsAnArbitraryNumberOfPorts), model.IsAnArbitraryNumberOfPorts, default);
				}
			}
			
			await next();
		}
	}
}
