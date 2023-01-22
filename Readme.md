# Fast Endpoints + Entity Framwork + RabbitMQ example

This is an example for a .net core 7 solution that contains two projects running FastEndpoints framwork for API

The first API is a Catalog service is CRUD service that stores prodcuts into SQLite datbase locally, it also has a rabbitMQ client that emit a message when a new product is added.

The product is validated using FluentValidation and Fast Endpoints will discover these validation and execute them


The second API is Email notification service which is an API that start consuming messages from rabbitMQ upon start, and instead of sending emails it will print to console when a new product is received.


## Running

get rabbit mq up and running  
`docker compose up`

running catalog service  
`cd CatalogService`  
`dotnet run`

running email notification service  
`cd EmailNotificationService`  
`dotnet run`


## Disclaimer

This is just an example which is not suitable for production.

