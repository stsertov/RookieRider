namespace RookieRider.ModelBinders
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using System.Globalization;
    using System.Threading.Tasks;

    public class DateTimeModelBinder : IModelBinder
    {
        private string customDateFormat;

        public DateTimeModelBinder(string customDateFormat)
        {
            this.customDateFormat = customDateFormat;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ValueProviderResult result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if(result != ValueProviderResult.None && !String.IsNullOrEmpty(result.FirstValue.ToString()))
            {
                DateTime dateValue = DateTime.MinValue;
                bool success = false;
                string value = result.FirstValue;

                try
                {
                    dateValue = DateTime.ParseExact(value, customDateFormat, CultureInfo.InvariantCulture);

                    success = true;
                }
                catch (Exception)
                {
                    try
                    {
                        dateValue = DateTime.Parse(value, new CultureInfo("bg-bg"));
                        success = true;
                    }
                    catch (Exception ex)
                    {
                        bindingContext.ModelState.AddModelError(bindingContext.ModelName, ex, bindingContext.ModelMetadata);
                    }
                }

                if (success)
                {
                    bindingContext.Result = ModelBindingResult.Success(dateValue);
                }
            }

            return Task.CompletedTask;
        }
    }
}
