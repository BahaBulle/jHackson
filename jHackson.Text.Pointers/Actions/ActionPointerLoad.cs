// <copyright file="ActionPointerLoad.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Text.Pointers.Actions
{
    using System;
    using System.Globalization;
    using System.IO;
    using JHackson.Core;
    using JHackson.Core.Actions;
    using JHackson.Core.Common;
    using JHackson.Core.Localization;
    using JHackson.Core.PointersTable;
    using JHackson.Core.Projects;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using NLog;

    /// <summary>
    /// Provides a class that allows loading a pointers table.
    /// </summary>
    public class ActionPointerLoad : ActionBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionPointerLoad"/> class.
        /// </summary>
        public ActionPointerLoad()
            : base()
        {
            this.Name = "PointerLoad";
            this.Title = null;
            this.Todo = true;
        }

        /// <summary>
        /// Gets or sets the adress of pointers.
        /// </summary>
        public Adress Adress { get; set; }

        /// <summary>
        /// Gets or sets the calcul to calculate pointers.
        /// </summary>
        public string Calcul { get; set; }

        /// <summary>
        /// Gets or sets the endian type of the pointers.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public EnumEndianType Endian { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the memorystream where read pointers.
        /// </summary>
        public int? From { get; set; }

        /// <summary>
        /// Gets or sets the insertion mode/
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public EnumPointersTableMode Mode { get; set; }

        /// <summary>
        /// Gets or sets the number of pointers.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the size in bytes of a pointer.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the pointer table.
        /// </summary>
        public int? To { get; set; }

        /// <summary>
        /// Check errors in parameters.
        /// </summary>
        public override void Check()
        {
            if (!this.From.HasValue)
            {
                this.AddError(LocalizationManager.GetMessage("core.parameterNotFound", nameof(this.From), this.From.HasValue ? this.From.Value.ToString(CultureInfo.InvariantCulture) : "null"));
            }

            if (!this.To.HasValue)
            {
                this.AddError(LocalizationManager.GetMessage("core.parameterNotFound", nameof(this.To), this.To.HasValue ? this.To.Value.ToString(CultureInfo.InvariantCulture) : "null"));
            }

            if (this.Size != 2 && this.Size != 3 && this.Size != 4 && this.Size != 8)
            {
                this.AddError(LocalizationManager.GetMessage("core.parameter"));
            }
        }

        /// <summary>
        /// Execute the process of this action.
        /// </summary>
        public override void Execute()
        {
            if (this.Title != null)
            {
                Logger.Info(this.Title);
            }

            var ms = this.Context.Buffers.Get<MemoryStream>(this.From.Value);
            ms.Position = this.Adress.Value;

            IPointersTable pointersTable;
            if (this.Mode == EnumPointersTableMode.New)
            {
                pointersTable = new PointersTable();
                this.Context.PointersTables.Add(this.To.Value, pointersTable);
            }
            else
            {
                pointersTable = this.Context.PointersTables.Get(this.To.Value);
            }

            using (var binaryReader = new BinaryReaderCore(ms))
            {
                // TODO - Read pointers
                for (int i = 0; i < this.Quantity; i++)
                {
                    switch (this.Size)
                    {
                        case 2:
                            short value16 = binaryReader.ReadInt16(this.Endian);
                            break;

                        case 3:
                            int value24 = binaryReader.ReadInt24(this.Endian);
                            break;

                        case 4:
                            int value32 = binaryReader.ReadInt32(this.Endian);
                            break;

                        case 8:
                            long value64 = binaryReader.ReadInt64(this.Endian);
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Initialize this action.
        /// </summary>
        /// <param name="context">Context of the project.</param>
        public override void Init(IProjectContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            base.Init(context);
        }
    }
}