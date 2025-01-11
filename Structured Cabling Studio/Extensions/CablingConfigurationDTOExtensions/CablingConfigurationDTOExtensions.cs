using StructuredCablingStudio.DTOs.CalculationDTOs;
using StructuredCablingStudio.Models.CalculationModels;
using System.Text.Json;

namespace StructuredCablingStudio.Extensions.CablingConfigurationDTOExtensions
{
	public static class CablingConfigurationDTOExtensions
	{
		public static CablingConfiguration ToCablingConfiguration(this CablingConfigurationDTO dto)
		{
			return new CablingConfiguration
			{
				RecordTime = dto.RecordTime,
				MinPermanentLink = dto.MinPermanentLink,
				MaxPermanentLink = dto.MaxPermanentLink,
				AveragePermanentLink = dto.AveragePermanentLink,
				NumberOfWorkplaces = dto.NumberOfWorkplaces,
				NumberOfPorts = dto.NumberOfPorts,
				CableQuantity = dto.CableQuantity,
				CableHankMeterage = dto.CableHankMeterage,
				HankQuantity = dto.HankQuantity,
				TotalCableQuantity = dto.TotalCableQuantity,
				Recommendations = !string.IsNullOrEmpty(dto.Recommendations)
				? JsonSerializer.Deserialize<Dictionary<string, string>>(dto.Recommendations)!
				: []
			};
		}
	}
}
