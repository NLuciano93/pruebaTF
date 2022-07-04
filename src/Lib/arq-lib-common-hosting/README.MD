Fusap.Common.Hosting
====================

Single point of entry for all the basic needs that a Fusap project might have.

Features:

* 3 step process to bring a `dotnet new` project to Fusap standards
* No need to modify any file other than `Program.cs` and `launchSettings.json`
* Projects can focus on solving their core mission
* Simplified way of deploying improvements to Fusap standards

The FusapHost configuration is comprised of:

* Fusap remote configuration server activated with defaults
* Fusap logger configured with defaults
* Healthchecks automatically configured on `/health` with `UIResponseWriter` as the `ResponseWriter`
* Hot reload of settings enabled on `/refresh`
* Application insights enabled
* Open tracing of outgoing http requests

The FusapWebApi further configures:

* Api versioning support configured with defaults
* Automatically adds the version prefix to all controllers
* Swagger and Swagger UI
* Automatic fallback redirect of / to Swagger documentation
* Default configuration for lowercase routes and json ignore null values
* Http context acessor
* Open tracing of incoming http requests
* No `server:kestrel` response header

The FusapWorker configures a very light Kestrel server in order to support healthckecks and configuration hot-reloading.


## Introductory video

Please watch this session were we introduce a lot of these concepts to new team members:

https://web.microsoftstream.com/video/b8661018-24ca-4fff-a249-955862e3973f


## Getting started on WebApis

Install the package

```
Install-Package Fusap.Common.Hosting.WebApi
```

Configure the host on your `Program.cs`:

```csharp
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureFusapHost(options => options.ApplicationName = "sample-web")      // Specify the app name here
            .ConfigureFusapWebApi<Startup>(apiInfo =>
            {
                apiInfo.Title = "Fusap Api";                                            // Specify the OpenApi title and description
                apiInfo.Description = "Fusap host test";
            });
```

On your .csproj, enable the output of Xml documentation so that Swagger can include the descriptions for the Apis:

```
  <PropertyGroup>
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
    <NoWarn>$(NoWarn);1591</NoWarn>
  </PropertyGroup>
```

That is it! There is no need to modify any other file. The project can focus on the core mission that it aims to solve.

## Getting started on Workers

In order for worker services to report healthcheck and to allow hot reloading of settings they must be created for the SDK `Microsoft.NET.Sdk.Web`.

Install the package:

```
Install-Package Fusap.Common.Hosting.Worker
```

Configure the host on your `Program.cs`:

```csharp
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureFusapHost(options => options.ApplicationName = "sample-worker")   // Specify the app name here
            .ConfigureFusapWorker()                                                     // Configure basic worker infrastructure
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<Worker>();
                //...
            });
```

That is it!

## Swagger quality checks

Since hosting version 1.0.47 there are quality checks built in on swagger.

These checks are in place to ensure that all endpoints are correctly documented:

#### Rule 1: All endpoints must have summaries

Endpoints without xmldocs will fail:

```csharp
[HttpGet]
public IEnumerable<Forecast> GetForecasts()
{
    // ...
}
```

This will pass:

```csharp
/// <summary>
/// Returns weather forecasts
/// </summary>
/// <returns></returns>
[HttpGet]
public IEnumerable<Forecast> GetForecasts()
{
    // ...
}
```

If you forget to add the `GenerateDocumentationFile` property to the `.csproj` file, it will
also fail.

If you have to disable this rule, *not recommended*, add to your configure services:

```csharp
services.Configure<SwaggerDocumentValidationOptions>(opt =>
    opt.RequireOperationSummaries = false);
```

#### Rule 2: All parameters must have descriptions

If your endpoints receive parameters and they are not documented, it will fail:

```csharp
/// <summary>
/// Obtains the configuration of a given country id
/// </summary>
/// <returns></returns>
[HttpGet]
public CountryConfiguration GetCountryConfiguration(string countryId)
{
    // ...
}
```

Simply add a description to the parameter so it will pass:

```csharp
/// <summary>
/// Obtains the configuration of a given country id
/// </summary>
/// <param name="countryId">The iso code of the country</param>
/// <returns></returns>
[HttpGet]
public CountryConfiguration GetCountryConfiguration(string countryId)
{
    // ...
}
```

If you have to disable this rule, *not recommended*, add to your configure services:

```csharp
services.Configure<SwaggerDocumentValidationOptions>(opt =>
    opt.RequireOperationParameterDescriptions = false);
```

#### Rule 3: All endpoints must produce at least one successful status code

If no `[ProducesResponseType]` are specified one successful status code will be implied, so this will pass:

```csharp
[HttpGet]
public Forecast GetForecast(guid forecastId)
{
    // ...
}
```

If you use any `[ProducesResponseType]` attributes you will have to make sure to include one that is of success (between 100 and 299), so since this does not describe any sucessful returns, it will fail:

```csharp
[HttpGet]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public Forecast GetForecast(guid forecastId)
{
    // ...
}
```

When specifying both the error and successful codes, it will pass:

```csharp
[HttpGet]
[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Forecast))]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public Forecast GetForecast(guid forecastId)
{
    // ...
}
```

If you have to disable this rule, *not recommended*, add to your configure services:

```csharp
services.Configure<SwaggerDocumentValidationOptions>(opt =>
    opt.RequireAtLeastOneNonErrorResponse = false);
```

#### Rule 4: All endpoints must have unique operation ids

The default operation id is the action method name. If you have the same action method
name repeated on multiple controllers, it will fail:

```csharp
public class WeatherController {
    [HttpGet]
    public Forecast Get(guid forecastId) // operation id = "Get", duplicated
    {
        // ...
    }
}

public class WeatherControllerV2 {
    [HttpGet]
    public ForecastV2 Get(guid forecastId) // operation id = "Get", duplicated
    {
        // ...
    }
}

public class UserController {
    [HttpGet]
    public User Get(guid userId) // operation id = "Get", duplicated
    {
        // ...
    }
}
```

You can rename the method names to be more specific since they represent different
operations so this will pass:

```csharp
public class WeatherController {
    [HttpGet]
    public Forecast GetForecastV1(guid forecastId)
    {
        // ...
    }
}

public class WeatherControllerV2 {
    [HttpGet]
    public ForecastV2 GetForecastV2(guid forecastId)
    {
        // ...
    }
}

public class UserController {
    [HttpGet]
    public User GetUserV1(guid userId)
    {
        // ...
    }
}
```

You can optionally set the operation id directly on the attribute, so this will also
pass:

```csharp
public class WeatherController {
    [HttpGet(Name = "GetForecastV1")]
    public Forecast Get(guid forecastId)
    {
        // ...
    }
}

public class WeatherControllerV2 {
    [HttpGet(Name = "GetForecastV2")]
    public ForecastV2 Get(guid forecastId)
    {
        // ...
    }
}

public class UserController {
    [HttpGet(Name = "GetUserV1")]
    public User Get(guid userId)
    {
        // ...
    }
}
```

If you have to disable this rule, *not recommended*, add to your configure services:

```csharp
services.Configure<SwaggerDocumentValidationOptions>(opt =>
    opt.RequireSingleOperationIds = false);
```


#### Rule 5: all endpoints that require a token must include a possible 401 response

If you have an endpoint that has the `[Authorize]` attribute but does not indicate a possible
401 response, it will fail:

```csharp
[Authorize]
[HttpGet]
public Forecast GetForecast()
{
    // ...
}
```

Simply describe the correct expected responses so it will pass:

```csharp
[Authorize]
[HttpGet]
[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Forecast))]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public Forecast GetForecast()
{
    // ...
}
```

If you have to disable this rule, *not recommended*, add to your configure services:

```csharp
services.Configure<SwaggerDocumentValidationOptions>(opt =>
    opt.Require401ResponseForOperationsWithSecuritySchemes = false);
```


#### Rule 6: all `DELETE` operations should include a possible 404 response

Any endpoints that use the `Delete` method should include a possible 404 response
in the case the resouce being deleted no longer exist.

```csharp
[HttpDelete]
public void DeleteForecast()
{
    // ...
}
```

Simply describe the correct expected responses so it will pass:

```csharp
[Authorize]
[HttpDelete]
[ProducesResponseType(StatusCodes.Status204NoContent)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public void DeleteForecast()
{
    // ...
}
```

If you have to disable this rule, *not recommended*, add to your configure services:

```csharp
services.Configure<SwaggerDocumentValidationOptions>(opt =>
    opt.Require404ResponseForDeleteOperations = false);
```

## Upgrading older solutions

Older solutions can be upgraded to this standard first by following the steps above, then:

1. If the project is already using any of the following libraries, upgrade to the latest versions to avoid compatibility issues.
   If the project is NOT using these libraries, DO NOT install them.
    * `Fusap.GlobalPlatform.Mediatr` minimum version is 1.0.18;
    * `Fusap.GlobalPlatform.WebApi` minimum version is 1.0.19.


2. Remove options from `appsettings*.json`:
    * `spring`;
    * `ApplicationInsights`;
    * `JAEGER_*`;


3. Remove old references that have been incorporated into `Fusap.Common.Hosting`:
    * `AspNetCore.HealthChecks.UI.Client`;
    * `Jaeger`;
    * `Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer`;
    * `PlusUltra.OpenTracing.HttpPropagation`;
    * `Fusap.Common.Tracer`;
    * `Fusap.GlobalPlatform.Tracer`;
    * `Fusap.ConfigServer.Client.Extensions.Hosting`;
    * `Fusap.GlobalPlatform.Swagger`;
    * `Fusapdigital.ApplicationInsights`;
    * `Fusapdigital.Extensions.Logging`;
    * `Swashbuckle.AspNetCore`;
    * `Swashbuckle.AspNetCore.ReDoc`;


4. Cleanup `Program.cs`:
    * Remove all `Serilog` related code;
    * Remove the try-catch-finally around `CreateHostBuilder(args).Build().Run();`;
    * Remove `private static readonly LoggerProviderCollection Providers = new LoggerProviderCollection();`
    * Remove `Activity.DefaultIdFormat = ActivityIdFormat.W3C;`;
    * Remove `.ConfigureWebHostDefaults`
    * Remove `.PropagateHttpTracingContext`
    * Remove `.AddConfigServer`
    * Remove `.AddFusapdigitalApplicationInsightsWorkerService`
    * Remove `.ConfigureFusapdigitalLogging`


    Your `Program.cs` should look like this:

    ```csharp
    using Microsoft.Extensions.Hosting;

    namespace Authorizer.Master.Api
    {
        public static class Program
        {
            public static void Main(string[] args)
            {
                CreateHostBuilder(args).Build().Run();
            }
            public static IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args)
                    .ConfigureFusapHost(options => options.ApplicationName = "cd-ms-authorizer-mastercard")
                    .ConfigureFusapWebApi<Startup>(apiInfo =>
                    {
                        apiInfo.Title = "Mastercard Authorizer";
                        apiInfo.Description = "REST Api for Mastercard ISO 8583 operations";
                    });
        }
    }
    ```


5. Cleanup `Startup.cs` and related bootstrap providers:
    * ConfigureServices
        * Remove `.AddTraceIncomingRequestFilter`;
        * Remove `.AddHttpContextAccessor`;
        * Remove Api version configuration `.AddVersionedApiExplorer` and `.AddApiVersioning`;
        * Remove the standalone `.AddHealthChecks();`;
        * Remove `.AddJsonOptions(options => options.JsonSerializerOptions.IgnoreNullValues = true)`;
        * Remove `.AddRouting(options => options.LowercaseUrls = true)`;
        * Remove `.AddFusapTracer(configuration)`;
        * Remove `.AddFusapSwagger`
            If you need to customize the Api document generation you can do so through the built in configuration pattern as such:
            ```csharp
            services.Configure<SwaggerGenOptions>(options =>
            {
                options.OperationFilter<DefaultHeaderFilter>();
                options.CustomSchemaIds(x => x.FullName);
            });
            ```
        * Remove `.AddRefreshActuator()`;
        * Remove `.AddScoped<IApiVersionDescriptionProvider, DefaultApiVersionDescriptionProvider>();` or any other form of registration of `IApiVersionDescriptionProvider`; 

    * Configure
        * Remove redirect from `/` to `/docs`;
        * Remove healthcheck endpoint: `endpoints.MapHealthChecks("/health"...`;
        * Remove `.UseFusapdigitalRequestLogging`;
        * Remove `.UseSerilogRequestLogging`;
        * Remove `.UseFusapSwagger`;
        * Remove `.UseRefreshActuator`;
        * Remove `app.ApplicationServices.GetRequiredService<ITracer>()`;
        * Remove all method parameters that are no longer used (ex: `IHostApplicationLifetime appLifetime`, `IApiVersionDescriptionProvider provider`, `IConfiguration configuration` etc)


6. Launch settings:

    * Remove lines containing `**/Properties/launchSettings.json` or `launchSettings.json` from the `.gitignore` file so that it will be versioned.
    * On each `launchSettings.json`, remove the key `iisSettings` and the `IIS Express` profile.


7. Ensure that your helm deployment has Jaeger enabled as a sidecar, the `helm\xxx\templates\deployment.yml` must have the annotation `sidecar.jaegertracing.io/inject` set to `true`:

    ```yaml
    apiVersion: apps/v1
    kind: Deployment
    metadata:
      name: xxxxx
      labels:
        xxxxx
      annotations:
        sidecar.jaegertracing.io/inject: "true" <<< Add this annotation
    ```

8. If the project uses endpoints with the `[Authorize]` attribute to authenticate using the `Authorization` header, Swagger will be configured automatically. 
    If you are using a different security scheme, please refer to the [Fusap.Common.Swagger](https://dev.azure.com/Fusapdigital-HoldCo/global-platform/_git/arq-lib-common-swagger) documentation for more information.


9. If your solution uses Silverback, refer to [Fusap.Common.Silverback](https://dev.azure.com/Fusapdigital-HoldCo/global-platform/_git/arq-lib-common-silverback) for the new usage pattern.
