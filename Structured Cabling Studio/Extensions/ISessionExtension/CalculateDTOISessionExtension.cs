using System.Text.Json.Serialization;
using System.Text.Json;
using StructuredCablingStudio.DTOs.CalculationDTOs;

namespace StructuredCablingStudio.Extensions.ISessionExtension
{
	public static class CalculateDTOISessionExtension
	{
		private static readonly string key = nameof(CalculateDTO);
		private static readonly JsonSerializerOptions jsonSerializerOptions = new()
		{
			WriteIndented = true,
			ReferenceHandler = ReferenceHandler.Preserve
		};

		public static void SetCalculateDTO(this ISession session, CalculateDTO parameters)
		{
			session.SetString(key, JsonSerializer.Serialize(parameters, jsonSerializerOptions));
		}

		public static CalculateDTO? GetCalculateDTO(this ISession session)
		{
			string? str = session.GetString(key);
			if (str != null)
			{
				return JsonSerializer.Deserialize<CalculateDTO>(str, jsonSerializerOptions);
			}
			return null;
		}
	}
}
