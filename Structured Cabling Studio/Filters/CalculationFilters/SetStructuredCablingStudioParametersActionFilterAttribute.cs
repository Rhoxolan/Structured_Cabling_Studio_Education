using Microsoft.AspNetCore.Mvc.Filters;
using StructuredCablingStudio.Extensions.ISessionExtension;
using StructuredCablingStudio.Models.CalculationModels;

namespace StructuredCablingStudio.Filters.CalculationFilters
{
	public class SetStructuredCablingStudioParametersActionFilterAttribute : ActionFilterAttribute
	{
		private static readonly string _actionArgumentsKey = "cablingParameters";

		public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			if (context.ActionArguments[_actionArgumentsKey] == null)
			{
				var parameters = new StructuredCablingStudioParameters
				{
					IsStrictComplianceWithTheStandart = true,
					IsAnArbitraryNumberOfPorts = true,
					IsTechnologicalReserveAvailability = true,
					IsRecommendationsAvailability = true
				};
				parameters.RecommendationsArguments.IsolationType = IsolationType.Indoor;
				parameters.RecommendationsArguments.IsolationMaterial = IsolationMaterial.LSZH;
				parameters.RecommendationsArguments.ShieldedType = ShieldedType.UTP;
				parameters.RecommendationsArguments.ConnectionInterfaces = new List<ConnectionInterfaceStandard>
				{
					ConnectionInterfaceStandard.FastEthernet,
					ConnectionInterfaceStandard.GigabitBASE_T
				};
				context.HttpContext.Session?.SetStructuredCablingStudioParameters(parameters);
				context.ActionArguments[_actionArgumentsKey] = parameters;
			}

			await next();
		}
	}
}
