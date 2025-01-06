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

		public static CalculateViewModel FromCablingConfigurationParameters(this CalculateViewModel model,
			StructuredCablingStudioParameters structuredCablingStudioParameters, ConfigurationCalculateParameters configurationCalculateParameters,
			CalculateDTO calculateDTO)
		{
			return new CalculateViewModel
			{
				IsStrictComplianceWithTheStandart = structuredCablingStudioParameters.IsStrictComplianceWithTheStandart.GetValueOrDefault(),
				IsRecommendationsAvailability = structuredCablingStudioParameters.IsRecommendationsAvailability.GetValueOrDefault(),
				IsTechnologicalReserveAvailability = structuredCablingStudioParameters.IsTechnologicalReserveAvailability.GetValueOrDefault(),
				IsAnArbitraryNumberOfPorts = structuredCablingStudioParameters.IsAnArbitraryNumberOfPorts.GetValueOrDefault(),
				TechnologicalReserve = structuredCablingStudioParameters.TechnologicalReserve,
				IsCableHankMeterageAvailability = configurationCalculateParameters.IsCableHankMeterageAvailability.GetValueOrDefault(),
				CableHankMeterage = configurationCalculateParameters.CableHankMeterage,
				MinPermanentLink = calculateDTO.MinPermanentLink,
				MaxPermanentLink = calculateDTO.MaxPermanentLink,
				NumberOfPorts = calculateDTO.NumberOfPorts,
				NumberOfWorkplaces = calculateDTO.NumberOfWorkplaces,
				IsCableRouteRunOutdoors = structuredCablingStudioParameters.RecommendationsArguments.IsolationType == IsolationType.Outdoor,
				IsConsiderFireSafetyRequirements = structuredCablingStudioParameters.RecommendationsArguments.IsolationMaterial == IsolationMaterial.LSZH,
				IsCableShieldingNecessity = structuredCablingStudioParameters.RecommendationsArguments.ShieldedType == ShieldedType.FTP,
				HasTenBase_T = structuredCablingStudioParameters.RecommendationsArguments.ConnectionInterfaces.Contains(ConnectionInterfaceStandard.TenBASE_T),
				HasFastEthernet = structuredCablingStudioParameters.RecommendationsArguments.ConnectionInterfaces.Contains(ConnectionInterfaceStandard.FastEthernet),
				HasGigabitBASE_T = structuredCablingStudioParameters.RecommendationsArguments.ConnectionInterfaces.Contains(ConnectionInterfaceStandard.GigabitBASE_T),
				HasGigabitBASE_TX = structuredCablingStudioParameters.RecommendationsArguments.ConnectionInterfaces.Contains(ConnectionInterfaceStandard.GigabitBASE_TX),
				HasTwoPointFiveGBASE_T = structuredCablingStudioParameters.RecommendationsArguments.ConnectionInterfaces.Contains(ConnectionInterfaceStandard.TwoPointFiveGBASE_T),
				HasFiveGBASE_T = structuredCablingStudioParameters.RecommendationsArguments.ConnectionInterfaces.Contains(ConnectionInterfaceStandard.FiveGBASE_T),
				HasTenGE = structuredCablingStudioParameters.RecommendationsArguments.ConnectionInterfaces.Contains(ConnectionInterfaceStandard.TenGE)
			};
		}
	}
}
