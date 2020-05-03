namespace IocServiceStack.Config
{
    using System;
    using System.Reflection;
    
    internal static class AdditionProcess
    {
        public static void Process(AdditionInfo additionInfo, IocContainer iocContainer)
        {
            // Look for static IocServiceStackConfig
            // Execute method Config (IocContainer)

            if (string.IsNullOrWhiteSpace(additionInfo.Module))
            {
                return;
            }

            string defaultConfigClass = additionInfo.Module + ".IocServiceStackConfig";
            string confMethod = "Configure";
            string configClass = !string.IsNullOrWhiteSpace(additionInfo.ConfigClass)
                                ? additionInfo.ConfigClass
                                : defaultConfigClass;

            try
            {
                Assembly assembly = Assembly.LoadFrom(additionInfo.Module);
                Type type = assembly.GetType(configClass, throwOnError: false);
                if (type != null)
                {
                    var configStaticMethod = type.GetMethod(confMethod, BindingFlags.Static | BindingFlags.Public);
                    configStaticMethod.Invoke(null, new[] { iocContainer });
                }
            }
            catch
            {
                // Ignore any error
            }
        }
    }
}
