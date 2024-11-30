namespace StructuredCablingStudio.Models.CalculationModels
{
	public class StructuredCablingStudioParameters
	{
		public StructuredCablingStudioDiapasons Diapasons { get; set; } = default!;

		public double TechnologicalReserve { get; set; }

		public RecommendationsArguments RecommendationsArguments { get; set; } = default!;

		public bool? IsRecommendationsAvailability { get; set; }

		public bool? IsStrictComplianceWithTheStandart { get; set; }

		public bool? IsAnArbitraryNumberOfPorts { get; set; }

		public bool? IsTechnologicalReserveAvailability { get; set; }
	}
}
