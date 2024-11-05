namespace StructuredCablingStudio.Entities
{
	public record CablingConfiguration : Models.CalculationModels.CablingConfiguration
	{
		public uint Id { get; set; }

		public User User { get; set; } = default!;
	}
}
