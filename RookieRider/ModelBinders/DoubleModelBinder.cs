namespace RookieRider.ModelBinders
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using System.Globalization;
    using System.Threading.Tasks;

    public class DoubleModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ValueProviderResult result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (result != ValueProviderResult.None && !String.IsNullOrEmpty(result.FirstValue.ToString()))
            {
                double doubleValue = 0;
                bool success = false;

                try
                {
                    string value = result.FirstValue;

                    value = value.Replace(".", CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator);
                    value = value.Replace(",", CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator);

                    doubleValue = Convert.ToDouble(value, CultureInfo.CurrentCulture);

                    success = true;
                }
                catch (Exception e)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, e, bindingContext.ModelMetadata);
                }

                if(success)
                {
                    bindingContext.Result = ModelBindingResult.Success(doubleValue);
                }
            }

            return Task.CompletedTask;
        }
    }
}
