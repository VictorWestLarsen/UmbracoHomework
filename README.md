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

Start of with no Raffles entered, and new serialnumbers, you have to do this command "drop-database -Context -RaffleDbContext".
The only reason why the database is populated is for the purpose of showing the paging.

To see the current entries, you need to create a new user by clicking on the "Register" button, and then fillout the form. 
Once you have created a user, you can access the Entries tab in the menu, this will show you all the entries, and you can, delete, Edit and view Details.

