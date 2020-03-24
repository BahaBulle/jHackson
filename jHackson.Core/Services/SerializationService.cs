﻿using jHackson.Core.Json.ContractResolver;
using jHackson.Core.Projects;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace jHackson.Core.Services
{
    public class SerializationService : ISerializationService
    {
        #region Dépendencies

        private readonly UnityContractResolver _contractResolver;

        #endregion

        #region Constructors

        public SerializationService(UnityContractResolver unityContractResolver)
        {
            this._contractResolver = unityContractResolver;
        }

        #endregion

        #region Methods

        public IProjectJson Deserialize(string filename)
        {
            IProjectJson pj;

            using (StreamReader file = File.OpenText(filename))
            {
                JsonSerializer serializer = new JsonSerializer()
                {
                    ContractResolver = this._contractResolver
                };

                pj = (IProjectJson)serializer.Deserialize(file, typeof(IProjectJson));
            }

            return pj;
        }

        [SuppressMessage("Style", "IDE0063:Utiliser une instruction 'using' simple", Justification = "Je n'aime pas le 'using' simple")]
        public void Serialize(IProjectJson project, string filename)
        {
            using (StreamWriter file = File.CreateText(filename))
            {
                JsonSerializer serializer = new JsonSerializer
                {
                    Formatting = Formatting.Indented,
                };

                serializer.Serialize(file, project);
            }
        }

        #endregion
    }
}