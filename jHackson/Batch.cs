using jHackson.Core.Exceptions;
using jHackson.Core.Services;
using NLog;
using System.Collections.Generic;
using System.IO;

namespace jHackson
{
    public class Batch
    {
        #region Dependencies

        private readonly ISerializationService _serializationService;

        #endregion

        #region Fields

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Constructors

        public Batch(ISerializationService serializationService)
        {
            this._serializationService = serializationService;
        }

        #endregion

        #region Publics methods

        public void Run(List<string> arguments)
        {
            if (arguments == null || arguments.Count < 1)
            {
                _logger.Error("Not enough parameter!");
                Usage();

                return;
            }

            if (!File.Exists(arguments[0]))
            {
                _logger.Error($"File doesn't exists : {arguments[0]}");

                return;
            }

            try
            {
                var pj = this._serializationService.Deserialize(arguments[0]);

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

        #endregion

        #region Privates methods

        private static void Usage()
        {
            _logger.Info("Usage :");
            _logger.Info("  jHackson.exe <json_file>");
        }

        #endregion
    }
}