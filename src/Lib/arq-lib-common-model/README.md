# Fusap.Common.Model

Common constructs to represent output of processes like success and error results, custom non-exception based error details, error catalogs and pagination.

## Success and error results

This library centers around these two interfaces. Both are to be used to indicate the output of a given process. This output can hold a value in case of success or can hold an error in case of failure.

Methods that return `IResult` or `IResult<T>` are not expected to throw exceptions. Exceptions should instead be wrapped inside a failure result.

There are two concrete types with several shortcuts to make working with `IResult`s easier, they are `Result` and `Result<T>`.

```csharp
// Produces an IResult
var result1 = Result.Success();

// Produces an IResult<string> with value = "test"
var result2 = Result.Success("test");

// Produces an IResult with error
var result3 = Result.Failure(new Error("ERR-01", "Error message"));
```

If you are working on a context where an exception is expected, for instance, inside a consumer, you can ensure that the result was successful like so:

```csharp
// Will throw FailedResultException if the result is not successful.
result.EnsureSuccess();
```

You can check if a result was successful or not by checking if there is an error or by invoking `IsSuccessful`:

```csharp
// Checking if result is successful
var successful1 = result.IsSuccessful();

// Checking if result has no error
var successful2 = result.Error == null;
```

## Error description

This library introduces a new type to describe an error: `Error`.

There are several built in error types to cover common scenarios:

* `BusinessRuleViolatedError`: indicates that one or more business rules were violated.
* `ConflictError`: the action could not be completed due to conflicting information.
* `NotAuthorizedError`: access to the feature was denied.
* `NotFoundError`: a required resource was not found.
* `UnexpectedError`: an unexpected exception was caught.
* `ValidationError`: indicates that an input had invalid data.

New types can be derived from the base `Error` to convey domain-specific semantics as each team sees fit.

Errors are instantiated with a code and a message or can be optionally be instantiated
with an `ErrorCatalogEntry` like so:

```csharp
// Using specific code and message
var error1 = new NotFoundError("CUS-01", "Customer not found");

// Using error catalog
var error2 = new NotFoundError(ErrorCatalog.InvalidAmount);
// Produces an error with code "RN-02" and message "The amount is invalid"

// Error catalog entries can be formatted to allow further customization:
var error3 = new NotFoundError(ErrorCatalog.NotFound.Format("1234"));
// Produces an error with code "RN-01" and message "The customer was not found: 1234"

```

### Error catalogs

All error codes and messages can be declared following this pattern:

```csharp
public static class ErrorCatalog
{
    public static ErrorCatalogEntry NotFound => ("RN-01", "The customer was not found: {0}");
    public static ErrorCatalogEntry InvalidAmount => ("RN-02", "The amount is invalid");

    public static class SubDomain
    {
        public static ErrorCatalogEntry ErrorInSubDomain => ("RN-S1-01", "Random error description");
    }
}
```

The error catalog must be static and must contain only static properties of type `ErrorCatalogEntry`.
These properties must have a get that returns the specific catalog entry.

#### Error validation

You can validate your error catalog on a unit test, this will ensure that all entries
have unique error codes and that all declarations are as they are expected.

As a byproduct, you can render the catalog as an `.md` file and put that in your project's
`doc` folder.

Install the package:

```powershell
Install-Package ApprovalTests
```

Add the sample test:

```csharp
[UseReporter(typeof(ClipboardReporter), typeof(DiffReporter))]
public sealed class ErrorCatalogApprovalTests
{
    [Fact]
    public void ErrorCatalog()
    {
        // Arrange

        // Act
        var catalogDescription = ErrorCatalogDescription.For(typeof(ErrorCatalog)).SerializeAsMarkdown();

        // Assert
        Approvals.Verify(catalogDescription);
    }
}
```

The above ErrorCatalog will produce the following markdown definition for the approval test:

```markdown
# Error catalog

| Code                           | Key                            | Message                                                      |
|--------------------------------|--------------------------------|--------------------------------------------------------------|
| RN-01                          | NotFound                       | The customer was not found: {0}                              |
| RN-02                          | InvalidAmount                  | The amount is invalid                                        |


## SubDomain

| Code                           | Key                            | Message                                                      |
|--------------------------------|--------------------------------|--------------------------------------------------------------|
| RN-S1-01                       | ErrorInSubDomain               | Random error description                                     |
```

You can then use the following target on the `csproj` file for your test project:

```xml
<Target Name="CopyApprovedErrorCatalogToDocsFolder" AfterTargets="PostBuildEvent">
    <Copy SourceFiles="$(ProjectDir)ApprovalTests\ErrorCatalogApprovalTests.ErrorCatalog.approved.txt" DestinationFiles="$(ProjectDir)..\..\docs\error-catalog.md" ContinueOnError="true" />
</Target>
```

## Pagination

There are multiple pagination strategies:

* Offset pagination: This strategy is the simplest to implement and is achieved using the commands like `offset` and `limit` in a database. Example: `GET /items/?offset=0&limit=15`

* Seek pagination: This strategy is similar to the offset pagination, however instead of specifying an offset, it is specified an identifier like `GET /items?limit=20&after_id=40`

* Cursor pagination: This strategy is the most advanced. With the initial query result it is returned a token that needs to be passed to get the next set of results. Example `GET /items/?cursor=hdfiuhi3we2143ewd`

The pagination model here can handle these 3 cases with the interface `IPagination<TItem, TNext>`. It has 3 properties: `Items`, `Next` and `EstimatedCount`.

For the general case where `TNext` is `long` you can use the simpler `IPagination<TItem>`.

There are also concrete implementations for these interfaces.

