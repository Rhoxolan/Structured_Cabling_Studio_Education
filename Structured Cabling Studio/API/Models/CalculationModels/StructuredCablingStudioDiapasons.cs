namespace StructuredCablingStudio.API.Models.CalculationModels
{
    public class StructuredCablingStudioDiapasons
    {
        public required StructuredCablingStudioInputDiapason MinPermanentLinkDiapason { get; init; }

        public required StructuredCablingStudioInputDiapason MaxPermanentLinkDiapason { get; init; }

        public required StructuredCablingStudioInputDiapason NumberOfPortsDiapason { get; init; }

        public required StructuredCablingStudioInputDiapason NumberOfWorkplacesDiapason { get; init; }

        public required StructuredCablingStudioInputDiapason CableHankMeterageDiapason { get; init; }

        public required StructuredCablingStudioInputDiapason TechnologicalReserveDiapason { get; init; }
    }
}
