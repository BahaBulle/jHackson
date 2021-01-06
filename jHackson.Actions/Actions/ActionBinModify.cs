// <copyright file="ActionBinModify.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Actions.Binary
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using JHackson.Actions.Binary.Common;
    using JHackson.Core.Actions;
    using JHackson.Core.Localization;
    using JHackson.Core.Projects;
    using Newtonsoft.Json.Linq;
    using NLog;

    /// <summary>
    /// Provides a class that allows write data in a file in binary format.
    /// </summary>
    public class ActionBinModify : ActionBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionBinModify" /> class.
        /// </summary>
        public ActionBinModify()
        {
            this.Name = "BinModify";
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

            var msDest = this.Context.GetBufferMemoryStream(this.To.Value, true);

            using (var binaryWriter = new BinaryWriter(msDest, Encoding.Default, true))
            {
                foreach (var data in this.DataParameters)
                {
                    binaryWriter.BaseStream.Position = data.Adress;

                    switch (data.Type)
                    {
                        case EnumDataType.Array8:
                            byte[] bytesValue = ((JArray)data.Value).ToObject<byte[]>();
                            binaryWriter.Write(bytesValue);
                            break;

                        case EnumDataType.Array16:
                            ushort[] shortArray = ((JArray)data.Value).ToObject<ushort[]>();
                            foreach (ushort value in shortArray)
                            {
                                foreach (byte b in BinHelper.GetBytes(value, data.Endian))
                                {
                                    binaryWriter.Write(b);
                                }
                            }

                            break;

                        case EnumDataType.Array32:
                            uint[] uintArray = ((JArray)data.Value).ToObject<uint[]>();
                            foreach (uint value in uintArray)
                            {
                                foreach (byte b in BinHelper.GetBytes(value, data.Endian))
                                {
                                    binaryWriter.Write(b);
                                }
                            }

                            break;

                        case EnumDataType.Array64:
                            ulong[] ulongArray = ((JArray)data.Value).ToObject<ulong[]>();
                            foreach (ulong value in ulongArray)
                            {
                                foreach (byte b in BinHelper.GetBytes(value, data.Endian))
                                {
                                    binaryWriter.Write(b);
                                }
                            }

                            break;

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