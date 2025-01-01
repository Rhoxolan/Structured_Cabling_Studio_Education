using StructuredCablingStudio.DTOs.CalculationDTOs;
using StructuredCablingStudio.Models.CalculationModels;
using StructuredCablingStudio.ViewModels.CalculationViewModels;

namespace StructuredCablingStudio.Extensions.StructuredCablingStudioParametersExtensions
{
	public static class CalculateViewModelExtension
	{
		public static StructuredCablingStudioParameters ToStructuredCablingStudioParameters(this CalculateViewModel model)
		{
			var structuredCablingStudioParameters = new StructuredCablingStudioParameters
			{
				IsAnArbitraryNumberOfPorts = model.IsAnArbitraryNumberOfPorts,
				IsRecommendationsAvailability = model.IsRecommendationsAvailability,
				IsStrictComplianceWithTheStandart = model.IsStrictComplianceWithTheStandart,
				IsTechnologicalReserveAvailability = model.IsTechnologicalReserveAvailability,
				TechnologicalReserve = model.IsTechnologicalReserveAvailability ? model.TechnologicalReserve : 1.0
			};
			structuredCablingStudioParameters.RecommendationsArguments = new()
			{
				IsolationType = model.IsCableRouteRunOutdoors ? IsolationType.Outdoor : IsolationType.Indoor,
				IsolationMaterial = model.IsConsiderFireSafetyRequirements ? IsolationMaterial.LSZH : IsolationMaterial.PVC,
				ShieldedType = model.IsCableShieldingNecessity ? ShieldedType.FTP : ShieldedType.UTP
			};
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
			return structuredCablingStudioParameters;
		}

		public static ConfigurationCalculateParameters ToConfigurationCalculateParameters(this CalculateViewModel model)
		{
			var configurationCalculateParameters = new ConfigurationCalculateParameters
			{
				IsCableHankMeterageAvailability = model.IsCableHankMeterageAvailability,
			};
			if (model.IsCableHankMeterageAvailability)
			{
				configurationCalculateParameters.CableHankMeterage = model.CableHankMeterage;
			}

			return configurationCalculateParameters;
		}

		public static CalculateDTO ToCalculateDTO(this CalculateViewModel model)
		{
			return new CalculateDTO
			{
				MaxPermanentLink = model.MaxPermanentLink,
				MinPermanentLink = model.MinPermanentLink,
				NumberOfPorts = model.NumberOfPorts,
				NumberOfWorkplaces = model.NumberOfWorkplaces
			};
		}
	}
}
