# Fusap.Common.SqlServer

Features:

* Single package import and activation with single service registration.
* Standardized configuration options.
* `IFusapDatabase` allows the creation of fully configured `IDbConnections` optimzed either for reading or writing to the cluster.
* Shortcuts for easier DI consumption through injecting the `IDbConnection` or `IReadOnlyDbConnection`.
* OpenTracing support with annotations for common usage mistakes. 

## Installation

Install the package:
```
Install-Package Fusap.Common.SqlServer
```

Register the service:
```csharp
services.AddFusapSqlServer();
```

Make sure you have the appropriate settings configured:
```json
{
  "SqlServer": {
    "ActiveTracer": true,
    "ConnectionString": "...",
    "ReadOnlyConnectionString": "..."
  }
}
```

Note: The legacy `CQRSSettings:SqlServer` is also supported.

## Usage

The most common usage scenario is to inject `IDbConnection` or `IReadOnlyDbConnection` on the classes that you need database access.

Use the `IReadOnlyDbConnection` for cases where you are not going to issue queries that modify the database in any way. These queries would be routed to a read replica to spread the load improving access times.

```csharp
public class DbConnectionConsumerService
{
    private readonly IDbConnection _connection;
    private readonly IReadOnlyDbConnection _readOnlyConnection;

    public DbConnectionConsumerService(IDbConnection connection, IReadOnlyDbConnection readOnlyConnection)
    {
        _connection = connection;
        _readOnlyConnection = readOnlyConnection;
    }

    public async Task DoWorkAsync()
    {
        // Run queries on a read replica
        await _readOnlyConnection.QueryAsync("...");

        // Modify the write replica
        await _connection.QueryAsync("...");
    }
}
```

For scenarios where you want to parallelize work, you can get a dedicated `IDbConnection` from the pool by injecting the `IFusapDatabase` service.

```csharp
public class FusapDatabaseConsumerService
{
    private readonly IFusapDatabase _database;

    public FusapDatabaseConsumerService(IFusapDatabase database)
    {
        _database = database;
    }

    public async Task DoWorkAsync()
    {
        // Create task list
        var tasks = Enumerable.Range(0, 2)
            .Select(_ => ParallelWorkAsync())
            .ToArray();

        await Task.WhenAll(tasks);
    }

    public async Task ParallelWorkAsync()
    {
        // Be sure to dispose the created connections so they return to the pool.

        // Run queries on a read replica
        using (var readOnlyConnection = _database.CreateReadOnlyConnection())
        {
            await readOnlyConnection.QueryAsync("SELECT SERVERPROPERTY('productversion')");
        }

        // Modify the write replica
        using (var connection = _database.CreateConnection())
        {
            await connection.QueryAsync("SELECT SERVERPROPERTY('productversion')");
        }
    }
}
```

## OpenTracing taggig of common pitfalls

When this library is used with tracing enabled you will get a visual feedback indicating potential
performance issues when running sync queries or preparing a lot of statements among other checks.

![Usage warnings](usage-warnings.png)

## Migrating from legacy `Fusap.GlobalPlatorm.CQRS.*`

This library is compatible with the previous `Fusap.GlobalPlatorm.CQRS` with regards to usage, but has different interface and class names that in some cases refer to the original types instead of customizations.

To upgrade:

1. Remove the packages:
    * `Fusap.GlobalPlatform.Data.CQRS`
    * `Fusap.GlobalPlatform.Data.CQRS.SqlServer`
    * `AspNetCore.HealthChecks.SqlServer`

2. Install new package:
    ```
    Install-Package Fusap.Common.SqlServer
    ```

3. Fix service registration:
    ```csharp
    services.AddFusapSqlServer();
    ```
4. Remove shortcuts like:
    ```csharp
    services.AddTransient<IDbConnection>(b =>
    {
        return new SqlConnection(connectionString);
    });
    ```

5. Search all occurrences of `using Fusap.GlobalPlatform.Data.CQRS.Queries;` and replace them by `using System.Data;`

6. Search all occurrences of `using Fusap.GlobalPlatform.Data.CQRS.Commands;` and replace them by `using System.Data;`

7. Search all occurrences of `ICommandDbConnection` and replace them by `IDbConnection`;

8. Search all occurrences of `IQueryDbConnection` and replace them by `IReadOnlyDbConnection`;

At this point the solution should compile and all tests should pass, however to keep the code clean and consistent I strongly recommend that the variable names used before also be updated to the new counterparts:

9. `IQueryDbConnection _queryDbConnection` becomes `IReadOnlyDbConnection _readOnlyDbConnection`;

10. `ICommandDbConnection _commandDbConnection` becomes `IDbConnection _dbConnection`.

That is it!
