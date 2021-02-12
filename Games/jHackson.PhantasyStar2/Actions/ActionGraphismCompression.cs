// <copyright file="ActionGraphismCompression.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.PhantasyStar2.Actions
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using JHackson.Core.Actions;
    using JHackson.Core.Extensions;
    using JHackson.Core.Localization;
    using NLog;

    /// <summary>
    /// Provides a action which allows to compress data use in Phantasy Star 2 on Genesis.
    /// </summary>
    public class ActionGraphismCompression : ActionBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionGraphismCompression"/> class.
        /// </summary>
        public ActionGraphismCompression()
        {
            this.Name = "GEN-PS2-C";
            this.Title = null;
            this.Todo = true;

            this.From = null;
            this.To = null;
        }

        /// <summary>
        /// Gets or sets the id of the MemoryStream to read.
        /// </summary>
        public int? From { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the memorystream where load the file.
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

            var msSource = this.Context.Buffers.Get<MemoryStream>(this.From.Value);
            msSource.Position = 0;

            var msDestination = Compress(msSource);

            this.Context.Buffers.Add(this.To.Value, msDestination);
        }

        private static MemoryStream Compress(MemoryStream source)
        {
            var destination = new MemoryStream();

            using (var sourceReader = new BinaryReader(source, Encoding.UTF8, true))
            using (var destinationWriter = new BinaryWriter(destination, Encoding.UTF8, true))
            {
                var size = source.Length;
                var numberOfTiles = size / 32;

                for (int i = 0; i < numberOfTiles; i++)
                {
                    var bytes = sourceReader.ReadBytes(32);

                    var regroup = bytes
                        .GroupBy(x => x)
                        .Select(x => new { Byte = x.Key, Count = x.Count() })
                        .Where(x => x.Count >= 6);

                    byte repetition = 0;
                    var buffer = new List<byte>();
                    uint header = 0;
                    uint headerTotal = 0;
                    foreach (var group in regroup)
                    {
                        header = 0;
                        repetition++;
                        buffer.Add(group.Byte);
                        for (int j = 0; j < 32; j++)
                        {
                            if (bytes[j] == group.Byte)
                            {
                                header = header.SetBit(32 - j - 1);
                            }
                        }

                        headerTotal |= header;

                        var headerInBytes = BitConverter.GetBytes(header);
                        buffer.Add(headerInBytes[3]);
                        buffer.Add(headerInBytes[2]);
                        buffer.Add(headerInBytes[1]);
                        buffer.Add(headerInBytes[0]);
                    }

                    for (int j = 0; j < 32; j++)
                    {
                        if (!headerTotal.IsBitSet(32 - j - 1))
                        {
                            buffer.Add(bytes[j]);
                        }
                    }

                    destinationWriter.Write(repetition);
                    destinationWriter.Write(buffer.ToArray());
                }

                destinationWriter.Write((byte)0xFF);
            }

            return destination;
        }
    }
}