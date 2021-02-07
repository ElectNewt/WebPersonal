using System.Globalization;
using WebPersonal.Shared.Language;

namespace WebPersonal.BackEnd.Translations
{
    public class TraduccionErrores
    {
        private readonly CultureInfo _culture;
        public TraduccionErrores(CultureInfo culture)
        {
            _culture = culture;
        }

        public string PersonalProfile => LocalizationUtils<TraduccionErrores>.GetValue("PersonalProfileNotFound", _culture);
        public string IdentityNotFound => LocalizationUtils<TraduccionErrores>.GetValue("IdentityNotFound", _culture);
    }

}
