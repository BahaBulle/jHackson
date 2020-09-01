// <copyright file="ActionBase.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Core.Actions
{
    using System.Collections.Generic;
    using JHackson.Core.Common;
    using JHackson.Core.Projects;
    using Newtonsoft.Json;

    public abstract class ActionBase : IActionJson
    {
        private readonly List<string> errors;

        protected ActionBase()
        {
            this.errors = new List<string>();
        }

        [JsonIgnore]
        public bool HasErrors => this.errors.Count > 0;

        [JsonIgnore]
        public string Name { get; protected set; }

        public string Title { get; set; }

        public bool? Todo { get; set; }

        protected IProjectContext Context { get; set; }

        public virtual void AddError(string message)
        {
            this.errors.Add($"{this.Name} - {message}");
        }

        public virtual void AddErrors(List<string> errors)
        {
            this.errors.AddRange(errors);
        }

        /// <summary>
        /// Check if mandatories properties have values.
        /// </summary>
        public abstract void Check();

        public abstract void Execute();

        public virtual List<string> GetErrors()
        {
            return this.errors;
        }

        public virtual void Init(IProjectContext context)
        {
            this.Context = context;

            this.Title = PluginsHelper.ReplaceParameters(this, this.Title);
        }
    }
}