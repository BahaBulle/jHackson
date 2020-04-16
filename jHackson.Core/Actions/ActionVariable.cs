﻿namespace jHackson.Core.Actions
{
    public class ActionVariable : IActionVariable
    {
        public ActionVariable()
        {
            this.Name = string.Empty;
            this.Value = string.Empty;
        }

        public string Name { get; set; }
        public string Value { get; set; }
    }
}