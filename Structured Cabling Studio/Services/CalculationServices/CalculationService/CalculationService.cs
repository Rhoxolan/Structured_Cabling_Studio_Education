using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StructuredCablingStudio.Contexts;
using StructuredCablingStudio.DTOs.CalculationDTOs;
using StructuredCablingStudio.Models.CalculationModels;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;
using static StructuredCablingStudio.Properties.Resources;

namespace StructuredCablingStudio.Services.CalculationServices.CalculationService
{
	public class CalculationService(ApplicationContext context) : ICalculationService
	{
		public async Task<StructuredCablingStudioParameters> GetStructuredCablingStudioParametersDefaultAsync()
		{
			StructuredCablingStudioParameters inputParameters = GetStructuredCablingStudioParametersDefaultValue();
			XmlDocument inputdocument = SerializeToXML(inputParameters);

			var documentParameter = new SqlParameter("StructuredCablingStudioParameters", inputdocument.OuterXml);
			await context.Database.ExecuteSqlAsync($@"EXEC Calculation.SetStructuredCablingStudioParametersDiapasons 
																	@StructuredCablingStudioParameters = {documentParameter} OUTPUT");

			var outputDocument = new XmlDocument();
			outputDocument.LoadXml(documentParameter.Value.ToString()!);
			var outputParameters = DeserializeFromXML<StructuredCablingStudioParameters>(outputDocument)!;

			return outputParameters;
		}

		public ConfigurationCalculateParameters GetConfigurationCalculateParametersDefault()
		{
			return GetConfigurationCalculateParametersDefaultValue();
		}

		public CalculateDTO GetCalculateDTODefault()
		{
			return GetCalculateDTODefaultValue();
		}

		private static StructuredCablingStudioParameters GetStructuredCablingStudioParametersDefaultValue()
		{
			return new StructuredCablingStudioParameters
			{
				IsStrictComplianceWithTheStandart = bool.Parse(
					StructuredCablingStudioParameters_IsStrictComplianceWithTheStandart_StandartValue),

				IsAnArbitraryNumberOfPorts = bool.Parse(
					StructuredCablingStudioParameters_IsAnArbitraryNumberOfPorts_StandartValue),

				IsTechnologicalReserveAvailability = bool.Parse(
					StructuredCablingStudioParameters_IsTechnologicalReserveAvailability_StandartValue),

				IsRecommendationsAvailability = bool.Parse(
					StructuredCablingStudioParameters_IsRecommendationsAvailability_StandartValue),

				TechnologicalReserve = double.Parse(
					StructuredCablingStudioParameters_TechnologicalReserve_StandartValue,
					CultureInfo.InvariantCulture),

				RecommendationsArguments = new RecommendationsArguments
				{
					IsolationType = Enum.Parse<IsolationType>(
						StructuredCablingStudioParameters_RecommendationsArguments_IsolationType_StandartValue),

					IsolationMaterial = Enum.Parse<IsolationMaterial>(
						StructuredCablingStudioParameters_RecommendationsArguments_IsolationMaterial_StandartValue),

					ShieldedType = Enum.Parse<ShieldedType>(
						StructuredCablingStudioParameters_RecommendationsArguments_ShieldedType_StandartValue),

					ConnectionInterfaces = StructuredCablingStudioParameters_RecommendationsArguments_ConnectionInterfaces_StandartValue
					.Split(',')
					.Select(Enum.Parse<ConnectionInterfaceStandard>)
					.ToList()

				}
			};
		}

		private static ConfigurationCalculateParameters GetConfigurationCalculateParametersDefaultValue()
		{
			return new ConfigurationCalculateParameters
			{
				IsCableHankMeterageAvailability = bool.Parse(
					ConfigurationCalculateParameters_IsCableHankMeterageAvailability_StandartValue),

				CableHankMeterage = int.Parse(ConfigurationCalculateParameters_CableHankMeterage_StandartValue)

			};
		}

		private static CalculateDTO GetCalculateDTODefaultValue()
		{
			return new CalculateDTO
			{
				MinPermanentLink = double.Parse(CalculateDTO_MinPermanentLink_StandartValue, CultureInfo.InvariantCulture),
				MaxPermanentLink = double.Parse(CalculateDTO_MaxPermanentLink_StandartValue, CultureInfo.InvariantCulture),
				NumberOfPorts = int.Parse(CalculateDTO_NumberOfPorts_StandartValue),
				NumberOfWorkplaces = int.Parse(CalculateDTO_NumberOfWorkplaces_StandartValue),
			};
		}

		private XmlDocument SerializeToXML<T>(T obj)
		{
			var serializer = new XmlSerializer(typeof(T));
			var xmlDocument = new XmlDocument();
			using var stringWriter = new StringWriter();

			serializer.Serialize(stringWriter, obj);
			xmlDocument.Load(stringWriter.ToString());

			return xmlDocument;
		}

		private T? DeserializeFromXML<T>(XmlDocument xmlDocument)
		{
			var serializer = new XmlSerializer(typeof(T));
			using var stringReader = new StringReader(xmlDocument.OuterXml);
			return (T?)serializer.Deserialize(stringReader);
		}
	}
}
