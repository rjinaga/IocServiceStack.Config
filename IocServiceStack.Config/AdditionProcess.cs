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

            
            Assembly assembly = Assembly.LoadFrom(additionInfo.LoadFrom + additionInfo.Module + ".dll");
            Type type = assembly.GetType(configClass, throwOnError: true);
            
            var configStaticMethod = type.GetMethod(confMethod, BindingFlags.Static | BindingFlags.Public);

            if (configStaticMethod == null)
            {
                throw new Exception($"Static method '{confMethod}' does not found in specified type.");
            }
            configStaticMethod.Invoke(null, new[] { iocContainer });

        }
    }
}
