using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StructuredCablingStudio.Extensions.ISessionExtension;
using StructuredCablingStudio.Models.CalculationModels;
using StructuredCablingStudio.Services.CalculationServices.CalculationService;
using StructuredCablingStudio.ViewModels.CalculationViewModels;

namespace StructuredCablingStudio.Filters.CalculationFilters
{
	public class StructuredCablingStudioParametersResultFilterAttribute(ICalculationService calculationService) : ActionFilterAttribute
	{
		private static readonly string _viewDataDiapasonsKey = "Diapasons";

		public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
		{
			var controller = (Controller)context.Controller;
			var model = (CalculateViewModel?)controller.ViewData.Model;
			if (model != null)
			{
				var structuredCablingStudioParameters = new StructuredCablingStudioParameters
				{
					IsAnArbitraryNumberOfPorts = model.IsAnArbitraryNumberOfPorts,
					IsRecommendationsAvailability = model.IsRecommendationsAvailability,
					IsStrictComplianceWithTheStandart = model.IsStrictComplianceWithTheStandart,
					IsTechnologicalReserveAvailability = model.IsTechnologicalReserveAvailability,
					TechnologicalReserve = model.TechnologicalReserve
				};
				structuredCablingStudioParameters.RecommendationsArguments.IsolationType = model.IsCableRouteRunOutdoors ? IsolationType.Outdoor : IsolationType.Indoor;
				structuredCablingStudioParameters.RecommendationsArguments.IsolationMaterial = model.IsConsiderFireSafetyRequirements ? IsolationMaterial.LSZH : IsolationMaterial.PVC;
				structuredCablingStudioParameters.RecommendationsArguments.ShieldedType = model.IsCableShieldingNecessity ? ShieldedType.FTP : ShieldedType.UTP;
				if (model.HasTenBase_T)
				{
					structuredCablingStudioParameters.RecommendationsArguments.ConnectionInterfaces.Add(ConnectionInterfaceStandard.TenBASE_T);
				}
				if (model.HasFastEthernet)
				{
					structuredCablingStudioParameters.RecommendationsArguments.ConnectionInterfaces.Add(ConnectionInterfaceStandard.FastEthernet);
				}
				if (model.HasGigabitBASE_T)
				{
					structuredCablingStudioParameters.RecommendationsArguments.ConnectionInterfaces.Add(ConnectionInterfaceStandard.GigabitBASE_T);
				}
				if (model.HasGigabitBASE_TX)
				{
					structuredCablingStudioParameters.RecommendationsArguments.ConnectionInterfaces.Add(ConnectionInterfaceStandard.GigabitBASE_TX);
				}
				if (model.HasTwoPointFiveGBASE_T)
				{
					structuredCablingStudioParameters.RecommendationsArguments.ConnectionInterfaces.Add(ConnectionInterfaceStandard.TwoPointFiveGBASE_T);
				}
				if (model.HasFiveGBASE_T)
				{
					structuredCablingStudioParameters.RecommendationsArguments.ConnectionInterfaces.Add(ConnectionInterfaceStandard.FiveGBASE_T);
				}
				if (model.HasTenGE)
				{
					structuredCablingStudioParameters.RecommendationsArguments.ConnectionInterfaces.Add(ConnectionInterfaceStandard.TenGE);
				}
				structuredCablingStudioParameters
					= await calculationService.SetStructuredCablingStudioParametersDiapasonsAsync(structuredCablingStudioParameters);

				controller.ViewData[_viewDataDiapasonsKey] = structuredCablingStudioParameters.Diapasons;

				context.HttpContext.Session.SetStructuredCablingStudioParameters(structuredCablingStudioParameters);
			}

			await next();
		}
	}
}
