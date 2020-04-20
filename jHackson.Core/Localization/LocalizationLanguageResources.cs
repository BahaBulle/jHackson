using Newtonsoft.Json.Linq;
using System.Globalization;

namespace jHackson.Core.Localization
{
    public class LocalizationLanguageResources
    {
        public CultureInfo Culture { get; set; }
        public JToken Value { get; set; }
    }
}