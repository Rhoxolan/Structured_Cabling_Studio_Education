using Microsoft.AspNetCore.Mvc.Filters;
using StructuredCablingStudio.Extensions.ISessionExtension;
using StructuredCablingStudio.Services.CalculationServices.CalculationService;

namespace StructuredCablingStudio.Filters.CalculationFilters
{
	public class SetCalculateDTOActionFilterAttribute(ICalculationService calculationService) : ActionFilterAttribute
	{
		private static readonly string _actionArgumentsKey = "calculateDTO";

		public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			if (context.ActionArguments[_actionArgumentsKey] == null)
			{
				var calculateDTO = calculationService.GetCalculateDTODefault();
				context.HttpContext.Session?.SetCalculateDTO(calculateDTO);
				context.ActionArguments[_actionArgumentsKey] = calculateDTO;
			}

			await next();
		}
	}
}
