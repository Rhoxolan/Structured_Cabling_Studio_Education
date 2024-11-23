using Microsoft.AspNetCore.Mvc.ModelBinding;
using StructuredCablingStudio.Extensions.ISessionExtension;
using StructuredCablingStudio.Models.CalculationModels;

namespace StructuredCablingStudio.Binders.CalculationBinders
{
    public class StructuredCablingStudioParametersModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext?.HttpContext?.Session != null)
            {
                StructuredCablingStudioParameters? parameters = bindingContext.HttpContext.Session.GetStructuredCablingStudioParameters();
                bindingContext.Result = ModelBindingResult.Success(parameters);
            }
            return Task.CompletedTask;
        }
    }
}
