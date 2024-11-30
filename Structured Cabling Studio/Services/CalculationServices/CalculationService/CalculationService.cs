﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StructuredCablingStudio.Contexts;
using StructuredCablingStudio.DTOs.CalculationDTOs;
using StructuredCablingStudio.Models.CalculationModels;
using System.Xml;
using System.Xml.Serialization;

namespace StructuredCablingStudio.Services.CalculationServices.CalculationService
{
    public class CalculationService(ApplicationContext context) : ICalculationService
    {
        public async Task<StructuredCablingStudioParameters> GetStructuredCablingStudioParametersDefaultAsync()
        {
			var inputParameters = GetStructuredCablingStudioParametersDefaultValue();
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
				IsStrictComplianceWithTheStandart = true,
				IsAnArbitraryNumberOfPorts = true,
				IsTechnologicalReserveAvailability = true,
				IsRecommendationsAvailability = true,
				TechnologicalReserve = 1.10,
				RecommendationsArguments = new RecommendationsArguments
				{
					IsolationType = IsolationType.Indoor,
					IsolationMaterial = IsolationMaterial.LSZH,
					ShieldedType = ShieldedType.UTP,
					ConnectionInterfaces = new List<ConnectionInterfaceStandard>
					{
						ConnectionInterfaceStandard.FastEthernet,
						ConnectionInterfaceStandard.GigabitBASE_T
					}
				}
			};
		}

		private static ConfigurationCalculateParameters GetConfigurationCalculateParametersDefaultValue()
		{
			return new ConfigurationCalculateParameters
			{
				IsCableHankMeterageAvailability = true,
				CableHankMeterage = 305
			};
		}

		private static CalculateDTO GetCalculateDTODefaultValue()
		{
			return new CalculateDTO
			{
				MinPermanentLink = 25,
				MaxPermanentLink = 85,
				NumberOfPorts = 1,
				NumberOfWorkplaces = 10,
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