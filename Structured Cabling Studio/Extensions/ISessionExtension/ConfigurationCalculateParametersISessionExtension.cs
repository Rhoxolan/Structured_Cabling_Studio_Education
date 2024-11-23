using StructuredCablingStudio.Models.CalculationModels;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace StructuredCablingStudio.Extensions.ISessionExtension
{
	public static class ConfigurationCalculateParametersISessionExtension
	{
		private static readonly string key = nameof(ConfigurationCalculateParameters);
		private static readonly JsonSerializerOptions jsonSerializerOptions = new()
		{
			WriteIndented = true,
			ReferenceHandler = ReferenceHandler.Preserve
		};

		public static void SetConfigurationCalculateParameters(this ISession session, ConfigurationCalculateParameters parameters)
		{
			session.SetString(key, JsonSerializer.Serialize(parameters, jsonSerializerOptions));
		}

		public static ConfigurationCalculateParameters? GetConfigurationCalculateParameters(this ISession session)
		{
			string? str = session.GetString(key);
			if (str != null)
			{
				return JsonSerializer.Deserialize<ConfigurationCalculateParameters>(str, jsonSerializerOptions);
			}
			return null;
		}
	}
}
