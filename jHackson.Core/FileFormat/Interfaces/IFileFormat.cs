// <copyright file="IFileFormat.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.FileFormat
{
    public interface IFileFormat
    {
        string Name { get; }

        void Save(string filename, object buffer);
    }
}