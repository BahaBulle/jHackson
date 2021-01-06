﻿// <copyright file="ActionBinInsert.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Actions.Binary
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using JHackson.Actions.Binary.Common;
    using JHackson.Core.Actions;
    using JHackson.Core.Localization;
    using JHackson.Core.Projects;
    using NLog;

    /// <summary>
    /// Provides a class that allows insert data in a file in binary format.
    /// </summary>
    public class ActionBinInsert : ActionBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionBinInsert" /> class.
        /// </summary>
        public ActionBinInsert()
        {
            this.Name = "BinInsert";
            this.Title = null;
            this.Todo = true;

            this.To = null;

            this.DataParameters = new List<DataParameters>();
        }

        /// <summary>
        /// Gets parameters of data to write.
        /// </summary>
        public List<DataParameters> DataParameters { get; private set; }

        /// <summary>
        /// Gets or sets the identifier of the memorystream where write data.
        /// </summary>
        public int? To { get; set; }

        /// <summary>
        /// Check errors in parameters.
        /// </summary>
        public override void Check()
        {
            if (!this.To.HasValue)
            {
                this.AddError(LocalizationManager.GetMessage("core.parameterNotFound", nameof(this.To), this.To.HasValue ? this.To.Value.ToString(CultureInfo.InvariantCulture) : "null"));
            }

            foreach (var data in this.DataParameters)
            {
                if (!data.Check())
                {
                    this.AddError(LocalizationManager.GetMessage("actions.incorrectFormat", nameof(this.DataParameters), data.Value, data.Type));
                }
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

            var msSource = this.Context.GetBufferMemoryStream(this.To.Value, true);
            var msDest = new MemoryStream();
            msSource.Position = 0;

            byte[] bytes;
            using (var binaryReader = new BinaryReader(msSource))
            using (var binaryWriter = new BinaryWriter(msDest, Encoding.Default, true))
            {
                long position = 0;

                foreach (var data in this.DataParameters.OrderBy(x => x.Adress))
                {
                    bytes = binaryReader.ReadBytes((int)data.Adress - (int)position);
                    position = data.Adress;

                    binaryWriter.Write(bytes, 0, (int)data.Adress);

                    switch (data.Type)
                    {
                        case EnumDataType.U8:
                            byte byteValue = Convert.ToByte(data.Value, CultureInfo.CurrentCulture);
                            binaryWriter.Write(byteValue);
                            break;

                        case EnumDataType.U16:
                            ushort ushortValue = Convert.ToUInt16(data.Value, CultureInfo.CurrentCulture);
                            foreach (byte b in BinHelper.GetBytes(ushortValue, data.Endian))
                            {
                                binaryWriter.Write(b);
                            }

                            break;

                        case EnumDataType.U32:
                            uint uintValue = Convert.ToUInt32(data.Value, CultureInfo.CurrentCulture);
                            foreach (byte b in BinHelper.GetBytes(uintValue, data.Endian))
                            {
                                binaryWriter.Write(b);
                            }

                            break;

                        case EnumDataType.U64:
                            ulong ulongValue = Convert.ToUInt64(data.Value, CultureInfo.CurrentCulture);
                            foreach (byte b in BinHelper.GetBytes(ulongValue, data.Endian))
                            {
                                binaryWriter.Write(b);
                            }

                            break;

                        case EnumDataType.Str:
                            binaryWriter.Write(((string)data.Value).ToCharArray());
                            break;
                    }
                }

                int size = (int)binaryReader.BaseStream.Length - (int)position;
                bytes = binaryReader.ReadBytes(size);

                binaryWriter.Write(bytes, 0, size);

                this.Context.AddBuffer(this.To.Value, msDest);
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

        private int GetNumberOfBytesToInsert()
        {
            int numberOfBytes = 0;

            foreach (var data in this.DataParameters)
            {
                switch (data.Type)
                {
                    case EnumDataType.U8:
                        numberOfBytes += 1;
                        break;

                    case EnumDataType.U16:
                        numberOfBytes += 2;
                        break;

                    case EnumDataType.U32:
                        numberOfBytes += 4;
                        break;

                    case EnumDataType.U64:
                        numberOfBytes += 8;
                        break;

                    case EnumDataType.Str:
                        numberOfBytes += ((string)data.Value).Length;
                        break;
                }
            }

            return numberOfBytes;
        }
    }
}