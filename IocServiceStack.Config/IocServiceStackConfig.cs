namespace IocServiceStack.Config
{
    using IocServiceStack;

    public static class IocServiceStackConfig
    {
        public static IocContainer Configure()
        {
            var appDependencies = ConfigExtension.DeserializeConfig();
            var container = IocServicelet.Configure(config =>
            {
                ConfigExtension.ConfigFromObject(config, appDependencies);
            });
            ConfigExtension.ConfigAddition(container, appDependencies);
            return container;
        }

    }
}
