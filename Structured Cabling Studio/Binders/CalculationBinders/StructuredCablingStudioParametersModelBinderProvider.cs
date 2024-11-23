using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using StructuredCablingStudio.Models.CalculationModels;

namespace StructuredCablingStudio.Binders.CalculationBinders
{
    public class StructuredCablingStudioParametersModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context?.Metadata?.ModelType == typeof(StructuredCablingStudioParameters))
            {
                return new BinderTypeModelBinder(typeof(StructuredCablingStudioParametersModelBinder));
            }
            return null;
        }
    }
}
