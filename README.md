# TektonApp
 Tekton Challenge


A few considerations for the repo.

- Technology used is .NET 6.0
- The Db is reproduced with Entity Framework and the migration is created already, only update-database for the db recreation using SQL Server EXPRESS.
- Software architecture used is DDD using repository/service pattern.
- Swagger is implemented as middleware.
- To run locally only replace the connection string in appsettings.json with a local SQL EXPRESS server, then run update-database to create the database and run locally.
- Mockapi.io has 50 id's for the GET
- Serilog was used to store json of logs.
