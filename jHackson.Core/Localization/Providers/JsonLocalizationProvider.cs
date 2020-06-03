// <copyright file="JsonLocalizationProvider.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Localization.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class JsonLocalizationProvider : ILocalizationResourceProvider
    {
        private const string DIRECTORYFILES = "Localization";
        private readonly Dictionary<string, List<LocalizationLanguageResources>> files = new Dictionary<string, List<LocalizationLanguageResources>>();
        private readonly Regex regexLanguage = new Regex(@"([^_]+)_([a-zA-Z]{2,3}-[a-zA-Z]+)\.json");
        private Dictionary<string, string> messages = new Dictionary<string, string>();

        public JsonLocalizationProvider()
        {
        }

        public List<CultureInfo> CulturesList { get; private set; }

        public object GetValue(string cle)
        {
            if (this.messages != null)
            {
                try
                {
                    this.messages.TryGetValue(cle, out string message);

                    return message;
                }
                catch (ArgumentNullException)
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

            if (Directory.Exists(DIRECTORYFILES))
            {
                filesList = Directory.GetFiles(DIRECTORYFILES, "*.json");
            }

            if (filesList != null)
            {
                foreach (var fichier in filesList)
                {
                    var mr = this.SplitFileName(fichier);

                    if (mr == null)
                    {
                        continue;
                    }

                    var culture = new CultureInfo(mr.Language);

                    if (!this.CulturesList.Contains(culture))
                    {
                        this.CulturesList.Add(culture);
                    }

                    var json = GetFileContent(fichier);

                    if (!this.files.ContainsKey(mr.Name))
                    {
                        this.files.Add(mr.Name, new List<LocalizationLanguageResources>());
                    }

                    this.files[mr.Name].Add(new LocalizationLanguageResources() { Culture = culture, Value = json });
                }
            }
        }

        public void Load()
        {
            this.messages = this.GetDictionary(CultureInfo.CurrentUICulture);
        }

        public void Unload()
        {
            this.messages = new Dictionary<string, string>();
        }

        private static JToken GetFileContent(string fichier)
        {
            JToken token = null;
            StreamReader file = null;

            try
            {
                file = File.OpenText(fichier);

                using (var reader = new JsonTextReader(file))
                {
                    file = null;
                    token = JToken.ReadFrom(reader);
                }
            }
            finally
            {
                if (file != null)
                {
                    file.Dispose();
                }
            }

            return token;
        }

        private static string Join(string prefix, string name)
        {
            return string.IsNullOrEmpty(prefix) ? name : prefix + "." + name;
        }

        private void GenerateDictionary(Dictionary<string, string> dict, JToken token, string prefix)
        {
            switch (token.Type)
            {
                case JTokenType.Object:
                    foreach (var prop in token.Children<JProperty>())
                    {
                        this.GenerateDictionary(dict, prop.Value, Join(prefix, prop.Name));
                    }

                    break;

                case JTokenType.Array:
                    int index = 0;
                    foreach (var value in token.Children())
                    {
                        this.GenerateDictionary(dict, value, Join(prefix, index.ToString(CultureInfo.CurrentCulture)));
                        index++;
                    }

                    break;

                default:
                    if (!dict.ContainsKey(prefix))
                    {
                        dict.Add(prefix, ((JValue)token).Value.ToString());
                    }

                    break;
            }
        }

        private Dictionary<string, string> GetDictionary(CultureInfo culture)
        {
            var dic = new Dictionary<string, string>();

            foreach (var file in this.files)
            {
                var ml = file.Value.Single(x => x.Culture.Equals(culture));

                if (ml == null)
                {
                    continue;
                }

                this.GenerateDictionary(dic, ml.Value, null);
            }

            return dic;
        }

        private LocalizationFilesResources SplitFileName(string fichier)
        {
            var fileName = Path.GetFileName(fichier);

            var match = this.regexLanguage.Match(fileName);

            if (!match.Success && match.Groups.Count != 3)
            {
                return null;
            }

            var res = new LocalizationFilesResources
            {
                Name = match.Groups[1].Value,
                Language = match.Groups[2].Value,
            };

            return res;
        }
    }
}