using Microsoft.AspNetCore.Mvc;
using StructuredCablingStudio.Models;

namespace StructuredCablingStudio.API.Controllers.Calculation
{
	[Route("api/{controller}/{action}/{id?}")]
	public class CalculateController : Controller
	{
		/// <summary>
		/// Returns the partial view with the clean Calculate form
		/// </summary>
		[HttpGet]
		public IActionResult GetCalculateForm(StructuredCablingStudioParameters cablingParameters,
			ConfigurationCalculateParameters calculateParameters/*,
			CalculateDTO calculateDTO*/)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Returns the partial view with the clean display of structured cabling configuration
		/// </summary>
		[HttpGet]
		public IActionResult GetConfigurationDisplayCalculate()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Edits the viewmodel data from the Calculate form after applying the "StrictComplianceWithTheStandart" setting
		/// </summary>
		/// <returns>The partial view with the Calculate form</returns>
		[HttpPut]
		public IActionResult PutStrictComplianceWithTheStandart(/*CalculateViewModel calculateVM*/)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Edits the viewmodel data from the Calculate form after applying the "RecommendationsAvailability" setting
		/// </summary>
		/// <returns>The partial view with the Calculate form</returns>
		[HttpPut]
		public IActionResult PutRecommendationsAvailability(/*CalculateViewModel calculateVM*/)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Edits the viewmodel data from the Сalculate form after applying the "CableHankMeterageAvailability" setting
		/// </summary>
		/// <returns>The partial view with the Calculate form</returns>
		[HttpPut]
		public IActionResult PutCableHankMeterageAvailability(/*CalculateViewModel calculateVM*/)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Edits the viewmodel data from the Сalculate form after applying the "AnArbitraryNumberOfPorts" setting
		/// </summary>
		/// <returns>The partial view with the Calculate form</returns>
		[HttpPut]
		public IActionResult PutAnArbitraryNumberOfPorts(/*CalculateViewModel calculateVM*/)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Edits the viewmodel data from the Сalculate form after applying the "TechnologicalReserveAvailability" setting
		/// </summary>
		/// <returns>The partial view with the Calculate form</returns>
		[HttpPut]
		public IActionResult PutTechnologicalReserveAvailability(/*CalculateViewModel calculateVM*/)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Restore defaults settings on viewmodel data to the Сalculate form
		/// </summary>
		/// <returns>The partial view with the Calculate form</returns>
		[HttpPut]
		public IActionResult RestoreDefaultsCalculateForm(/*CalculateViewModel calculateVM*/)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Calculation of the structured cabling configuration
		/// </summary>
		/// <returns>The partial view with the display of the structured cabling configuration</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Calculate(/*CalculateViewModel calculateVM*/)
		{
			throw new NotImplementedException();
		}
	}
}
