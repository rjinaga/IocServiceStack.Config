# IocServiceStack.Config
Config dependencies through AppDependencies.Json config file



## [NuGet](https://www.nuget.org/packages/IocServiceStack/)
```
PM> Install-Package IocServiceStack.Config 
```
[![NuGet Release](https://img.shields.io/badge/nuget-0.0.4-blue)](https://www.nuget.org/packages/IocServiceStack.Config/)

## Sample appdependencies.json

```json
{
    "AppDependencies": {
        "Name": "BusinessServices",
        "Modules": [
           "App.Customer.BusinessService",
           "App.Admin.BusinessService",
        ],
        "Dependencies": {
          "Name": "Data",
          "Modules": [
            "App.DataService",
          ]
        }
    },
    "SharedDependencies" : {
      "Modules": [
        "SharedImplementationLib"
      ]
    }
}
```

## Invoke Configuration

 ```csharp
   using IocServiceStack;
   using IocServiceStack.Config;
   
   public static class ConfigureIocServiceStack {
        public static void Configure() {
            IocServicelet.Configure(config => config.ConfigFromFile());
        }
   }
```
