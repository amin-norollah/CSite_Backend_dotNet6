# CSite Backend

It would be an interesting project to help my student to learn how to use modern technologies, framwork and libraries in developing .Net back-end.

This educational web API built using .NET Core 6.0 and help student to learn following items:
- Build data driven RESTful API
- Using MS SQL server database and Entity framework
- Using Unit of Work
- Exception handler and response wrapper using autowrapper
- Generic controllers and dependency injection 
- Managing controllers using helper
- Logging with Serilog
- API documentation using SwaggerUI
- Authentication and authorization using IdentityServer4
- Using DTOs and Authomapper
- Versioning

## How to install and run

To begin with this project please follow the below instraction:

- First of all, open the project with Visual Studio 2022 (.Net 6 only supported in this version),
- Build the solution. In the meanwhile, the Nuget package manager will install all required packages, if not install them manually,
- Open the Nuget package manager console and run "Update-Database" command in all main projects (CSite and CSite.Identity),
- Select all projects in multi startup and run it.
