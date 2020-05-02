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
    },
    "StrictMode": true,
    "Addition": [
      {"Module": "CustomerDiscount"}
    ]
}
```

## Invoke Configuration

```csharp
   using IocServiceStack;
   using IocServiceStack.Config;

   /* Simple configuration */
   IocContainer container = IocServiceStackConfig.Configure();
   // You can use container object to add or replace new extensions
```
#### OR

```csharp
   using IocServiceStack;
   using IocServiceStack.Config;

   /* This configuration method gives little more control to use config object */
   IocServicelet.Configure(config => config.ConfigFromFile());
```
#### OR
```csharp
   using IocServiceStack;
   using IocServiceStack.Config;

   /* This configuration method gives  more control to use config object */
   IocServicelet.Configure(config => {
        config.AddServices(service => {
            service.ConfigFromFile();
            service.StrictMode = true;
        };
   });
```
