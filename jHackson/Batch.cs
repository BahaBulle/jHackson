// <copyright file="Batch.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson
{
    using System.Collections.Generic;
    using System.IO;
    using JHackson.Core.Exceptions;
    using JHackson.Core.Localization;
    using JHackson.Core.Services;
    using NLog;

    /// <summary>
    /// Provides a class which allows to run the process.
    /// </summary>
    public class Batch
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ISerializationService serializationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="Batch"/> class.
        /// </summary>
        public Batch(ISerializationService serializationService)
        {
            this.serializationService = serializationService;
        }

        /// <summary>
        /// Run the process on the file given in arguments.
        /// </summary>
        /// <param name="arguments">Arguments of command line.</param>
        public void Run(List<string> arguments)
        {
            if (arguments == null || arguments.Count < 1)
            {
                Logger.Error(LocalizationManager.GetMessage("batch.insufficientParameters"));
                Usage();

                return;
            }

            if (!File.Exists(arguments[0]))
            {
                Logger.Error(LocalizationManager.GetMessage("batch.fileUnknow", arguments[0]));

                return;
            }

            try
            {
                var pj = this.serializationService.Deserialize(arguments[0]);

                pj.Init();

                pj.Check();

                pj.Execute();
            }
            catch (JHacksonException)
            {
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

        private static void Usage()
        {
            Logger.Info(LocalizationManager.GetMessage("batch.usage"));
            Logger.Info("  jHackson.exe <json_file>");
        }
    }
}