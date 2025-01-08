using StructuredCablingStudio.DTOs.CalculationDTOs;
using StructuredCablingStudio.Models.CalculationModels;

namespace StructuredCablingStudio.Services.CalculationServices.CalculationService
{
    public interface ICalculationService
    {
        ConfigurationCalculateParameters GetConfigurationCalculateParametersDefault();

		Task<StructuredCablingStudioParameters> GetStructuredCablingStudioParametersDefaultAsync();

		CalculateDTO GetCalculateDTODefault();

		Task<StructuredCablingStudioDiapasons> SetStructuredCablingStudioDiapasonsAsync(
			StructuredCablingStudioParameters structuredCablingStudioParameters);

		Task<int> GetCeiledAveragePermanentLink(double minPermanentLink, double maxPermanentLink, double technologicalReserve);
		
		Task<CablingConfiguration> Calculate(StructuredCablingStudioParameters structuredCablingStudioParameters,
			ConfigurationCalculateParameters configurationCalculateParameters, DateTime recordTime, double minPermanentLink,
			double maxPermanentLink, int numberOfWorkplaces, int numberOfPorts);
	}
}
