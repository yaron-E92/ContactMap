# ContactMap

A cross-platform, event-driven contact sharing and mapping application using Clean Architecture, DDD, CQRS, and Yaref92.Events for event aggregation.

## Solution Structure

- **ContactMap.Domain**: Core domain entities, value objects, events, and repositories.
- **ContactMap.Application**: CQRS commands, command handlers, use cases, event subscribers, and DI configuration.
- **ContactMap.Infrastructure**: EF Core persistence, repository implementations, and DbContext.
- **ContactMap.WebApi**: ASP.NET Core Web API for all backend operations.
- **ContactMap.Presentation.Maui**: .NET MAUI cross-platform UI (mobile/desktop).
- **Test Projects**: Unit and integration tests for each layer.

## Key Technologies

- **CQRS**: Commands and command handlers for all write operations.
- **Event Aggregation**: Yaref92.Events for domain event publishing and async subscribers.
- **EF Core**: SQLite persistence (can be swapped for PostgreSQL or others).
- **.NET MAUI**: Modern, cross-platform UI.

## Event-Driven Flow

- Domain events (e.g., `RelationshipRequestedEvent`, `RelationshipApprovedEvent`) are raised and published via Yaref92.Events.
- Subscribers (e.g., `RelationshipSubscriber`) handle these events asynchronously for side effects (notifications, etc).

## How to Run

1. **Restore NuGet packages**: `dotnet restore`
2. **Apply EF Core migrations** (if needed): `dotnet ef database update` in the Infrastructure project.
3. **Run the Web API**: `dotnet run --project ContactMap.WebApi`
4. **Run the MAUI app**: `dotnet build ContactMap.Presentation.Maui` then deploy to your platform.
5. **Test endpoints**: Use Swagger, Postman, or the MAUI app to interact with the API.

## Extending the App

- Add new commands/events for new business flows.
- Add new event subscribers for integrations, notifications, etc.
- Swap out SQLite for a central DB if needed.

## License

GPL-3.0 (see Yaref92.Events and this repo)
