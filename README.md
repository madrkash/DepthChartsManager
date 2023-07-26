# DepthChartsManager
A library that helps to manage and work with Depth Charts for teams in different sports.

## How to run the application
Clone the repo

Navigate to path `~\DepthChartsManager` and run the command `dotnet run --project ./src/DepthChartsManager.ConsoleApp/`

The application runs with some input data and displays the output on console. Press any key to quit.

## How to run the tests

Navigate to path `~\DepthChartsManager` and run the command `dotnet test` which will run both the unit and component tests.

## Core Technologies
1. Framework: .NET 6.0
2. Language: C#


## Implementation Details

### Architecture

I chose to use Clean / Onion Architecture as it makes the solution independent of

* Framework & anything external
* Client consuming it
* Infrastructure
* Easily testable due to loose coupling

### Engineering Principles
* Input validation using fluent validation
* Mediator pattern to handle the complexity of multiple objects communication
* Builder pattern for cleaner object construction
* Custom exceptions
* Entity based repository

### Unit testing
* Used *xUnit* as the Unit testing library along with Moq and FluentAssertions.
* Employed xUnit data driven testing where applicable with both InlineData and ClassData attributes employed.
* Healthy Unit tests coverage
* Component testing covering all use cases provided

### Few Constraints
* No persistence layer implemented.
* Without database, the behaviour in repository is implemented using in-memory collection objects.


## TODO
### If I had more time, I would have implemented the below.

* Create an API with Swagger UI
* Design database
* Implement ORM framework like Dapper in Repository.
* Containerise the app and database
* Create pipeline on Github Actions & Workflows
* Host the app on AWS

