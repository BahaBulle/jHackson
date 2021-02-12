// <copyright file="IProjectJson.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Projects
{
    using System.Collections.Generic;
    using JHackson.Core.Actions;
    using JHackson.Core.Variables;

    /// <summary>
    /// Provides an interface that represent a project.
    /// </summary>
    public interface IProjectJson
    {
        /// <summary>
        /// Gets the list of actions to execute.
        /// </summary>
        List<IActionJson> Actions { get; }

        /// <summary>
        /// Gets or sets the version of the application.
        /// </summary>
        string Application { get; set; }

        /// <summary>
        /// Gets or sets the console of the project.
        /// </summary>
        string Console { get; set; }

        /// <summary>
        /// Gets or sets the description of the project.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the game of the project.
        /// </summary>
        string Game { get; set; }

        /// <summary>
        /// Gets the list of plugins to load.
        /// </summary>
        List<string> Plugins { get; }

        /// <summary>
        /// Gets the list of tables to load.
        /// </summary>
        List<IActionTable> Tables { get; }

        /// <summary>
        /// Gets the list of variables to load.
        /// </summary>
        List<Variable> Variables { get; }

        /// <summary>
        /// Gets or sets the version of the project.
        /// </summary>
        string Version { get; set; }

        /// <summary>
        /// Check all possible errors.
        /// </summary>
        void Check();

        /// <summary>
        /// Execute all actions of the project.
        /// </summary>
        void Execute();

        /// <summary>
        /// Initialise the project.
        /// </summary>
        void Init();
    }
}