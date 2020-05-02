namespace IocServiceStack.Config
{
    using System;
    using System.IO;
    using Newtonsoft.Json;
    using IocServiceStack;

    public static class ConfigExtension
    {
        public static void ConfigFromFile(this ContainerConfig containerConfig)
        {
            var appDependencies = DeserializeConfig();
            ConfigFromObject(containerConfig, appDependencies);
        }

        public static void ConfigFromObject(this ContainerConfig containerConfig, AppDependenciesObject appDependecies)
        {
            containerConfig.AddServices(service =>
            {
                ConfigFromObjectInternal(service, appDependecies);
                service.StrictMode = appDependecies.StrictMode;
            });
        }

        public static void ConfigFromFile(this ContainerOptions containerOptions)
        {
            var appDependencies = DeserializeConfig();
            ConfigFromObjectInternal(containerOptions, appDependencies);
        }
       
        public static void ConfigAddition(this IocContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            var dependencies = DeserializeConfig();

            if (dependencies.Addition != null)
            {
                foreach (var item in dependencies.Addition)
                {
                    AdditionProcess.Process(item, container);
                }
            }
        }

        internal static void ConfigAddition(this IocContainer container, AppDependenciesObject appDependencies)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            if (appDependencies == null)
            {
                throw new ArgumentNullException(nameof(appDependencies));
            }

            if (appDependencies.Addition != null)
            {
                foreach (var item in appDependencies.Addition)
                {
                    AdditionProcess.Process(item, container);
                }
            }
        }

        internal static AppDependenciesObject DeserializeConfig(string file = "appdependencies.json")
        {
            var fileContent = File.ReadAllText("appdependencies.json");
            return JsonConvert.DeserializeObject<AppDependenciesObject>(fileContent);
        }


        #region Private Static Methods
        private static void AddDependeciesRecursively(ContainerDependencyOptions containerOptions, ModuleDependencies dependencies)
        {
            if (dependencies == null)
            {
                return;
            }
            containerOptions.AddDependencies(layer =>
            {
                layer.Name = dependencies.Name;

                var modules = dependencies.Modules;
                layer.Assemblies = modules == null || modules.Length == 0 ? null : modules;

                AddDependeciesRecursively(layer, dependencies.Dependencies);
            });
        }

        private static void AddDependeciesRecursively(ContainerOptions containerOptions, ModuleDependencies dependencies)
        {
            if (dependencies == null)
            {
                return;
            }
            containerOptions.AddDependencies(layer =>
            {
                layer.Name = dependencies.Name;

                var modules = dependencies.Modules;
                layer.Assemblies = modules == null || modules.Length == 0 ? null : modules;

                AddDependeciesRecursively(layer, dependencies.Dependencies);
            });
        }

        private static void ConfigFromObjectInternal(ContainerOptions containerOptions, AppDependenciesObject dependencies)
        {
            // Add Root Assemblies
            var modules = dependencies.AppDependencies.Modules;
            containerOptions.Assemblies = modules == null || modules.Length == 0 ? null : modules;

            if (containerOptions.Assemblies != null)
            {

                // Add dependecies in hirarchy
                AddDependeciesRecursively(containerOptions, dependencies.AppDependencies.Dependencies);

                // Add Shared Dependencies
                if (dependencies.SharedDependencies != null && dependencies.SharedDependencies.Modules != null)
                {
                    containerOptions.AddSharedServices(shared =>
                    {
                        var sharedModules = dependencies.SharedDependencies.Modules;
                        shared.Assemblies = sharedModules == null || sharedModules.Length == 0 ? null : sharedModules;
                    });
                }
            }
            
        }
        #endregion
    }
}
