
# API and Databases
API forms the layer between our front-end and database. This is where all the heavy logic is processed when a request is sent from the front-end.

## 1. before you Start
- Visual Studio 2019
- .NET Core 3.1
- Azure Account with active subscription. See Azure for Student folder for instruction
- Git
## 1.1 Learning Outcomes
- Creating a database hosted on Azure
- Using Code First programming to design our system (If you are interested you can see NZMSA 2019 for the database first approach)
	- Add tables remotely through code without SQL
	- Update database to reflect model in code withour SQL
- Creating an API that performs CRUD operations.
- Understanding the interactions with API and Databases

1.2 Context
So what is an API and why do we need it? Why does it exist separate from the database. How is it helpful to have an API?

# 2. Model

Before we even start to write a line of code, we need to think about what we would like to store in our database and what properties we want our API to return. This is crucial because the cost of modifying an existing database is very high. To keep it simple we will only have one table and one model. Be aware that in modern system that multiple models can exist that pull data from multiple databases.

Now that we have the information out of the way we can design our model. Here we are going to create a student database.

Our model will have the following fields
- StudentId
- First Name
- Last Name
- Email Address

Aside from variable name, we also need to consider their types. For example, it is normal to store StudentId as an integer, but it doesn't make sense to only allow numbers to be stored in the name.

You can read more about data types on [https://www.w3schools.com/sql/sql_datatypes.asp]([https://www.w3schools.com/sql/sql_datatypes.asp](https://www.w3schools.com/sql/sql_datatypes.asp))

# 3. Azure Database
Before we can employ code first db we first need to create our Database server. We can create one online using Azure

Make sure you have an active subscription, navigate to https://portal.azure.com on your browser and click “Create a resource” and search for “SQL Database” and click it. You should get the follow screen

Make sure the subscription is Azure for Students, give the database server a name and create new resource group for it, Click Create new. (This is the resource group which is basically like a folder) Give your database a name. Click create new and enter the details for the admin account for this db

Select location as aus east (doesn’t really matter but keep it close because lower latency)

There are many options for configuring your database but we will use basic for now (It cost a lot to host these other options)  as can see by the estimation calculator

Click review and create And your deployment should be underway. This might take some time.

Click on go to resource and find the Query Editor on the left panel.

Log in and if you get this error click on the set server firewall link. Make sure Allow Azure service is Yes and add the rule 0.0.0.0 amd 255.255.255.255. this is giving all IP addresses access. Ideally we would want to restrict access.

On the left hand panel find the label Connection String and copy the string under ado.net.

# 4. Time to Code – Model and context creations

<![if !supportLists]>- <![endif]>Open Visual Studio 2019 -> Create a new Project -> ASP.NET Core Web Application

<![if !supportLists]>- <![endif]>Give your project a name

<![if !supportLists]>- <![endif]>Select API -> Click CrreateCreate

At this point you can click IIS Express to run the project. The newly created API project comes with a default API WeatherForecastController.cs, It should show you the follow data, (I have JSON formatter so it will look different)

Delete the WeatherForecast.cs and the WeatherForecastController.cs in the solution explorer on the left hand side

Got to Tools -> Nuget Package Manager -> Manage Nuget Package for Solution.

Click browse and search for Microsoft.EntityFrameworkCore

Add Microsoft.EntityFrameworkCore and install it. Accept the agreements. Do the same with Microsoft.EntityFrameworkCore.Tools and Microsoft.EntityFrameworkCore.SqlServer.

Right click your project and create a new folder called Models

Right click the Models folder and select add new item

Make sure class is selected and give it a name. This class will be our data model for our students data.

Under the Student class add the follow code. [code]. You will give visual studio complaining about an error. Hover or click the lightbulb icon click show potential fixes and select using [using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;]

This imports the libraries that we added in the Nuget packages. We must do this so that the program knows where to look.

[Key] denoted the id that is inserted into the database. This annotation isn’t strictly needed if your variable has Id in the name however I will do it anyway. [DatabaseGenerated(DatabaseGeneratedOption.Identity)] annotation tells the database that we want this to be autogenerated and is an identifyer. Read more here for other annotations which allow you more control over the column in your database.

We are now done with our basic model

Right click the project and create a new folder called Data. In this new folder create a new class called StudentContext, Add the follow code to the file and fix all the errors using the suggestions.

# 5. Time to Code – Migrations

The next step is to update our remote database with the information from our code. It will create a table that fits our model. First remember we had the connection previously from Azure. Open appsettings.json and add the following where CONNECTIONSTRING is the string you copied earlier. Make sure you  replace {your_password} with your admin password

At the bottom of the screen Click package console manager and run the follow command add-migration InitialCreate. This creates a migration folder and code. This is basically git but for our model. We haven’t updated the remote database but running Update-Database will create the model on our database

Go back to azure and find your database and select the query editor, log in and expand the tables folder. You can see 2 tables. 1 is a record of the migrations and the other is your model. You have successfully update database using code first approach. If you want to know how to do database first take a look at the 2019 msa phase 1 API and databases.

# 5. Time to Code – Controllers

The controller is where all our api’s are created. To create basic API we will use scaffolding which will give us some API that is automatically created.

Go to Startup.cs and add the follow line to ConfigureServices()

var connection = Configuration.GetConnectionString("schoolSIMSConnection");

services.AddDbContext<StudentSIMSContext>(options => options.UseSqlServer(connection));

Replace the context with yours. What we are doing is letting our program know that we want to user the context we have created

Right click the controllers folder and select add->add new scaffold item-> slect Add api controller with actions, using entity framework. Here select your model and context you create previously.

It should generate the API for you. This is very basic api but it will give us the some boilder plate code to work with.

You can run then program again but go to one of the api/Students. It will return a empty list. But we know that it has successfully accessed our database.

This isn’t very visual pleasing to work with so we will add swagger in the next step.

# 5. Time to Code – Swagger.

Install the nuget package Swashbuckle.AspNetCore

Add the following code to Startup.cs in the Confgure service method

services.AddSwaggerGen(c =>

{

c.SwaggerDoc("v1", new OpenApiInfo { Title = "StudentSIMS", Version = "v1" });

});

And fix the errors add the following to Configure

app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),

// specifying the Swagger JSON endpoint.

app.UseSwaggerUI(c =>

{

c.SwaggerEndpoint("/swagger/v1/swagger.json", "My first API V1");

c.RoutePrefix = string.Empty; // launch swagger from root

});

In launchsetting.json edit the launchUrl to be “”

Run the program.

Your should now be greeted with a nice ui

Time to see if our api works.

Click on POST api/Students and click try it out

POST is an HTTP method used to send data to a server to create/update a resource

Edit the string so that it has a first and last name and email.

And click execute if the response is 201 then we have successfully added some data to our database.

We can check this if we execute our get api/students

# 5. Time to Code – Updating our model

If your model needs to change we can simply add it to our existing model. I will add a timestamp and phone and middle name. I will also make it a so that the first and lastname are required fields and firstname has a maxlenth allowable.

Got to package manger console and call add-migration UpdatedStudentModel and update-database.

If made a mistake with the model you can call roll back the migration by calling Update-Database witht ehe name of the update before. Take a look at for more functions [https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=vs#remove-a-migration](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=vs#remove-a-migration)
 
