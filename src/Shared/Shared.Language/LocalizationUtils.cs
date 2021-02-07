using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace WebPersonal.Shared.Language
{
    public class LocalizationUtils<TEntity>
    {

        private static readonly IStringLocalizer _localizer;

        static LocalizationUtils()
        {
            var options = Options.Create(new LocalizationOptions());
            var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            var type = typeof(TEntity);

            _localizer = factory.Create(type);
        }


        public static string GetValue(string field)
        {
            return _localizer[field];
        }

        public static string GetValue(string field, CultureInfo cultureinfo)
        {
            using (new CultureScope(cultureinfo))
            {
                return GetValue(field);
            }
        }
    }
}
