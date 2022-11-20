# dotenv.core extensions for Microsoft.Extensions.DependencyInjection

[![dotenv.core](https://img.shields.io/badge/.NET%20Standard-2.0-red)](https://github.com/mrdave1999/Dotenv.Extensions.Microsoft.DependencyInjection)
[![dotenv.core](https://img.shields.io/badge/License-MIT-green)](https://raw.githubusercontent.com/MrDave1999/Dotenv.Extensions.Microsoft.DependencyInjection/master/LICENSE)
[![Nuget-Badges](https://buildstats.info/nuget/Dotenv.Extensions.Microsoft.DependencyInjection)](https://www.nuget.org/packages/Dotenv.Extensions.Microsoft.DependencyInjection/)
[![PayPal-donate-button](https://img.shields.io/badge/paypal-donate-orange)](https://www.paypal.com/paypalme/DavidRomanAmariles)


This library adds extension methods for the [Microsoft.Extensions.DependencyInjection](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection) package, which can be accessed through the [IServiceCollection](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection) interface. So, this library is just a wrapper and behind the scenes what happens is that it uses the classes and methods from the [dotenv.core](https://github.com/MrDave1999/dotenv.core) project.

This project was created to add support for DI and can be used in ASP.NET Core projects.

Don't forget to visit the library [website](https://mrdave1999.github.io/Dotenv.Extensions.Microsoft.DependencyInjection) where you can find [API documentation](https://mrdave1999.github.io/Dotenv.Extensions.Microsoft.DependencyInjection/api/Microsoft.Extensions.DependencyInjection.html).

## Installation

If you're want to install the package from Visual Studio, you must open the project/solution in Visual Studio, and open the console using the **Tools** > **NuGet Package Manager** > **Package Manager Console** command and run the install command:
```
Install-Package Dotenv.Extensions.Microsoft.DependencyInjection
```
If you are making use of the dotnet CLI, then run the following in your terminal:
```
dotnet add package Dotenv.Extensions.Microsoft.DependencyInjection
```

## Usage

You only need to invoke the `AddDotEnv` method to add the environment vars using a service:
```cs
// Example in ASP.NET Core 6+
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IServiceCollection services = builder.Services;
services.AddDotEnv<AppSettings>();

var app = builder.Build();
```
The following line of code:
```cs
services.AddDotEnv<AppSettings>();
```
It does several things:
- Invokes the `Load` method of the `EnvLoader` class to set the environment variables from a file named `.env`.
- Invokes the `Bind` method of the `EnvBinder` class to map the keys of the .env file with the `AppSettings` properties.
- Registers `AppSettings` as a singleton for the DI container.

Subsequently, the configuration class can be used in the controllers and the DI container will take care of injecting the instance when necessary:
```cs
public class ExampleController : ControllerBase
{
    private readonly AppSettings _settings;

    public ExampleController(AppSettings settings)
    {
        _settings = settings;
    }
}
```
**Note:** You can also take a look at the [source code](https://github.com/MrDave1999/Dotenv.Extensions.Microsoft.DependencyInjection/tree/master/example) of the example project.

## Contribution

Follow the steps below:

1. Fork it
2. Create your feature branch (git checkout -b my-new-feature)
3. Commit your changes (git commit -am 'Added some feature')
4. Push to the branch (git push origin my-new-feature)
5. Create new [Pull Request](https://github.com/MrDave1999/Dotenv.Extensions.Microsoft.DependencyInjection/pulls)