# Fusap.Common.Mediator

Common abstractions to simplify and tie together the application and presentation layers using middlewares to handle validation (using [Fluent Validation](https://fluentvalidation.net/)) and authorization (using [Fusap Authorization](https://dev.azure.com/Fusapdigital-HoldCo/global-platform/_git/arq-lib-common-authorization)).

This package relies heavily on the Fusap.Common.Model libraries. Please be sure that you are familiar with them before proceeding:

## Installation

Install the Fusap Mediator + Fluent validation packages:

```powershell
Install-Package Fusap.Common.Mediator
Install-Package Fusap.Common.Mediator.FluentValidation
```

Configure services:

```csharp
// Use this shortcut to get a list of all assemblies that have been loaded and that start with "Fusap.*"
var fusapAssemblies = AppDomain.CurrentDomain.GetFusapLoadedAssemblies();

// You can optionally register all mapping profiles at this point.
services.AddAutoMapper(fusapAssemblies);

// Add FusapMediator
services.AddFusapMediator(fusapAssemblies, opt => opt
    // Enable the validation middleware
    .UseValidation()
);
```

The Fusap Mediator is fully compatible with the tradicional method of using [Mediatr](https://github.com/jbogard/MediatR) to allow the teams to start using it in previous projects, mixing legacy and new code.

### Create a new request

To create a new request, simply make a class inherit from `Fusap.Common.Mediator.Request` or `Fusap.Common.Mediator.Request<TValue>` like so:

```csharp
public class CreateBatchAssociationsRequest : Request<CreateBatchAssociationsResult>
{
    public Guid LegalPersonId { get; set; }
    public Guid UserId { get; set; }
    public IEnumerable<AssociationData> Lines { get; set; } = default!;
}
```

Please note:

* The request class only carries state. All validations must be delegated to validator classes.
* You can have several validator classes for a single request class. This allows you to separate validation rules based on any factor you wish and to also keep files small, easy to test and easy to version.
* All validator classes are applied and all must pass before the request is handled.
* If you are using `Request<TValue>`, `TValue` must be the success-case specific type. All requests have [Fusap.Common.Model](https://dev.azure.com/Fusapdigital-HoldCo/global-platform/_git/arq-lib-common-model) built in, so all request handlers can automatically return either an `Error` or a success of `TValue`.

### Create a new request validator

Validator classes can have dependencies such as database connections or other services, including other validator classes. They can also mix both sync and async validations so you can make a database or cache check without worrying about sync-over-async.

**Attention:** Please be aware that the validators were designed to check for consistency on the requests, this means that all validation errors will be treated like `BadRequest`. All business rules must be coded as such inside the handlers themselves.

```csharp
// All validator classes inherit from AbstractValidator<T>
public class CreateBatchAssociationsValidator : AbstractValidator<CreateBatchAssociationsRequest>
{
    // The constructor can have dependencies injected to be able to handle complex validations
    // The dependencies can also include other validators so you can compose complex validation
    // scenarios from basic ones, reusing as much as possible.
    public CreateBatchAssociationsValidator(IConfigurationRepository configurationRepository,
        IValidator<AssociationData> associationDataValidator)
    {
        RuleFor(x => x.LegalPersonId)
            // Rules can be async or sync
            .MustAsync(async (x, cancellationToken) => await configurationRepository.GetAsync(x) != null)
            // Seamless integration with ErrorCatalog
            .WithErrorCatalog(ErrorCatalog.Configurations.NotFound);

        RuleFor(x => x.Lines)
            .NotEmpty().WithErrorCatalog(ErrorCatalog.Associations.InvalidBatch);

        RuleForEach(x => x.Lines)
            // Forwarding validation to a separate validator
            .SetValidator(associationDataValidator);
    }
}
```

Please check the [Fluent Validation Documentation](https://fluentvalidation.net/) for more tips on how to create more complex validations.

### Create a new request handler

To create a new handler, simply inherit from `Fusap.Common.Mediator.Handler<TRequest>` or `Fusap.Common.Mediator.Handler<TRequest, TValue>`.

Two main points here:

* The Fusap Mediator pipeline, when configured with FluentValidation, has a fail-fast approach. This means that the request handler will only be executed if all validations pass first, so our handlers can focus on the happy paths only.

* The base implementation has error handling built in, so there is no need for a global try-catch block. Any errors are automatically handled. You should only add a try-catch block if you intend to invoke a specific flow or fallback, not for logging nor tracing.

Sample handler:

```csharp
public class CreateBatchAssociationsHandler : Handler<CreateBatchAssociationsRequest, CreateBatchAssociationsResult>
{
    private readonly IMapper _mapper;
    private readonly IPublisher _publisher;

    public CreateBatchAssociationsHandler(IMapper mapper, IPublisher publisher)
    {
        _mapper = mapper;
        _publisher = publisher;
    }

    // Handle function only deals with happy-path
    public override async Task<Result<CreateBatchAssociationsResult>> Handle(CreateBatchAssociationsRequest request, CancellationToken cancellationToken)
    {
        // All validations need to pass before reaching this point.


        // The handler should perform the checks to ensure that the target entities exist
        if (!doesCompanyExist)
        {
            // Not found errors will be rendered as 404 responses
            return NotFound(ErrorCatalog.Configurations.NotFound);
        }

        // The handler should perform the business rules checks
        if (!personExist) {
            // Business rule violation errors will be redered as 422 responses
            return BusinessRuleViolated(ErrorCatalog.Associations.PersonDoesNotExist);
        }
        if (!limitApproved) {
            return BusinessRuleViolated(ErrorCatalog.Associations.TooManyAssociations);
        }


        // If all is good, do the work, then return the success result
        // Success responses are rendered as 2xx according with the API Controller
        // Check the readme on https://dev.azure.com/Fusapdigital-HoldCo/global-platform/_git/arq-lib-common-model-presenter for more information
        return Success(new CreateBatchAssociationsResult { BatchId = batchId });
    }
}
```

## Writing tests

### Testing the validator

Since request validation is handled by a validator class, it can easily be tested, like such:

```csharp
public class CreateAssociationRequestValidatorTests
{
    [Fact]
    public void Validate_WithInvalidFields_ReturnsValidationErrors()
    {
        // Arrange
        var data = new CreateBatchAssociationsRequest
        {
            LegalPersonId = Guid.Empty,
            Lines = Array.Empty<AssociationData>()
        };

        var configRepository = new Mock<IConfigurationRepository>(MockBehavior.Strict);
        configRepository
            .Setup(x => x.GetAsync(It.Is<Guid>(x => x == Guid.Empty)))
            .ReturnsAsync((Configuration)null);

        var associationDataValidator = new AssociationDataValidator();

        var sut = new CreateBatchAssociationsValidator(configRepository.Object, associationDataValidator);

        // Act
        var result = sut.TestValidate(data);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.LegalPersonId)
            .WithErrorCode(ErrorCatalog.Configurations.NotFound);
        result.ShouldHaveValidationErrorFor(x => x.Lines)
            .WithErrorCode(ErrorCatalog.Associations.InvalidBatch);
    }

    // ...
}
```

### Testing the handler

The same is valid for the handler:

```csharp
public class CreateBatchAssociationsHandlerTests
{
    [Fact]
    public async Task Handle_WithThreeLines_PublishesThreeCommands()
    {
        // Arrange

        var legalPersonId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var request = new CreateBatchAssociationsRequest
        {
            LegalPersonId = legalPersonId,
            UserId = userId,
            Lines = new List<AssociationData>
            {
                new AssociationData(),
                new AssociationData(),
                new AssociationData(),
            }
        };

        var publisher = new Mock<IPublisher>(MockBehavior.Strict);
        publisher
            .Setup(x => x.PublishAsync(It.Is<IEnumerable<object>>(x =>
                x.Count() == 3 && x.Select(y => (CreateAssociationCommand)y).All(y =>
                    y.UserId == userId && y.LegalPersonId == legalPersonId))))
            .Returns(Task.CompletedTask);

        var sut = new CreateBatchAssociationsHandler(MapperUtil.Create(), publisher.Object);

        // Act
        var result = await sut.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful());
    }

    // ...
}
```

To make tests easier to write, we have a simple class that creates a valid mapper wherever it is needed:

```csharp
public static class MapperUtil
{
    public static IMapper Create()
    {
        return new ServiceCollection()
            .AddAutoMapper(AppDomain.CurrentDomain.GetFusapLoadedAssemblies())
            .BuildServiceProvider()
            .GetRequiredService<IMapper>();
    }
}
```

## Built in tracing and logging middlewares

This package comes with built in tracing and logging features to register when a request starts and finishes processing. Exception details are automatically included so there is really no need for try-catch blocks just for logging.

Logging example:

```
[10:26:04 INF] Handling request SearchAssociationsRequest started
[10:26:04 INF] Handling request SearchAssociationsRequest finished in 105ms successfully
```

Tracing example:

![Tracing sample](docs/tracing.jpg)

Here you can see the time it took for execution the middlewares and for the handler itself. The middlewares also detail the context filters and the time it takes for each validator to run.

## Advanced scenarios

### Providing context to the validators

Sometimes you need to create a validation that requires more information than the request carries itself. This extra information can come from a database for example.

To handle this scenario, you can create an `IValidationContextFilter`.

Example: We need to validate a request to check if the document type and number match what is configured for a given country, but the country information is not provided by the user, it needs to be loaded from the source of truth.

So we first create a marker interface that will indicate that this particular request needs to have the country looked up before the validation runs:

```csharp
public interface IHasCountryBasedValidation
{
    public Guid LegalPersonId { get; set; }
}
```

Then we create an `IValidationContextFilter` to extract the legal person id, load the country information and save it on the validation context:

```csharp
public class CountryBasedValidationContextFilter : IValidationContextFilter
{
    private readonly IConfigurationRepository _configurationRepository;
    private readonly ICountryInfoProvider _countryInfoProvider;

    public CountryBasedValidationContextFilter(IConfigurationRepository configurationRepository, ICountryInfoProvider countryInfoProvider)
    {
        _configurationRepository = configurationRepository;
        _countryInfoProvider = countryInfoProvider;
    }

    public async Task ApplyAsync(IValidationContext context, CancellationToken cancellationToken)
    {
        // Check if the request implements the interface
        if (!(context.InstanceToValidate is IHasCountryBasedValidation request))
        {
            return;
        }

        // Load the configuration for the given legal person
        var configuration = await _configurationRepository.GetAsync(request.LegalPersonId);

        if (configuration == null)
        {
            return;
        }

        // Load the country for the given country iso code
        var countryInfo = _countryInfoProvider.GetCountryInfo(configuration.CountryIsoCode);

        // Set the country information to the validation context
        context.SetData(countryInfo);
    }
}
```

This way, your validator can use information about the country without having to load it itself. It might seem like a waste, but the big performance improvement here is that the country information is loaded only once and it is made available for all validators. Keep in mind that a single request can use several validators (like for validating all elements in an array).

To use the context information is easy:

```csharp
public class AssociationDataValidator : AbstractValidator<AssociationData>
{
    public AssociationDataValidator(IEnumerable<IDocumentValidator> documentValidators)
    {
        RuleFor(x => x.DocumentTypeCode)
            .NotEmpty().WithErrorCatalog(ErrorCatalog.Associations.AssociationData.InvalidDocumentTypeCode)
            .Must((target, value, context) => // Use the overload that gives access to the context
            {
                var documentInfo = context
                    .GetData<CountryInfo>()? // extracts the needed data from the context
                    .DocumentTypes
                    .FirstOrDefault(d => d.DocumentTypeCode == target.DocumentTypeCode);

                return documentInfo != null;
            })
            .WithErrorCatalog(ErrorCatalog.Associations.AssociationData.InvalidDocumentTypeCode);

        RuleFor(x => x.DocumentNumber)
            .NotEmpty().WithErrorCatalog(ErrorCatalog.Associations.AssociationData.InvalidDocumentNumber)
            .Must((target, value, context) =>   // Use the overload that gives access to the context
            {
                // extracts the needed data from the context
                var countryIsoCode = context.GetData<CountryInfo>().CountryIsoCode;

                var result = documentValidators
                    .Where(v => v.CanHandle(countryIsoCode, target.DocumentTypeCode))
                    .All(v => v.IsValid(countryIsoCode, target.DocumentTypeCode, target.DocumentNumber));

                return result;
            })
            .WithErrorCatalog(ErrorCatalog.Associations.AssociationData.InvalidDocumentNumber);
    }
}
```

There is no need for service registration because if you are following this pattern, all related interfaces will be found and registered automagically.

### Several requests that share the same validation logic

If you have several request classes that all need to have the same validations, you should create an interface with the common properties that need to be validated and create a validator against that interface.

Example: Several requests need to validate if the legal person id has been informed.

Create a interface to expose the required property:

```csharp
public interface ILegalPersonRequest
{
    public Guid LegalPersonId { get; set; }
}
```

Then create a validator for the interface:

```csharp
public class LegalPersonRequestValidator : AbstractValidator<ILegalPersonRequest>
{
    public LegalPersonRequestValidator()
    {
        RuleFor(x => x.LegalPersonId)
            .NotEmpty().WithErrorCatalog(ErrorCatalog.InvalidLegalPersonId);
    }
}
```

Now you can test just this validator and you can apply this validation to all requests that implement the interface.

The final step is to register this interface-based validator:

```csharp
var fusapAssemblies = AppDomain.CurrentDomain.GetFusapLoadedAssemblies();

services.AddValidatorForInterface<LegalPersonRequestValidator, ILegalPersonRequest>(fusapAssemblies);
```

**PLEASE** prefer to use interface-based validators for shared validations instead of inheritance because this can easily be composible and because it keeps the coupling very low.

## Authentication

Make sure that you have read the readme for [Fusap.Common.Authorization](https://dev.azure.com/Fusapdigital-HoldCo/global-platform/_git/arq-lib-common-authorization) as this section is written assuming that all auth-related terms are known.

The Fusap Mediator Authorization middleware was designed to introduce AspNetMVC-like authorization semantics to the application layer.

The `Fusap.Common.Mediator.Authorization.IAuthorizationRequirementsDescriptor` is what glues everything together. The main purpose of this interface is to inform the middleware what are the security requirements for a given mediator request.

This is how it works:

* First a mediator request is created and `await _mediator.Send(request)` is called.
* The `AuthorizationMiddleware` intercepts this request and enumerate all `IAuthorizationRequirementsDescriptor` registered.
* It then call `IAuthorizationRequirementsDescriptor.DescribeRequirements(request)` on each one to produce a list of security requirements.
* With the requirements in hand, it uses the `IFusapAuthorizationClient` to validate if the user is authorized or not.
* If any of the requirements are not met, the request will fail with `NotAuthorized`.

### How to use

Install the authorization package:

```powershell
Install-Package Fusap.Common.Mediator.Authorization
```

Define an interface that all requests that need authorization will implement:

```csharp
public interface ILegalPersonAuthenticatedRequest
{
    public Guid UserId { get; } // Identifies the user logged in
    public Guid LegalPersonId { get; } // Identifies the legal person where the action is being executed
}
```

Create a permission catalog to keep things organized, this can be added to the root of the Application project:

```csharp
public static class PermissionCatalog
{
    public static class Configurations
    {
        public const string Update = "lp-ms-associate-configuration-update";
    }

    public static class Associations
    {
        public const string View = "lp-ms-associate-association-view";
        public const string Create = "lp-ms-associate-association-create";
        public const string CreateBatch = "lp-ms-associate-association-create-batch";
        public const string Remove = "lp-ms-associate-association-remove";
    }

    // ...
}
```

With that ready, implement the interface in a request that needs authorization using the `Authorize` attribute detailing the permissions needed together with the interface to tell the middleware how to extract the resource and the identity:

```csharp
[Authorize(PermissionCatalog.Associations.Create, PermissionCatalog.Associations.CreateBatch)]
public class CreateBatchAssociationsRequest : Request<CreateBatchAssociationsResult>,
    ILegalPersonAuthenticatedRequest
{
    public Guid LegalPersonId { get; set; }
    public Guid UserId { get; set; }
    public IEnumerable<AssociationData> Lines { get; set; } = default!;
}
```

The next step is to define the requirements descriptor to teach the middleware how to extract security requirements from the requests:

```csharp
public class LegalPersonRequirementsDescriptor : AuthorizationRequirementsDescriptor<ILegalPersonAuthenticatedRequest>
{
    protected override IEnumerable<Requirement> DescribeRequirements(ILegalPersonAuthenticatedRequest request)
    {
        // Extract the actions attribute for the request
        var actions = AuthorizeAttribute.ActionsFor(request.GetType());

        // Validate that there are actions defined
        if (actions == null)
        {
            throw new InvalidOperationException(
                $"The request of type {request.GetType()} implements ILegalPersonAuthenticatedRequest but has not specified actions through AuthorizeAttribute.");
        }

        // Extract the person who is trying to execute the request
        var identity = FusapResources.Person(request.UserId);

        // Extract the legal person against which the request is being executed
        var resource = FusapLegalResources.LegalPerson(request.LegalPersonId);

        // Produce a requirement for the user (identity) to take the given (actions) against the legal person (resource).
        return new[] { new Requirement(identity, resource, actions) };
    }
}
```

When registering the mediator service, you need to enable authorization on the pipeline:

```csharp
services.AddFusapMediator(fusapAssemblies, opt => opt
    .UseAuthorization()     //                             <<<<<<<<<<<<<<   Enable authorization

    // Other middlewares
    .UseValidation()
);
```

And that is it.
