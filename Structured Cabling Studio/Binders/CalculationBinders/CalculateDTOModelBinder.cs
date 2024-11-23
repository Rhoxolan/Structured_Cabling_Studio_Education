using Microsoft.AspNetCore.Mvc.ModelBinding;
using StructuredCablingStudio.DTOs.CalculationDTOs;
using StructuredCablingStudio.Extensions.ISessionExtension;

namespace StructuredCablingStudio.Binders.CalculationBinders
{
	public class CalculateDTOModelBinder : IModelBinder
	{
		public Task BindModelAsync(ModelBindingContext bindingContext)
		{
			if (bindingContext?.HttpContext?.Session != null)
			{
				CalculateDTO? calculateDTO = bindingContext.HttpContext.Session.GetCalculateDTO();
				bindingContext.Result = ModelBindingResult.Success(calculateDTO);
			}
			return Task.CompletedTask;
		}
	}
}
