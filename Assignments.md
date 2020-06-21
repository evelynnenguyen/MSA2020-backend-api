# Assignments for API + Database module
Students will need to submit a link to GitHub repository. All the screenshots and explanations should be put into the README.md file.
- Create a code-first API server with Azure SQL Database
  - Database:
    - Create another table named "Address" with attributes: StudentId, Street Number, Street, Suburb, City, Postcode and Country. The Student table would have one-to-many relationship with this table. Please assign appropriate datatype for each of the attributes.
    - Show SQL database through the Query editor (screenshots) for both tables with rows of example instances
  - API manipulate the created Azure Database using Code-First migration:
    - Create an API that add new address for a student using his/her StudentId.
    - Create an API that change the address of a student using his/her StudentId.
    - Screenshot of Swagger UI for each of the CRUD requests with header, body, and successful response status
- MS Learn:
  - Compulsory: [Create a web API with ASP.NET Core](https://docs.microsoft.com/en-us/learn/modules/build-web-api-net-core/?fbclid=IwAR0YijdrKtl3UUkQLVTUw3i6RTJbkxLte7RbZhD2aBPYvZva-Pp-_WRYbJM)
  - Optional: Choose 1 out of the following two modules:
    - [Provision an Azure SQL database to store application data](https://docs.microsoft.com/en-us/learn/modules/provision-azure-sql-db/?fbclid=IwAR0k7zN0rgLgISyDoSZP7l3Mm1nEUjUY9nJJS0TnVEPjdn78xzWThfJesLk)
    - [Develop and configure an ASP.NET application that queries an Azure SQL database](https://docs.microsoft.com/en-us/learn/modules/develop-app-that-queries-azure-sql/?fbclid=IwAR2j2JDWm8dfkpOV8T-QYu6M1VHw6cFgvRBYF03K_ZXUerX2HJ28O2OUWBo)
