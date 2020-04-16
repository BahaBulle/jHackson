using jHackson.Core.Actions;
using jHackson.Core.Common;
using jHackson.Core.FileFormat;
using jHackson.Core.Json.ContractResolver;
using jHackson.Core.Json.JsonConverters;
using jHackson.Core.Projects;
using jHackson.Core.Services;
using jHackson.Core.TableElements;
using System;
using System.IO;
using System.Reflection;
using Unity;

namespace jHackson
{
    public class BootStrapper
    {
        private const string PLUGINS_DIRECTORY = "Plugins";

        private static IUnityContainer _container;

        public static void Init(IUnityContainer container)
        {
            _container = container;

            // Register IoC
            RegisterElements();

            // Load plugins
            LoadPlugins();
        }

        private static void LoadPlugins()
        {
            if (Directory.Exists(PLUGINS_DIRECTORY))
            {
                var filesList = Directory.GetFiles(PLUGINS_DIRECTORY, "*.dll");

                if (filesList.Length > 0)
                {
                    foreach (var fileName in filesList)
                    {
                        var assembly = Assembly.LoadFrom(fileName);

                        foreach (Type t in assembly.GetExportedTypes())
                        {
                            if (t.GetInterface("IActionJson", true) != null)
                            {
                                var action = (IActionJson)t.InvokeMember(null, BindingFlags.DeclaredOnly |
                                                                               BindingFlags.Public | BindingFlags.NonPublic |
                                                                               BindingFlags.Instance | BindingFlags.CreateInstance, null, null, null);

                                DataContext.AddMethod(action.Name, t);
                            }
                            else if (t.GetInterface("IFileFormat", true) != null)
                            {
                                var format = (IFileFormat)t.InvokeMember(null, BindingFlags.DeclaredOnly |
                                                                               BindingFlags.Public | BindingFlags.NonPublic |
                                                                               BindingFlags.Instance | BindingFlags.CreateInstance, null, null, null);

                                DataContext.AddFormat(format.Name, t);
                            }
                            else if (t.GetInterface("ITableElement", true) != null && !t.IsAbstract)
                            {
                                var element = (ITableElement)t.InvokeMember(null, BindingFlags.DeclaredOnly |
                                                                                  BindingFlags.Public | BindingFlags.NonPublic |
                                                                                  BindingFlags.Instance | BindingFlags.CreateInstance, null, null, null);

                                DataContext.AddTableElement(element.Name, t);
                            }
                        }
                    }
                }
            }
        }

        private static void RegisterElements()
        {
            _container.RegisterType<IProjectJson, ProjectJson>();
            _container.RegisterType<ISerializationService, SerializationService>();

            _container.RegisterType<ActionJsonConverter>();
            _container.RegisterType<VariableJsonConverter>();
            _container.RegisterType<Batch>();
            _container.RegisterType<UnityContractResolver>();
        }
    }
}