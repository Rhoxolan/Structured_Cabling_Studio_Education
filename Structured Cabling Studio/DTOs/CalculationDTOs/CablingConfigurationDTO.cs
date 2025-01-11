using System.Xml.Serialization;

namespace StructuredCablingStudio.DTOs.CalculationDTOs
{
	[XmlRoot("CablingConfiguration")]
	public class CablingConfigurationDTO
	{
		public DateTime RecordTime { get; set; }

		public required double MinPermanentLink { get; set; }

		public required double MaxPermanentLink { get; set; }

		public required double AveragePermanentLink { get; set; }

		public required int NumberOfWorkplaces { get; set; }

		public required int NumberOfPorts { get; set; }

		public double? CableQuantity { get; set; }

		public int? CableHankMeterage { get; set; }

		public int? HankQuantity { get; set; }

		public required double TotalCableQuantity { get; set; }

		public required string Recommendations { get; set; }
	}
}
