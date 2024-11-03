namespace StructuredCablingStudio.Models.CalculationModels
{
	public record CablingConfiguration
	{
		public required DateTime RecordTime { get; init; }

		public required double MinPermanentLink { get; init; }

		public required double MaxPermanentLink { get; init; }

		public required double AveragePermanentLink { get; init; }

		public required int NumberOfWorkplaces { get; init; }

		public required int NumberOfPorts { get; init; }

		public double? CableQuantity { get; init; } = null;

		public int? CableHankMeterage { get; init; } = null;

		public int? HankQuantity { get; init; } = null;

		public required double TotalCableQuantity { get; init; }

		public required Dictionary<string, string> Recommendations { get; init; }
	}
}
