// <copyright file="PointerParameters.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Text.Pointers.Common
{
    using System.Collections.Generic;
    using JHackson.Core;
    using JHackson.Core.Common;
    using JHackson.Core.Localization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Provides a class that allows to set parameters for the pointer table.
    /// </summary>
    public class PointerParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PointerParameters" /> class.
        /// </summary>
        public PointerParameters()
        {
        }

        /// <summary>
        /// Gets or sets the adress of the pointers.
        /// </summary>
        public Adress Adress { get; set; }

        /// <summary>
        /// Gets or sets the formula to calculate the value of a pointer.
        /// </summary>
        public string Calcul { get; set; }

        /// <summary>
        /// Gets or sets the endian type of a pointers.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public EnumEndianType Endian { get; set; }

        /// <summary>
        /// Gets or sets the number of pointers.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the Size of a pointer in bytes.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Check if there is errors.
        /// </summary>
        /// <returns>List of errors message.</returns>
        public List<string> Check(string parent)
        {
            var errors = new List<string>();

            // TODO - Check errors
            // Test Calcul
            if (this.Adress == null)
            {
                errors.Add(LocalizationManager.GetMessage("text.pointers.incorrectAdressFormat", parent, nameof(this.Adress), this.Adress));
            }

            return errors;
        }
    }
}