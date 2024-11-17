using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data;
using System.Data.Common;
using System.Security.Claims;

namespace StructuredCablingStudio.Interceptors
{
	public class ConfigureSessionContextInterceptor(IHttpContextAccessor httpContextAccessor) : DbConnectionInterceptor
	{
		public override async Task ConnectionOpenedAsync(DbConnection connection, ConnectionEndEventData eventData,
			CancellationToken cancellationToken = default)
		{
			var userId = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);

			using var command = connection.CreateCommand();

			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "Calculation.SetAuthorizationSessionContext";
			command.Parameters.Add(new SqlParameter("@UserId", userId?.Value));

			await command.ExecuteNonQueryAsync(cancellationToken);

			await base.ConnectionOpenedAsync(connection, eventData, cancellationToken);
		}
	}
}
