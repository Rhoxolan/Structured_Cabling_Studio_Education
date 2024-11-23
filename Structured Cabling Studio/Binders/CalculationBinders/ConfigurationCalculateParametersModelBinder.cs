using Microsoft.AspNetCore.Mvc.ModelBinding;
using StructuredCablingStudio.Extensions.ISessionExtension;
using StructuredCablingStudio.Models.CalculationModels;

namespace StructuredCablingStudio.Binders.CalculationBinders
{
    public class ConfigurationCalculateParametersModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext?.HttpContext?.Session != null)
            {
                ConfigurationCalculateParameters? parameters = bindingContext.HttpContext.Session.GetConfigurationCalculateParameters();
                bindingContext.Result = ModelBindingResult.Success(parameters);
            }
            return Task.CompletedTask;
        }
    }
}
