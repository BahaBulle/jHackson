// <copyright file="ISerializationService.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Services
{
    using JHackson.Core.Projects;

    public interface ISerializationService
    {
        IProjectJson Deserialize(string filename);

        void Serialize(IProjectJson project, string filename);
    }
}