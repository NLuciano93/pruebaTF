Fusap.Common.Swagger
====================

Standard Fusap patterns for describing Apis alongiside with their graphical UIs.

Features:
* One service to register: `.AddFusapSwagger`;
* Automatic configuration of Api versioning standards;
* Automatic configuration of Swagger UI on `\docs`;
* Automatic redirection of `\` to Swagger UI;
* Configurable security requirements;
* `[SecurityScheme]` to customize what endpoints have what security requirements;
* `[ProducesResponseHeader]` to indicate that an endpoint produces custom headers;
* `[ProducesPaginationResponseHeaders]` to indicate that an endpoint produces paginated responses with standard pagination headers;
* `[ApiExposureLevel]` to indicate what is the desired exposure level that each endpoint should have.

## Getting started

**ATTENTION** This library is embedded into `Fusap.Common.Hosting` and should not be installed directly.

## Producing custom headers

You can indicate that your response include custom headers using the `[ProducesResponseHeader]` attribute as such:

```csharp
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status201Created)]
[ProducesResponseHeader("x-test-number", StatusCodes.Status200OK, Description = "Test number header", Type = HeaderResponseType.Number)]
[ProducesResponseHeader("x-test-string", StatusCodes.Status201Created, Description = "Test string header", Type = HeaderResponseType.String)]
[HttpGet("action-with-custom-response-headers")]
public IActionResult ActionWithCustomResponseHeaders()
{
    // ...
}
```

### Paginated responses

Actions that return paginated responses have a standardized header setup:

```csharp
[HttpGet("action-with-paginated-response")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesPaginationResponseHeaders]
public IActionResult ActionWithPagination([FromHeader(Name = "X-pageNumber")] int pageNumber, [FromHeader(Name = "X-pageSize")] int pageSize)
{
    // ...
}
```

## Authentication

### Using `Authentication: Bearer ...` and endpoints with `[Authorize]`

In order to enable basic header authentication you just have to make sure that your endpoints or controllers have
the `[Authorize]` attribute. No further configuration is needed.

### Using basic http authentication or oauth2

If you are using other authentication schemes, you must configure them likewise.

```
"FusapSwagger": {
  "SecurityDefinitions": [
    {
      "Type": "oauth2",
      "Description": "OAuth 2.0 with Client Credentials Grant type",
      "Flow": "password",
      "TokenUrl": "http://dev.fusapdigital.com/auth/realms/zendesk/protocol/openid-connect/token",
      "AuthorizationUrl": "http://dev.fusapdigital.com/auth/realms/zendesk/protocol/openid-connect/token"
    },
    {
      "Type": "http",
      "Description": "Basic http auth"
    }
  ]
}
```

By default, all endpoints with the `[Authorize]` attribute will be associated with the `OAuth2` scheme.
To change that you can specify a different scheme using the `[SecurityScheme(SecuritySchemeType)]` attribute as such: 

```csharp
[Authorize]
[HttpPut("action-with-oauth2-scheme")]
[SecurityScheme(SecuritySchemeType.OAuth2)]
public IActionResult ActionSecuredWithOAuth2Scheme()
{
    // ...
}

[Authorize]
[HttpGet("action-secured-with-basic-http-scheme")]
[SecurityScheme(SecuritySchemeType.Http)]
public IActionResult ActionSecuredWithBasicHttpScheme()
{
    // ...
}
```

## Api exposure

This package produces 3 OpenApi documents by default, grouping all enpoints by their level of exposure:
1. `Internal`: Apis on this level are intended to be used on the internal network. No traffic should be placed over the Internet.
2. `External`: Apis on this level are intended to be consumed by internal clients over the Internet like a web-frontend or a mobile application.
3. `Open`: Apis on this level are intended to be consumed by third parties over the Internet.

**Attention**: Please note that this does not protect the annotaded Apis. You still need to correctly setup all required firewall rules.

Examples:

```csharp
[HttpGet("/v1/forecast")]
[ApiExposureLevel(ApiExposureLevel.Open)]
public IEnumerable<WeatherForecast> GetOpenForecast()
{
    // ...
}

[HttpGet("/v1/mobile-app/forecast")]
[ApiExposureLevel(ApiExposureLevel.External)]
public IEnumerable<WeatherForecast> GetMobileAppForecast()
{
    // ...
}

[HttpGet("/v1/cluster/forecast")]
// [ApiExposureLevel(ApiExposureLevel.Internal)] -- optional since it is the default
public IEnumerable<WeatherForecast> GetInternalAppForecast()
{
    // ...
}
```
