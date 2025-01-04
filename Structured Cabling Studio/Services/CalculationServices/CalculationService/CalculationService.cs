using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StructuredCablingStudio.Contexts;
using StructuredCablingStudio.DTOs.CalculationDTOs;
using StructuredCablingStudio.Models.CalculationModels;
using System.Data;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;
using static StructuredCablingStudio.Properties.Resources;

namespace StructuredCablingStudio.Services.CalculationServices.CalculationService
{
	public class CalculationService(ApplicationContext context) : ICalculationService
	{
		public async Task<StructuredCablingStudioDiapasons> SetStructuredCablingStudioDiapasonsAsync(
			StructuredCablingStudioParameters structuredCablingStudioParameters)
		{
			return await SetStructuredCablingStudioDiapasonsAsync_internal(structuredCablingStudioParameters);
		}

		public async Task<int> GetCeiledAveragePermanentLink(double minPermanentLink, double maxPermanentLink, double technologicalReserve)
		{
			var ceiledAveragePermanentLinkParameter = new SqlParameter("CeiledAveragePermanentLink", SqlDbType.Int)
			{
				Direction = ParameterDirection.Output,
			};

			await context.Database.ExecuteSqlAsync($@"EXEC Calculation.CalculateCeiledAveragePermanentLink
																	@MinPermanentLink = {minPermanentLink},
																	@MaxPermanentLink = {maxPermanentLink},
																	@TechnologicalReserve = {technologicalReserve},
																	@CeiledAveragePermanentLink = {ceiledAveragePermanentLinkParameter} OUTPUT");

			return (int)ceiledAveragePermanentLinkParameter.Value;
		}

		public async Task<StructuredCablingStudioParameters> GetStructuredCablingStudioParametersDefaultAsync()
		{
			StructuredCablingStudioParameters structuredCablingStudioParameters = GetStructuredCablingStudioParametersDefaultValue();
			structuredCablingStudioParameters.Diapasons = await SetStructuredCablingStudioDiapasonsAsync_internal(structuredCablingStudioParameters);

			return structuredCablingStudioParameters;
		}

		public ConfigurationCalculateParameters GetConfigurationCalculateParametersDefault()
		{
			return GetConfigurationCalculateParametersDefaultValue();
		}

		public CalculateDTO GetCalculateDTODefault()
		{
			return GetCalculateDTODefaultValue();
		}

		private async Task<StructuredCablingStudioDiapasons> SetStructuredCablingStudioDiapasonsAsync_internal(
			StructuredCablingStudioParameters structuredCablingStudioParameters)
		{
			XmlDocument structuredCablingStudioParametersXMLDocument = SerializeToXML(structuredCablingStudioParameters);

			var structuredCablingStudioParametersParameter = new SqlParameter("StructuredCablingStudioParameters", SqlDbType.Xml)
			{
				SqlValue = structuredCablingStudioParametersXMLDocument.OuterXml,
			};

			var structuredCablingStudioDiapasonsParameter = new SqlParameter("StructuredCablingStudioDiapasons", SqlDbType.Xml)
			{
				Direction = ParameterDirection.Output
			};

			await context.Database.ExecuteSqlAsync($@"EXEC Calculation.SetStructuredCablingStudioParametersDiapasons
																	@StructuredCablingStudioParameters = {structuredCablingStudioParametersParameter},
																	@StructuredCablingStudioDiapasons = {structuredCablingStudioDiapasonsParameter} OUTPUT");

			var structuredCablingStudioDiapasonsXMLDocument = new XmlDocument();
			structuredCablingStudioDiapasonsXMLDocument.LoadXml(structuredCablingStudioDiapasonsParameter.Value.ToString()!);
			var structuredCablingStudioDiapasons = DeserializeFromXML<StructuredCablingStudioDiapasons>(structuredCablingStudioDiapasonsXMLDocument)!;

			return structuredCablingStudioDiapasons;
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
			xmlDocument.LoadXml(stringWriter.ToString());

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
