// <copyright file="ActionGraphismDecompression.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.PhantasyStar2.Actions
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using JHackson.Core.Actions;
    using JHackson.Core.Extensions;
    using JHackson.Core.Localization;
    using NLog;

    /// <summary>
    /// Provides a action which allows to decompress data use in Phantasy Star 2 on Genesis.
    /// </summary>
    public class ActionGraphismDecompression : ActionBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionGraphismDecompression"/> class.
        /// </summary>
        public ActionGraphismDecompression()
        {
            this.Name = "GEN-PS2-D";
            this.Title = null;
            this.Todo = true;

            this.AdressStart = 0;
            this.From = null;
            this.To = null;
        }

        /// <summary>
        /// Gets or sets the adress start of data.
        /// </summary>
        public long? AdressStart { get; set; }

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
            msSource.Position = this.AdressStart.Value;

            var msDestination = Decompress(msSource);

            this.Context.Buffers.Add(this.To.Value, msDestination);
        }

        private static uint BytesToInt(byte[] bytes)
        {
            Array.Reverse(bytes);

            return BitConverter.ToUInt32(bytes, 0);
        }

        private static MemoryStream Decompress(MemoryStream source)
        {
            var destination = new MemoryStream();

            using (var sourceReader = new BinaryReader(source, Encoding.UTF8, true))
            using (var destinationWriter = new BinaryWriter(destination, Encoding.UTF8, true))
            {
                while (true)
                {
                    byte repetition = sourceReader.ReadByte();

                    if (repetition == 0xFF)
                    {
                        break;
                    }

                    uint total = 0;
                    byte[] buffer = new byte[32];

                    for (int i = 0; i < repetition; i++)
                    {
                        byte byteToWrite = sourceReader.ReadByte();

                        uint header = BytesToInt(sourceReader.ReadBytes(4));

                        total |= header;

                        int bitPosition = 32;
                        for (int j = 0; j < 32; j++)
                        {
                            if (header.IsBitSet(bitPosition - 1))
                            {
                                buffer[j] = byteToWrite;
                            }

                            bitPosition--;
                        }
                    }

                    if ((int)(total ^ 0xFFFFFFFF) != 0)
                    {
                        int bitPosition = 32;
                        for (int j = 0; j < 32; j++)
                        {
                            if (!total.IsBitSet(bitPosition - 1))
                            {
                                buffer[j] = sourceReader.ReadByte();
                            }

                            bitPosition--;
                        }
                    }

                    destinationWriter.Write(buffer, 0, 32);
                }
            }

            return destination;
        }
    }
}