namespace Structured_Cabling_Studio.Models
{
	public class StructuredCablingStudioParameters
	{
		public StructuredCablingStudioDiapasons Diapasons { get; }

		public double TechnologicalReserve { get; set; }

		public RecommendationsArguments RecommendationsArguments { get; }

		public bool? IsRecommendationsAvailability { get; set; }

		public bool? IsStrictComplianceWithTheStandart { get; set; }

		public bool? IsAnArbitraryNumberOfPorts { get; set; }

		public bool? IsTechnologicalReserveAvailability { get; set; }
	}
}
