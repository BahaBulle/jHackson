// <copyright file="IVariable.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Variables
{
    /// <summary>
    /// Provides a interface that allows to declare a variable.
    /// </summary>
    public interface IVariable
    {
        /// <summary>
        /// Gets or sets the name of the variable.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the value of the variable.
        /// </summary>
        string Value { get; set; }
    }
}