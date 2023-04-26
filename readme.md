# Onion Architecture with Microsoft Identity - Template

## Purpose
This is a sample project to demonstrate how to implement the Onion Architecture with Microsoft Identity, 
Entity Framework Core 6 and how to implement a Test Project.

A Sample Api is also included, some of the Methods are not
implemented because the focus of this project is to demonstrate the Onion Architecture.

## Used Packages
* Microsoft.AspNetCore.Identity.EntityFrameworkCore
* Microsoft.EntityFrameworkCore
* Microsoft.EntityFrameworkCore.SqlServer
* Microsoft.EntityFrameworkCore.Tools
* Microsoft.AspNetCore.Authentication.JwtBearer
* Bogus
* Mailkit
* Mapster
* Nunit
* Moq
* Swashbuckle.AspNetCore

## Used Tools
* Jetbrains Rider

## How to run the project
1. Clone the project to your local machine.
2. Open the project in your IDE
3. Open the Package Manager Console and run the following command: `Update-Database`
4. Run the WEB Project project.

## Projects
* Contracts: Contains the Dtos and used Models over the whole solution
* Domain: Contains the Domain Models, Exceptions and IRepositories
* Services: Contains the Business Logic
* Services.Abstractions: Contains the Interfaces for the Business Logic
* Persistance: Contains the DbContext and the Migrations
* Presentation: Contains the Controllers
* Web: Contains the API Project
* Tests: Contains the Test Project

## Design Decisions
There is not one Design of the Onion Architecture. There are multiple ways to design
the Onion Architecture. You can still rearrange the Solution structure but you should
be aware of the dependencies between the projects.

### Repository
Its up to you if you want to use a Repository or not. If you are sure that
you will never change the Database Provider you can use the DbContext directly in the Services.
EfCore itself is already a Repository and Unit of Work Pattern.

## Testing
The Test Project is using the NUnit Framework and the InMemory Database Provider.
The InMemory Database Provider is not a real Database, it is just a fake Database.

This is not suggested in a real project. You should use a real Database Provider,
to be sure that your queries are working as expected. Therefor often a copy of the 
production database is used.