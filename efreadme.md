# Entity Framework Core Setup for ProgrammersToolKit

## Initial Migration

1. Open a terminal in the `ProgrammersToolKit/` project directory.
2. Run the following command to add the initial migration:

```
dotnet ef migrations add InitialCreate --project ProgrammersToolKit/ProgrammersToolKit.csproj
```

## Apply Migrations

To apply migrations and update the SQLite database, run:

```
dotnet ef database update --project ProgrammersToolKit/ProgrammersToolKit.csproj
```

## Notes
- The SQLite database is stored in the user's AppData folder under `ProgrammersToolKit`.
- Migrations are also applied automatically on app startup.
- You can add new migrations as you update your models:

```
dotnet ef migrations add <MigrationName> --project ProgrammersToolKit/ProgrammersToolKit.csproj
```

- To remove the last migration:

```
dotnet ef migrations remove --project ProgrammersToolKit/ProgrammersToolKit.csproj
```

- For more EF Core CLI commands, see: https://docs.microsoft.com/ef/core/cli/dotnet
