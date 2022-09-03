namespace RookieRider.ModelBinders
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using System.Globalization;
    using System.Threading.Tasks;

    public class DecimalModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ValueProviderResult result = bindingContext
                .ValueProvider
                .GetValue(bindingContext.ModelName);

            if (result != ValueProviderResult.None && !String.IsNullOrEmpty(result.FirstValue.ToString()))
            {
                decimal decimalValue = 0M;
                bool success = false;

                try
                {
                    string value = result.FirstValue;

                    value = value.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    value = value.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

                    decimalValue = Convert.ToDecimal(value, CultureInfo.CurrentCulture);

                    success = true;
                }
                catch (Exception e)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, e, bindingContext.ModelMetadata);
                }

                if(success)
                {
                   bindingContext.Result = ModelBindingResult.Success(decimalValue);
                }
            }

            return Task.CompletedTask;
        }
    }
}
