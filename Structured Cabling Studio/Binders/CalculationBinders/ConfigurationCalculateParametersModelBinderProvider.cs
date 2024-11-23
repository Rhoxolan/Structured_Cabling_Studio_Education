using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using StructuredCablingStudio.Models.CalculationModels;

namespace StructuredCablingStudio.Binders.CalculationBinders
{
    public class ConfigurationCalculateParametersModelBinderProvider : IModelBinderProvider
	{
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context?.Metadata?.ModelType == typeof(ConfigurationCalculateParameters))
            {
                return new BinderTypeModelBinder(typeof(ConfigurationCalculateParametersModelBinder));
            }
            return null;
        }
    }
}
