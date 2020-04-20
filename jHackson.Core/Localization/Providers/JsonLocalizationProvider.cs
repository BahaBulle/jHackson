using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace jHackson.Core.Localization.Providers
{
    public class JsonLocalizationProvider : ILocalizationResourceProvider
    {
        private const string DIRECTORY_FILES = "Localization";
        private readonly Dictionary<string, List<LocalizationLanguageResources>> _files = new Dictionary<string, List<LocalizationLanguageResources>>();
        private readonly Regex _regexLanguage = new Regex(@"([^_]+)_([a-zA-Z]{2,3}-[a-zA-Z]+)\.json");
        private Dictionary<string, string> _messages = new Dictionary<string, string>();

        public JsonLocalizationProvider()
        {
        }

        public List<CultureInfo> CulturesList { get; private set; }

        public object GetValue(string cle)
        {
            if (this._messages != null)
            {
                try
                {
                    this._messages.TryGetValue(cle, out string message);

                    return message;
                }
                catch
                {
                    return null;
                }
            }

            return null;
        }

        public void Init()
        {
            this.CulturesList = new List<CultureInfo>();

            string[] filesList = null;

            if (Directory.Exists(DIRECTORY_FILES))
                filesList = Directory.GetFiles(DIRECTORY_FILES, "*.json");

            if (filesList != null)
            {
                foreach (var fichier in filesList)
                {
                    LocalizationFilesResources mr = this.SplitFileName(fichier);

                    if (mr == null)
                        continue;

                    var culture = new CultureInfo(mr.Language);

                    if (!this.CulturesList.Contains(culture))
                        this.CulturesList.Add(culture);

                    JToken json = this.GetFileContent(fichier);

                    if (!this._files.ContainsKey(mr.Name))
                        this._files.Add(mr.Name, new List<LocalizationLanguageResources>());

                    this._files[mr.Name].Add(new LocalizationLanguageResources() { Culture = culture, Value = json });
                }
            }
        }

        public void Load()
        {
            this._messages = this.GetDictionary(CultureInfo.CurrentUICulture);
        }

        public void Unload()
        {
            this._messages = new Dictionary<string, string>();
        }

        private void GenerateDictionary(Dictionary<string, string> dict, JToken token, string prefix)
        {
            switch (token.Type)
            {
                case JTokenType.Object:
                    foreach (JProperty prop in token.Children<JProperty>())
                    {
                        this.GenerateDictionary(dict, prop.Value, this.Join(prefix, prop.Name));
                    }
                    break;

                case JTokenType.Array:
                    int index = 0;
                    foreach (JToken value in token.Children())
                    {
                        this.GenerateDictionary(dict, value, this.Join(prefix, index.ToString()));
                        index++;
                    }
                    break;

                default:
                    if (!dict.ContainsKey(prefix))
                        dict.Add(prefix, ((JValue)token).Value.ToString());
                    break;
            }
        }

        private Dictionary<string, string> GetDictionary(CultureInfo culture)
        {
            var dic = new Dictionary<string, string>();

            foreach (KeyValuePair<string, List<LocalizationLanguageResources>> file in this._files)
            {
                LocalizationLanguageResources ml = file.Value
                    .Single(x => x.Culture.Equals(culture));

                if (ml == null)
                    continue;

                this.GenerateDictionary(dic, ml.Value, null);
            }

            return dic;
        }

        [SuppressMessage("Style", "IDE0063:Utiliser une instruction 'using' simple", Justification = "I don't like the thing")]
        private JToken GetFileContent(string fichier)
        {
            JToken token = null;
            StreamReader file = null;

            try
            {
                file = File.OpenText(fichier);

                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    file = null;
                    token = JToken.ReadFrom(reader);
                }
            }
            finally
            {
                if (file != null)
                    file.Dispose();
            }

            return token;
        }

        private string Join(string prefix, string name)
        {
            return (string.IsNullOrEmpty(prefix) ? name : prefix + "." + name);
        }

        private LocalizationFilesResources SplitFileName(string fichier)
        {
            var fileName = Path.GetFileName(fichier);

            var match = this._regexLanguage.Match(fileName);

            if (!match.Success && match.Groups.Count != 3)
                return null;

            var res = new LocalizationFilesResources
            {
                Name = match.Groups[1].Value,
                Language = match.Groups[2].Value
            };

            return res;
        }
    }
}