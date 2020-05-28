// <copyright file="IActionVariable.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Actions
{
    public interface IActionVariable
    {
        string Name { get; }

        string Value { get; set; }
    }
}