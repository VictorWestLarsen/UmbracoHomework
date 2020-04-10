# Umbraco Raffle
### Getting started:
Once you have downloaded the project, you can go ahead and run the solution, then you need to make sure you have the following packages:
- Microsoft.AspNetCore.Identity.EntityFrameworkCore
- Microsoft.AspNetCore.Indentity.UI
- Microsoft.EntityFrameworkCore.Design
- Microsoft.EntityFrameworkCore.InMemory
- Microsoft.EntityFrameworkCore.Sqlite
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.NET.Test.Sdk

You can do this by going to Visual Studio --> Tools --> NuGet Package Manager --> Manage NuGet Packages for solution.


The first time you run the application you the DBInitializer will create 100 valid Serialnumbers, these numbers will be written to a txt document in the "UmbracoRaffle" folder named "Serialnumbers.txt"

