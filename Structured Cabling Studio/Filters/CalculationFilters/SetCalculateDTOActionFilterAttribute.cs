using Microsoft.AspNetCore.Mvc.Filters;
using StructuredCablingStudio.DTOs.CalculationDTOs;
using StructuredCablingStudio.Extensions.ISessionExtension;

namespace StructuredCablingStudio.Filters.CalculationFilters
{
	public class SetCalculateDTOActionFilterAttribute : ActionFilterAttribute
	{
		private static readonly string _actionArgumentsKey = "calculateDTO";

		public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			if (context.ActionArguments[_actionArgumentsKey] == null)
			{
				var calculateDTO = new CalculateDTO
				{
					MinPermanentLink = 25,
					MaxPermanentLink = 85,
					NumberOfPorts = 1,
					NumberOfWorkplaces = 10,
				};
				context.HttpContext.Session?.SetCalculateDTO(calculateDTO);
				context.ActionArguments[_actionArgumentsKey] = calculateDTO;
			}

			await next();
		}
	}
}
