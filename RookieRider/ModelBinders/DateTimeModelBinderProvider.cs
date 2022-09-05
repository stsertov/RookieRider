namespace RookieRider.ModelBinders
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class DateTimeModelBinderProvider : IModelBinderProvider
    {
        private readonly string dateTimeFormat;
        public DateTimeModelBinderProvider(string dateTimeFormat)
        {
            this.dateTimeFormat = dateTimeFormat;
        }
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if(context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if(context.Metadata.ModelType == typeof(DateTime) || context.Metadata.ModelType == typeof(DateTime?))
            {
                return new DateTimeModelBinder(dateTimeFormat);
            }

            return null;
        }
    }
}
