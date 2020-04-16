using jHackson.Core.Exceptions;
using jHackson.Core.Projects;
using jHackson.Core.Services;
using NLog;
using System.Collections.Generic;
using System.IO;

namespace jHackson.UI
{
    public class MainBatch : IBatch
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly ISerializationService _serializationService;

        public MainBatch(ISerializationService serializationService)
        {
            this._serializationService = serializationService;
        }

        public void Start(List<string> parameters)
        {
            if (parameters == null || parameters.Count < 2)
            {
                _logger.Error("Not enough parameter!");
                Usage();

                return;
            }

            if (!File.Exists(parameters[1]))
            {
                _logger.Error("File doesn't exists : " + parameters[1]);

                return;
            }

            try
            {
                IProjectJson pj = this._serializationService.Deserialize(parameters[1]);

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
            _logger.Info("Usage :");
            _logger.Info("  jHackson.exe <json_file>");
        }
    }
}