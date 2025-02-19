﻿using Microsoft.AspNetCore.Mvc;
using StructuredCablingStudio.DTOs.CalculationDTOs;
using StructuredCablingStudio.Extensions.StructuredCablingStudioParametersExtensions;
using StructuredCablingStudio.Filters.CalculationFilters;
using StructuredCablingStudio.Models.CalculationModels;
using StructuredCablingStudio.Services.CalculationServices.CalculationService;
using StructuredCablingStudio.ViewModels.CalculationViewModels;

namespace StructuredCablingStudio.Controllers
{
    [Route("api/{controller}/{action}/{id?}")]
    public class CalculationProcessing(ICalculationService calculationService) : Controller
    {
        /// <summary>
        /// Returns the partial view with the clean Calculate form
        /// </summary>
        [HttpGet]
        [ServiceFilter(typeof(SetStructuredCablingStudioParametersActionFilterAttribute))]
        [ServiceFilter(typeof(SetConfigurationCalculateParametersActionFilterAttribute))]
        [ServiceFilter(typeof(SetCalculateDTOActionFilterAttribute))]
        [SetGetCalculateFormViewModelActionFilter]
        [SetGetCalculateFormViewDataActionFilter]
        public IActionResult GetCalculateForm(StructuredCablingStudioParameters cablingParameters,
            ConfigurationCalculateParameters calculateParameters, CalculateDTO calculateDTO)
        {
            return PartialView("_CalculateFormPartial");
        }

        /// <summary>
        /// Returns the partial view with the clean display of structured cabling configuration
        /// </summary>
        [HttpGet]
        public IActionResult GetConfigurationDisplayCalculate()
        {
            return PartialView("_ConfigurationDisplayCalculatePartial");
        }

        /// <summary>
        /// Edits the viewmodel data from the Calculate form after applying the "StrictComplianceWithTheStandart" setting
        /// </summary>
        /// <returns>The partial view with the Calculate form</returns>
        [HttpPost]
        [SetStrictComplianceWithTheStandartActionFilter]
        [ServiceFilter(typeof(SetCalculationParametersActionFilterAttribute), Order = int.MaxValue)]
        [DiapasonActionFilter]
        [StructuredCablingStudioParametersResultFilter]
        [ConfigurationCalculateParametersResultFilter]
        [CalculateDTOResultFilter]
        public IActionResult SetStrictComplianceWithTheStandart(CalculateViewModel calculateVM)
        {
            return PartialView("_CalculateFormPartial", calculateVM);
        }

        /// <summary>
        /// Edits the viewmodel data from the Calculate form after applying the "RecommendationsAvailability" setting
        /// </summary>
        /// <returns>The partial view with the Calculate form</returns>
        [HttpPost]
        [SetRecommendationsAvailabilityActionFilter]
        [ServiceFilter(typeof(SetCalculationParametersActionFilterAttribute), Order = int.MaxValue)]
        [DiapasonActionFilter]
        [StructuredCablingStudioParametersResultFilter]
        [ConfigurationCalculateParametersResultFilter]
        [CalculateDTOResultFilter]
        public IActionResult SetRecommendationsAvailability(CalculateViewModel calculateVM)
        {
            return PartialView("_CalculateFormPartial", calculateVM);
        }

        /// <summary>
        /// Edits the viewmodel data from the Сalculate form after applying the "CableHankMeterageAvailability" setting
        /// </summary>
        /// <returns>The partial view with the Calculate form</returns>
        [HttpPost]
        [ServiceFilter(typeof(SetCableHankMeterageAvailabilityActionFilterAttribute))]
        [ServiceFilter(typeof(SetCalculationParametersActionFilterAttribute), Order = int.MaxValue)]
        [DiapasonActionFilter]
        [StructuredCablingStudioParametersResultFilter]
        [ConfigurationCalculateParametersResultFilter]
        [CalculateDTOResultFilter]
        public IActionResult SetCableHankMeterageAvailability(CalculateViewModel calculateVM)
        {
            return PartialView("_CalculateFormPartial", calculateVM);
        }

        /// <summary>
        /// Edits the viewmodel data from the Сalculate form after applying the "AnArbitraryNumberOfPorts" setting
        /// </summary>
        /// <returns>The partial view with the Calculate form</returns>
        [HttpPost]
        [ServiceFilter(typeof(SetCalculationParametersActionFilterAttribute), Order = int.MaxValue)]
        [DiapasonActionFilter]
        [StructuredCablingStudioParametersResultFilter]
        [ConfigurationCalculateParametersResultFilter]
        [CalculateDTOResultFilter]
        public IActionResult SetAnArbitraryNumberOfPorts(CalculateViewModel calculateVM)
        {
            return PartialView("_CalculateFormPartial", calculateVM);
        }

        /// <summary>
        /// Edits the viewmodel data from the Сalculate form after applying the "TechnologicalReserveAvailability" setting
        /// </summary>
        /// <returns>The partial view with the Calculate form</returns>
        [HttpPost]
        [SetTechnologicalReserveAvailabilityActionFilter]
        [ServiceFilter(typeof(SetCalculationParametersActionFilterAttribute), Order = int.MaxValue)]
        [DiapasonActionFilter]
        [StructuredCablingStudioParametersResultFilter]
        [ConfigurationCalculateParametersResultFilter]
        [CalculateDTOResultFilter]
        public IActionResult SetTechnologicalReserveAvailability(CalculateViewModel calculateVM)
        {
            return PartialView("_CalculateFormPartial", calculateVM);
        }

        /// <summary>
        /// Restore defaults settings on viewmodel data to the Сalculate form
        /// </summary>
        /// <returns>The partial view with the Calculate form</returns>
        [HttpPost]
        [ServiceFilter(typeof(RestoreDefaultsCalculateFormActionFilterAttribute))]
        [ServiceFilter(typeof(SetCalculationParametersActionFilterAttribute), Order = int.MaxValue)]
        [DiapasonActionFilter]
        [StructuredCablingStudioParametersResultFilter]
        [ConfigurationCalculateParametersResultFilter]
        [CalculateDTOResultFilter]
        public IActionResult RestoreDefaultsCalculateForm(CalculateViewModel calculateVM)
        {
            return PartialView("_CalculateFormPartial", calculateVM);
        }

        /// <summary>
        /// Calculation of the structured cabling configuration
        /// </summary>
        /// <returns>The partial view with the display of the structured cabling configuration</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Calculate(CalculateViewModel calculateVM)
        {
            StructuredCablingStudioParameters structuredCablingStudioParameters = calculateVM.ToStructuredCablingStudioParameters();
            structuredCablingStudioParameters.Diapasons = await calculationService
                .SetStructuredCablingStudioDiapasonsAsync(structuredCablingStudioParameters);

            ConfigurationCalculateParameters configurationCalculateParameters = calculateVM.ToConfigurationCalculateParameters();

            var recordTime = DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(calculateVM.RecordTime)).DateTime.ToLocalTime();

            CablingConfiguration configuration = await calculationService.Calculate(structuredCablingStudioParameters, configurationCalculateParameters,
                recordTime, calculateVM.MinPermanentLink, calculateVM.MaxPermanentLink, calculateVM.NumberOfWorkplaces, calculateVM.NumberOfPorts);

			//var structuredCablingParameters = _mapper.Map<StructuredCablingParameters>(structuredCablingStudioParameters);
			//HttpContext.Session.SetStructuredCablingParameters(structuredCablingParameters);
			//var calculateParameters = _mapper.Map<CalculateParameters>(configurationCalculateParameters);
			//HttpContext.Session.SetCalculateParameters(calculateParameters);
			//var calculateDTO = _mapper.Map<CalculateDTO>(calculateVM);
			//HttpContext.Session.SetCalculateDTO(calculateDTO);

			return PartialView("_ConfigurationDisplayCalculatePartial", configuration);
        }

        [HttpPost]
        public IActionResult SaveToTXT(string serializedCablingConfiguration)
        {
            throw new NotImplementedException();
        }
    }
}
