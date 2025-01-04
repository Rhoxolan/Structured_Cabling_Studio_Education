using Microsoft.AspNetCore.Mvc.Filters;
using StructuredCablingStudio.DTOs.CalculationDTOs;
using StructuredCablingStudio.Extensions.ISessionExtension;
namespace StructuredCablingStudio.Filters.CalculationFilters
{
	public class CalculateDTOResultFilterAttribute : ActionFilterAttribute
	{
		private static readonly string _calculateModelKey = "calculateModel";

		public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
		{
			var calculateModel = (CalculateDTO)context.HttpContext.Items[_calculateModelKey]!;
			context.HttpContext.Session.SetCalculateDTO(calculateModel);

			await next();
		}
	}
}
