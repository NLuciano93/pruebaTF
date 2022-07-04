Fusap.Common.Logger
===================

Standardized logging for all Fusap services.

## Getting started

**ATTENTION** This library is embedded into `Fusap.Common.Hosting` and should not be installed directly.

## Using `ILogger<T>`

In order to have properly contextualized logs, always get an `ILogger` instance from DI:

```csharp
private readonly ILogger<WeatherForecastController> _logger;

public WeatherForecastController(ILogger<WeatherForecastController> logger)
{
    _logger = logger;
}

[HttpGet]
public IEnumerable<WeatherForecast> Get()
{
    var position = new { Latitude = 25, Longitude = 134 };
    _logger.LogInformation("Processed {position}", position);

    // ...
}
```

## Structured logging

Structured logging **is not string interpolation**.

Example:
```csharp
_logger.LogInformation("The quick {color} {firstAnimal} jumped over the {adjective} {secondAnimal} {numberOfTimes} times",
    color, animal1, adj, animal2, random.Next(2, 10));
```

The code above will create a log entry that has the final rendered message as well as the message template and each property used in a strongly typed field.

Result:
```json
{
    "eventId": "d00f700d",
    "appName": "sample-log-app-web",
    "appVersion": "1.0.0",
    "level": "Information",
    "type": "Technical",
    "message": "The quick \"brown\" \"fox\" jumped over the \"agitated\" \"horse\" 6 times",
    "messageTemplate": "The quick {color} {firstAnimal} jumped over the {adjective} {secondAnimal} {numberOfTimes} times",
    "sourceContext": "SampleLogApp.Web.VerboseBackgroundService",
    "platform": "core",
    "environment": "Development",
    "host": "10.0.2.41",
    "properties": {
        "firstAnimal": "fox",
        "adjective": "agitated",
        "color": "brown",
        "secondAnimal": "horse",
        "numberOfTimes": 6,
    },
    "timestamp": "2020-12-17T14:23:27.7758550Z"
}
```

This allows the log entries to be indexed according with the properties passed which greatly simplifies how one can search for specific messages.

Please note that the properties are strongly-typed, so you can query using math operators example: `properties.numberOfTimes >= 4 AND properties.adjective = "agitated"`.

## Log types

There are four basic log types: `ActivityLog`, `FunctionalLog`, `SecurityLog` and `TechnicalLog`.

All logs collected by the `RequestLoggingMiddleware` are automatically categorized as `ActivityLog` and by default all other logs are categorized as `TechnicalLog`.

In order to produce logs for other categories you can inject `ILogger<T>` where `T` is one of the log types. 

```csharp
    private readonly ILogger<FunctionalLog> _functionalLogger;
    private readonly ILogger<SecurityLog> _securityLogger;

    public ClassConstructor(ILogger<FunctionalLog> functionalLogger, ILogger<SecurityLog> securityLogger)
    {
        _functionalLogger = functionalLogger;
        _securityLogger = securityLogger;
    }

    public void DoWork()
    {
        _functionalLogger.LogInformation("Something is going on");

        _securityLogger.LogWarning("Security warning!");
    } 

```

## Json formatting

### Fusap log format

To output using the Fusap standard, simply change the formatter:

```json
  "Serilog": {
    "WriteTo": [
      {
        "Name": "Console",
        "Args": {
          "restrictedToMinimumLevel": "Warning"
          "formatter": "Fusap.Common.Logger.FusapJsonLogFormatter, Fusap.Common.Logger"
        }
      }
    ]
  }
```

This setting is read from the standard `IConfiguration`, so it can be set globally by a remote configuration server, environment, command line switch, etc.

To test on your development machine you can enable machine-readable logs by using a launch profile such as:

```json
"SampleLogApp.Web [machine-readable log]": {
    "commandName": "Project",
    "launchBrowser": true,
    "launchUrl": "weatherforecast",
    "applicationUrl": "http://localhost:5000",
    "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development",
        "Serilog__WriteTo__1__Args__Formatter": "Fusap.Common.Logger.FusapJsonLogFormatter, Fusap.Common.Logger"
    }
}
```

### Santander log format

To output using the Santander standard, use specific formatter:

```json
  ...
  "formatter": "Fusap.Common.Logger.SantanderJsonLogFormatter, Fusap.Common.Logger"
  ...
```

The Santander log format is as follows:

![santander-logs](resources/santander-format.png "Santander Log Format")

## Configuring Serilog Kafka Sink to pull SSL CA Cert from Configuration

If a Kafka Sink is configured with SSL by the app using this library, it's possible to reference a CA Certificate from IConfiguration instead of relying on a pre-existing file.
During startup, if Kafka's Sink `bootstrapServers` has `SSL` in it, this library will look for a certificate's content under `Kafka.SslCaContent`. Then this a certificate file will be created on local storage.

The certificate's path will be composed as `{sslCaFolder}/{sslCaFilename}`, where by default `sslCaFolder` is `Path.GetTempPath()` (OS-Specific) and `sslCaFilename` is `ca-cert`.
Those can also be configured with `Kafka:SslCaTargetFolder` and `Kafka:SslCaFileName` respectively.

To proper configure SSL for Confluent Kafka Sink in Serilog, make sure to use the following configuration:

```yaml
# Make sure that Kafka properties are also configured for SSL, e.g.:

Kafka:
  SslCaContent: "-----BEGIN CERTIFICATE-----REPLACE WITH CERTIFICATE CONTENT WITH LINE BREAKS-----END CERTIFICATE-----"
  SecurityProtocol: "SSL"
  Port: "9093"

# For Kafka Sink, those are the only properties needed for SSL:
Serilog:
  WriteTo:
    - Name: Kafka
      Args:
        securityProtocol: Ssl
        sslCaLocation: /tmp/ca-cert
```

Notice that, in this case, `/tmp/ca-cert` is the *default* certificate's path for *linux*, for windows the default configuration would be: 

```yaml
Serilog:
  WriteTo:
    - Name: Kafka
      Args:
        securityProtocol: Ssl
        sslCaLocation: /Users/my-user/AppData/Local/Temp/ca-cert
```

*Optionally*, if for some reason, e.g., file permissions, you need to use a different CA Certificate location, or filename, it's also possible by adding the following:

```yaml
Kafka:
  SslCaTargetFolder: /a-folder/another-folder
  SslCaFileName: another-name

Serilog:
  WriteTo:
    - Name: Kafka
      Args:
        securityProtocol: Ssl
        sslCaLocation: ${Kafa.SslCaTargetFolder}/${Kafa.SslCaFileName}
```