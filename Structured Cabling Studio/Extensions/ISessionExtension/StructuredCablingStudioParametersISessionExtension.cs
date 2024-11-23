using System.Text.Json.Serialization;
using System.Text.Json;
using StructuredCablingStudio.Models.CalculationModels;

namespace StructuredCablingStudio.Extensions.ISessionExtension
{
	public static class StructuredCablingStudioParametersISessionExtension
	{
		private static readonly string key = nameof(StructuredCablingStudioParameters);
		private static readonly JsonSerializerOptions jsonSerializerOptions = new()
		{
			WriteIndented = true,
			ReferenceHandler = ReferenceHandler.Preserve
		};

		public static void SetStructuredCablingStudioParameters(this ISession session, StructuredCablingStudioParameters parameters)
		{
			session.SetString(key, JsonSerializer.Serialize(parameters, jsonSerializerOptions));
		}

		public static StructuredCablingStudioParameters? GetStructuredCablingStudioParameters(this ISession session)
		{
			string? str = session.GetString(key);
			if (str != null)
			{
				return JsonSerializer.Deserialize<StructuredCablingStudioParameters>(str, jsonSerializerOptions);
			}
			return null;
		}
	}
}
