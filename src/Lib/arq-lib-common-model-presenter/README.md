# Fusap.Common.Model.Presenter.WebApi

This library provides the basis for an uniform web api presentation and is centered around the [Fusap.Common.Model](https://dev.azure.com/Fusapdigital-HoldCo/global-platform/_git/arq-lib-common-model?path=%2FREADME.md&_a=preview) and [Fusap.Common.Mediator](https://dev.azure.com/Fusapdigital-HoldCo/global-platform/_git/arq-lib-common-mediator?path=%2FREADME.md&_a=preview) packages.

## Getting started

On your presentation layer, install the package:

```powershell
Install-Package Fusap.Common.Model.Presenter.WebApi
```

Register the services:

```csharp
services.AddFusapPresenter();
```

Then, new controllers can be created by simply inheriting from `FusapApiController`.

```csharp
[Authorize]
[ApiVersion("2")]
[Route("{legalPersonId}/[Controller]")]
public class AssociationsController : FusapApiController
{
    /// <summary>
    /// Creates a new association
    /// </summary>
    /// <param name="legalPersonId">The id of the legal person</param>
    /// <param name="input">The association data</param>
    /// <returns>The id of the association created</returns>
    [HttpPost]
    public async Task<ActionResult<CreateAssociationResult>> CreateAssociationAsync(Guid legalPersonId, AssociationData input)
    {
        var request = Map<CreateAssociationRequest>(input);

        request.LegalPersonId = legalPersonId;
        request.UserId = User.GetSubjectGuid();

        var result = await Send(request);

        return Created(result);
    }

    /// <summary>
    /// Searches associations
    /// </summary>
    /// <param name="legalPersonId">The id of the legal person</param>
    /// <param name="input">The several filters that can be applied when searching</param>
    /// <returns>The associations that match the given filters</returns>
    [HttpGet]
    public async Task<ActionResult<Pagination<AssociationViewModel>>> SearchAssociationsAsync(Guid legalPersonId,
        [FromQuery] SearchAssociationsInput input)
    {
        var request = Map<SearchAssociationsRequest>(input);

        request.LegalPersonId = legalPersonId;
        request.UserId = User.GetSubjectGuid();

        var result = await Send(request);

        return OkAs<Pagination<AssociationViewModel>>(result);
    }
}
```

Let`s understand all the subtle differences from the tradicional controllers.

Please note that the mediator requests on this example return `IResult` or `IResult<TValue>`. If your mediator requests return different structures you will have to adequate accordingly.

#### Reduced number of attributes needed

The following attributes are defined on the `FusapApiController` base class and no longer need to be declared on every single controller:

* `[ApiConventionType(typeof(FusapApiConventions))]`
* `[ApiController]`
* `[ProducesErrorResponseType(typeof(void))]`
* `[Produces("application/json")]`
* `[Consumes("application/json")]`

The following attributes are defined on the `FusapApiConventions` convention and no longer need to be declared on every single action:

* `[ProducesResponseType(StatusCodes.Status200OK)]`
* `[ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]`
* `[ProducesResponseType(typeof(ApiError), StatusCodes.Status422UnprocessableEntity)]`
* `[ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]`
* `[ProducesResponseType(StatusCodes.Status401Unauthorized)]`
* `[ProducesResponseType(StatusCodes.Status403Forbidden)]`

[Api conventions](https://docs.microsoft.com/en-us/aspnet/core/web-api/advanced/conventions?view=aspnetcore-3.1) are a built in mechanism on AspNetCore that will apply pattern matching to the route names and http verbs to identity what are the applicable attributes in order to reduce boiler plate code on your controllers.

#### Constructors no longer needed

First note that there is no constructor. The `FusapApiController` uses the AspNetCore Mvc infrastructure to provide direct shortcuts to 3 key functionalities that all controllers depend on:

* `Presenter`: returns an instance of `IPresenter`.
* `Mediator`: returns an instance of `IMediator`.
* `Mapper`: returns an instance of `IMapper`.

If you need to inject specific instances, for testing for example, you can set the value you desire and it will be used. If you don't set anything, the instance will be retrieved from the service provider that is scoped to the request being served.

#### Shortcuts to common functionalities

Some shortcuts are provided to make the code shorter, these are some examples of usage for mapping, sending using mediator and presenting:

* `Map<CreateAssociationRequest>` and `Map<SearchAssociationsRequest>`: the `Map<T>` shortcut uses `AutoMapper` to translate the input object into requests that can be sent to the application layer.
* `Send(request)`: this shortcut sends the requests to the application layer using Mediator.
* `Created(result)`: presents the `Fusap.Common.Model.IResult` received directly. If it holds an error, the error will be rendered, if it holds a successful value, the value will be rendered with the HttpStatus of Created.
* `OkAs<Pagination<AssociationViewModel>>`: presents the `IResult` received after mapping it to a view model. In case of success, it will use `AutoMapper` to apply the transformation to the view model and then render it as HttpStatus of Ok. In case of failure, will render the error.

There are shortcuts for all successful responses like `Ok` / `OkAs`, `Created` / `CreatedAs`, `Accepted` / `AcceptedAs` and `NoContent`, all of which take an `IResult`, render the error if not successful and render the appropriate error code optionally mapping to the view model.

#### `ActionResult<TValue>` compile-time checks

When declaring actions, please use the return type of `ActionResult<TValue>` instead of the traditional `IActionResult`. The strongly typed version will provide two key benefits:

1. `TValue` will be used by `SwaggerGen` to indicate the schema returned by this endpoint. There is no need to add attributes like `[ProducesResponseType(typeof(TValue))]`.
2. The compiler will ensure during build time that you are returning a value that is compatible with `TValue`. If you try to pass something different, it will not compile.

This reduces the chances of someone changing the return of the method and forgetting to change the type indicated on the attribute. They both must match.

