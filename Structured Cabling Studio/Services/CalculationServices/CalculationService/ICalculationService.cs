using StructuredCablingStudio.DTOs.CalculationDTOs;
using StructuredCablingStudio.Models.CalculationModels;

namespace StructuredCablingStudio.Services.CalculationServices.CalculationService
{
    public interface ICalculationService
    {
        ConfigurationCalculateParameters GetConfigurationCalculateParametersDefault();

		Task<StructuredCablingStudioParameters> GetStructuredCablingStudioParametersDefaultAsync();

		CalculateDTO GetCalculateDTODefault();

		Task<StructuredCablingStudioParameters> SetStructuredCablingStudioParametersDiapasonsAsync(
			StructuredCablingStudioParameters structuredCablingStudioParameters);
	}
}
