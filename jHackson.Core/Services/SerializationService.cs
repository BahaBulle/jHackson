﻿// <copyright file="SerializationService.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Services
{
    using System.IO;
    using JHackson.Core.Json.ContractResolver;
    using JHackson.Core.Projects;
    using Newtonsoft.Json;

    public class SerializationService : ISerializationService
    {
        private readonly UnityContractResolver contractResolver;

        public SerializationService(UnityContractResolver unityContractResolver)
        {
            this.contractResolver = unityContractResolver;
        }

        public IProjectJson Deserialize(string filename)
        {
            IProjectJson pj;

            using (var file = File.OpenText(filename))
            {
                var serializer = new JsonSerializer()
                {
                    ContractResolver = this.contractResolver,
                };

                pj = (IProjectJson)serializer.Deserialize(file, typeof(ProjectJson));
            }

            return pj;
        }

        public void Serialize(IProjectJson project, string filename)
        {
            using (var file = File.CreateText(filename))
            {
                var serializer = new JsonSerializer
                {
                    Formatting = Formatting.Indented,
                };

                serializer.Serialize(file, project);
            }
        }
    }
}