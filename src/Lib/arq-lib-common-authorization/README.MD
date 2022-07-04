Fusap.Common.Authorization
==========================

Collection of common tools that unify authorization of access across all projects.

## Fusap.Common.Authorization.Client

Provides an auto configured client that exposes an API to check if a given identity can take a 
particular action on a given resource.

### Concepts

1. `Identity`: The party that is requesting access, this will most likely be the user, eg: a 
`Person`.
2. `Resource`: The entity that is the aggregate root where the actions will take place, eg: 
an `Account`, a `Card`, a `Person`.
3. `Action`: The action itself that is meant to be executed, eg: `"account:view-balance"`, 
`"payroll:upload-associates"`, `"card:change-pin"`.

Identities and resources are specified as `Uri`s that point to the URN of the resource 
according with the WIKIPAGE.

Examples:

* A person with id of `GUID` is identified as `urn:fusap:resource:person:GUID`.
* An account with id of `GUID` is identified as `urn:fusap:resource:account:GUID`.

In some cases, depending on the context, a person could be both the identity and the 
resource at the same time, for instance
when a user is trying to modify information about anoter person on the system.

#### Basic building blocks

* `Requirement`: Specifies one or more actions that an identity wants to take on one 
or more resources.
* `ResourceAuthority`: Defines a server that is capable of handling all access authorization 
for resources that match a given list of prefixes.
* `IFusapAuthorizationClient`: Client that you inject in order to grant access to features.
* `AuthorizationResult`: Response of an `IFusapAuthorizationClient` regarding all the requirements that
were sent for authorization. Each individual requirement can be checked for success or you can check
the response as a whole.

### Installing instructions

Install the package

```
Install-Package Fusap.Common.Authorization.Client
```

Register the client

```
services.AddFusapAuthorizationClient();
```

### Usage instructions

First inject the `IFusapAuthorizationClient` into the class where you need to check access. Then there are 
several alternatives that depend on your usage scenario.

This library checks permissions based on `Requirement`. A single `Requirement` take one 
identity, one or more resources and one or more actions and if granted, will mean that the 
identity owner can perform all indicated actions on all indicated resources.

You can check as many requirements combinations as you need in a single call by passing them in an
array.

The following example shows how one could check multiple access levels in order to enable/disable
options on the UI:

```csharp
[ApiController]
[Route("[controller]")]
public class UiActionsController : ControllerBase
{
    private readonly IFusapAuthorizationClient _fusapAuthorizationClient;

    public TestController(IFusapAuthorizationClient fusapAuthorizationClient)
    {
        _fusapAuthorizationClient = fusapAuthorizationClient;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUiActionsAsync()
    {
        var personIdentity = FusapResources.Person(User.GetSubjectGuid());
        var account1Resource = FusapResources.Account(Guid.NewGuid());
        var account2Resource = FusapResources.Account(Guid.NewGuid());

        var canViewBothBalances = new Requirement(
            personIdentity,
            new[] { account1Resource, account2Resource },
            "account:view-balance"
        );
        var canTransferFromAcc1 = new Requirement(
            personIdentity,
            account1Resource,
            "account:transfer-funds", "account:view-transfer-receipts"
        );

        // You can query any number of requirements as long as all resources can be authorized by a single ResourceAuthority
        var requirements = new[] { canViewBothBalances, canTransferFromAcc1 };

        var authorization = await _fusapAuthorizationClient.AuthorizeAsync(requirements);

        var uiActions = new UiActions {
            CanViewBalances = authorization[canViewBothBalances],
            CanTransfer = authorization[canTransferFromAcc1]
        };

        return Ok(uiActions);
    }
}
```

The example above is the most powerful but most verbose way of checking permissions. There are 
several extensions and overloads that reduce the amount of boilerplate if you just to check a single 
requirement:

```csharp
if (await _fusapAuthorizationClient.AuthorizeAsync(personIdentity, account1Resource, "account:view-balance"))
{
    // personIdentity can view balance of account 1.
}
```

```csharp
if (await _fusapAuthorizationClient.AuthorizeAsync(personIdentity, new[] { account1Resource, account2Resource },
    new[] { "payroll:upload-batch-transaction", "payroll:upload-batch-associate" }))
{
    // personIdentity can upload batch transactions and batch associates to accounts 1 and 2.
}
```

### Testing locally

To test your projects locally you should configure your environment to mock an authorization server as described here.

Register the mock authrization server by adding the following to your `appsettings.[local/development/tracing].json`:

```json
{
  "FusapAuthorizationClient": {
    "Authorities": [
      {
        "ConnectionString": "http://localhost:8083/grant",
        "Resources": [
          "urn:fusap:"
        ]
      }
    ]
  }
}
```

Add the following service to your `docker-compose`:

```yaml
version: "3.4"
services:
  mock-authorization:
    image: "jordimartin/mmock"
    ports:
      - "8082:8082"
      - "8083:8083"
    volumes:
      - ./mock:/config
```

In a folder called `mock`, add the file `grant.json`:

```json
{
	"request": {
		"method": "POST",
		"path": "/grant"
	},
	"response": {
		"statusCode": 200,
		"headers": {
			"Content-Type":["application/json"]
		},
		"body": "[true,true,true,true,true,true,true,true,true,true,true,true]"
	}
}
```

Test the setup with a `docker-compose up`. All requirements passed will be approved.

## Fusap.Common.Authorization.WebApi

[![Fusap.Common.Authorization.WebApi package in Fusapdigital-HoldCo feed in Azure Artifacts](https://feeds.dev.azure.com/Fusapdigital-HoldCo/_apis/public/Packaging/Feeds/72f2e0cc-697d-43a3-9176-36504cdbfa58/Packages/88b2faf9-e720-4871-9904-0cd68adf9e6c/Badge)](https://dev.azure.com/Fusapdigital-HoldCo/global-platform/_packaging?_a=package&feed=72f2e0cc-697d-43a3-9176-36504cdbfa58&package=88b2faf9-e720-4871-9904-0cd68adf9e6c&preferRelease=true)

Provides extensions that simplify Jwt options binding and certificate parsing.

### Installing instructions

Install the package

```
Install-Package Fusap.Common.Authorization.WebApi
```

Add the Jwt authentication scheme loading options from the default section:

```csharp
    // Add authentication with default scheme and options binding
    services.AddFusapAuthentication(Configuration);
```

If you store Jwt options on a non-standard configuration section or if extra options must be
passed in you can also bind manually:

```csharp
    // Customize authentication options but use binder extension
    services.AddAuthentication("custom-sample-scheme")
        .AddJwtBearer("custom-sample-scheme", opt =>
        {
            opt.BindWithFusapOptions(Configuration.GetSection("Jwt-Custom"));

            opt.Authority = "test";
        });
```

Configue your middleware pipeline to include the following on your `startup.cs`:

```csharp
    app.UseAuthentication();
    app.UseAuthorization();
```

Audience validation is enabled by default on clusterized environments. Plase make sure that your service is properly configured on all config-repos with the following option:

```yaml
Jwt:
  Audience: api-resource-audience           // Usually the service name
```

### Usefull extensions

On `ClaimsPrincipal`:

```csharp
    User.GetSubjectGuid() // ClaimTypes.NameIdentifier parsed as Guid or default
```

On `IFusapAuthorizationClient`:

```csharp
    if (await _fusapAuthorizationClient.AuthorizeAsync(User, account1Resource, "account:view-balance"))
    {
        // User logged in as Person can view balance of account 1.
    }
```

```csharp
    if (await _fusapAuthorizationClient.AuthorizeAsync(User, new[] { account1Resource, account2Resource },
        new[] { "payroll:upload-batch-transaction", "payroll:upload-batch-associate" }))
    {
        // User logged in as Person can upload batch transactions and batch associates to accounts 1 and 2.
    }
```