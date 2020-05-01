namespace IocServiceStack.Config
{
    using System.IO;
    using Newtonsoft.Json;
    using IocServiceStack;

    public static class ConfigExtension
    {
        public static void ConfigFromJsonContent(this ContainerConfig containerConfig, string appDependeciesJson)
        {
            var dependencies = JsonConvert.DeserializeObject<AppDependenciesObject>(appDependeciesJson);

            containerConfig.AddServices(service =>
            {
                // Add Root Assemblies
                var modules = dependencies.AppDependencies.Modules;
                service.Assemblies = modules == null || modules.Length == 0 ? null : modules;

                if (service.Assemblies != null)
                {
                    // Add dependecies in hirarchy
                    AddDependeciesRecursively(service, dependencies.AppDependencies.Dependencies);

                    // Add Shared Dependencies
                    if (dependencies.SharedDependencies != null && dependencies.SharedDependencies.Modules != null)
                    {
                        service.AddSharedServices(shared =>
                        {
                            var sharedModules = dependencies.SharedDependencies.Modules;
                            shared.Assemblies = sharedModules == null || sharedModules.Length == 0 ? null : sharedModules;
                        });
                    }
                }
            });
        }

        public static void ConfigFromFile(this ContainerConfig containerConfig)
        {
            var fileContent = File.ReadAllText("appdependencies.json");
            ConfigFromJsonContent(containerConfig, fileContent);
        }

        private static void AddDependeciesRecursively(ContainerOptions containerOptions, AppDependencies dependencies)
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

                AddDependeciesRecursively(containerOptions, dependencies.Dependencies);
            });
        }
    }
}
