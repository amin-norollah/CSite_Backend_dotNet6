# CSite Backend

CSite is a sample open-source .Net 6 backend project using cutting-edge technologies.

It would be an interesting project to help beginners to learn how to use modern technologies, frameworks, and libraries in developing the .Net back-end.

This educational web API is built using .NET Core 6.0 and helps the student learn the following items:
- Build data-driven RESTful API
- Using MS SQL server database and Entity framework
- Using Unit of Work
- Exception handler and response wrapper using AutoWrapper
- Generic controllers and dependency injection 
- Managing controllers using helper
- Logging with Serilog
- API documentation using SwaggerUI
- Authentication and authorization using IdentityServer4
- Using DTOs and Authomapper
- Versioning

## How to install and run

To begin with this project please follow the below instructions:

- First of all, open the project with Visual Studio 2022 (.Net 6 is only supported in this version),
- Build the solution. In the meanwhile, the Nuget package manager will install all required packages, if not install them manually,
- Open the Nuget package manager console and run the "Update-Database" command in all main projects (CSite and CSite.Identity),
- Select all projects in multi startup and run it.
