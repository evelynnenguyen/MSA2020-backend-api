

# API and Databases
API forms the layer between our front-end and database. This is where all the heavy logic is processed when a request is sent from the front-end.

## 1. before you Start
- Visual Studio 2019
- .NET Core 3.1
- Azure Account with active subscription. See Azure for Student folder for instruction
- Git
### 1.1 Learning Outcomes
- Creating a database hosted on Azure
- Using Code First programming to design our system (If you are interested you can see NZMSA 2019 for the database first approach)
	- Add tables remotely through code without SQL
	- Update database to reflect model in code withour SQL
- Creating an API that performs CRUD operations.
- Understanding the interactions with API and Databases

### 1.2 Why API and Database
So what is an API and why do we need it? Why does it exist separate from the database. How is it helpful to have an API?

# 2. Model

Before we even start to write a line of code, we need to think about what we would like to store in our database and what properties we want our API to return. This is crucial because the cost of modifying an existing database is very high. To keep it simple we will only have one table and one model. Be aware that in modern system that multiple models can exist that pull data from multiple databases.

Now that we have the information out of the way we can design our model. Here we are going to create a database that holds student details. Our model will have the following fields:
- StudentId
- First Name
- Last Name
- Email Address

The model seems very simple but that is because we will update it later on in this tutorial.

# 3. Azure Database
Before we can build our model we first need a server to host our database. There are mutiple ways and technologies we could use to create a database. For the purpose of simplicity we will use Azure SQL databases to host our data. For this step you will need a Azure for Student Subscription and an account to go with it. Visit [https://azure.microsoft.com/en-us/free/students/](https://azure.microsoft.com/en-us/free/students/) to redeem your subscription. This will give you some free credits to use which is needed to host our API and Databases.
### 3.1 Creating the database
Navigate to https://portal.azure.com on your browser and click “Create a resource” and search for “SQL Database”.
![Create Resource](./img/Azure%20%281%29.png)
>Make sure the subscription is Azure for Students, 
- Click 'Create New', to create a new resource group. (The resource group is a collection of resources that are used for a particular application or group of applications) 
![Create Resource Group](./img/Azure%20%282%29.png)
- Give your database a name. 
![db name](./img/Azure%20%284%29.png)
- Click 'Create new' which will prompt you to create a admin account for this database and the select a region that it will hosted on.
![SQL admin](./img/Azure%20%285%29.png)
- You should have something similiar to this.
![settings](./img/Azure%20%286%29.png)
- Click 'Configure database' and navigate to the basic option (the default one is overkill and is quite expensive for our purposes) and Apply the changes
![config database](./img/Azure%20%287%29.png)
![config database](./img/Azure%20%288%29.png)
> We want to change the database configuration as the default one is expensive and overkill for our purposes (Cost $641 for me to host the default monthly)
![config database](./img/Azure%20%289%29.png)
> Way cheaper cost only $8 a month, be sure to delete the database after the phase 1 results are given out so that it doesn't eat into your credits.

Once satified with the setting you can click review and create and the deployment should be underway. This might take some time.
![Create Resource](./img/Azure%20%2810%29.png)
### 3.2 Firewall settings
When the database has finished being deployed you can click on "Go to resource" and 'Set server firewall' 
![Firewall](./img/Azure%20%2811%29.png)
![Firewall](./img/Azure%20%2812%29.png)
Change the seeting so that 'Allow Azure service' is Yes and add the rule 0.0.0.0 and 255.255.255.255. This is giving all IP addresses access. Ideally we would want to restrict access in a production environment but for simplicity I will allow all connections.
![Allow All](./img/Azure%20%2813%29.png)
On the left hand panel find the label Connection String and copy the string somewhere.
![Connection String](./img/Azure%20%2814%29.png)
Copy the connection string under ADO.NET
![Copy String](./img/Azure%20%2815%29.png)

# 4. Time to Code – Model and context creations
### 4.1 Creating a new web API project
Open Visual Studio 2019 -> Create a new Project -> ASP.NET Core Web Application
![Create app](./img/api%20%281%29.png)
Give your project a name
![project name](./img/api%20%282%29.png)
Select API -> Click Create
![Api](./img/api%20%283%29.png)
We have create an empty API project which will will create our core logic of our API. At this point you can Click 'IIS Express' to run the project. The newly created API project comes with a default API WeatherForecastController.cs, It should show you the following data
![run api](./img/api%20%284%29.png)
![output json](./img/api%20%285%29.png)
Now that we know the project runs we can delete the WeatherForecast.cs and the WeatherForecastController.cs by right clicking and selecting delete in the 'solution explorer' on the right hand side.
![delete default](./img/api%20%287%29.png)
### 4.2 Adding Nuget Packages
We also need to install some libraries/extensions to the project to help us create the API.
At the top of the screen go to "Tools -> Nuget Package Manager -> Manage Nuget Package for Solution".
![nuget manager](./img/api%20%288%29.png)
Click browse and search for Microsoft.EntityFrameworkCore, add Microsoft.EntityFrameworkCore and install it. 
![install packages](./img/api%20%289%29.png)
Do the same with:
- Microsoft.EntityFrameworkCore.Tools (Migration)
- Microsoft.EntityFrameworkCore.SqlServer (database communication).

### 4.3 Adding the Model
Add a folder called "Models" to the project by right clicking the project
![model](./img/api%20%2810%29.png)
Right click the Models folder and select add new item
![add item](./img/api%20%2811%29.png)
> Give your class file a name. This class will be our model for our student data.
Under the Student.cs class add the follow code. 

```C#
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int studentId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailAddress { get; set; }
    }
```
![model code](./img/api%20%2812%29.png)
You will see that Visual Studio complaining about an error. We can fix this by hovering over or clicking the lightbulb icon to show potential fixes. In our case we want to import the following.
- ```using System.ComponentModel.DataAnnotations;```
- ```using System.ComponentModel.DataAnnotations.Schema;```
![fix using snippet](./img/api%20%2813%29.png)
![fix using snippet](./img/api%20%2814%29.png)
This imports the libraries that we added in previously. 
> [Key] denotes the primary Id that is used to identify the row of data. This  annotation isn’t strictly needed if your variable has Id in the name.
> [DatabaseGenerated(DatabaseGeneratedOption.Identity)] annotation tells the database that we want this variable to be autogenerated and is the primary identifier. More infomration can be found [here](https://docs.microsoft.com/en-us/ef/core/modeling/entity-properties?tabs=data-annotations,without-nrt)

We are now done with our basic model!
### 4.3 Adding the DbContext

> More information on DbContext [here](https://docs.microsoft.com/en-us/dotnet/api/system.data.entity.dbcontext?view=entity-framework-6.2.0)

Right click the project and create a new folder called Data. In this new folder create a new class called StudentContext, Add the following code to the file and fix all the errors using the suggestions.
```C#
public class StudentContext : DbContext
    {
        public StudentContext() {}
        public StudentContext(DbContextOptions<StudentContext> options) : base(options) {}
        public DbSet<Student> Student { get; set; }
        public static System.Collections.Specialized.NameValueCollection AppSettings { get; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
           .AddJsonFile("appsettings.json")
           .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("schoolSIMSConnection"));
        }
    }
```
![Context](./img/api%20%2815%29.png)
![Context](./img/api%20%2816%29.png)
# 5. Time to Code – Migrations
Now that we have set up our model and the context we can begin to update the database with our model. Code first programming will allow us to mirror our model in our database. First remember we had the connection previously when we created our database. Open appsettings.json and add the following where CONNECTIONSTRING is the string you copied earlier. 
```JSON
"AllowedHosts": "*",
"ConnectionStrings": {
    "schoolSIMSConnection": "CONNECTIONSTRING"
  }
```
![conn string add](./img/api%20%2817%29.png)
> Make sure you replace {your_password} with your admin password in the connection string you copied

At the bottom of the screen click package console manager, run the following command ```Add-Migration InitialCreate```.  Running this will automatically create files needed to update the database. A folder called `Migration` will be create which will have a record or all migrations we have made. This is basically git but for our model. 
![add-migrations](./img/api%20%2818%29.png)
![add-migrations](./img/api%20%2819%29.png)
We haven’t updated the remote database yet but running the command `Update-Database` will create the model on our database.

Go back to Azure and find your database and select the `Query Editor` on the left hand panel, log in and expand the Tables folder. 
![table updated](./img/api%20%2820%29.png)
You can see two tables. one is a record of the migrations we have made and the other is the table for your model. You have successfully updated the database using code first approach. If you want to know how to do database first take a look at the last years API and Databases [here](https://github.com/NZMSA/2019-Phase-1/tree/master/Databases%20&%20API).

# 5. Time to Code – API Controllers
> The controller is where all our api’s are created. To create basic API we will use scaffolding which will give us some API that is automatically created.

Open Startup.cs and add the follow code to in ConfigureServices, replacing the string `schoolSIMSConnection` with the connection string name you have in `appsettings.json`
 ```C# 
	var connection = Configuration.GetConnectionString("schoolSIMSConnection");
	services.AddDbContext<StudentContext>(options => options.UseSqlServer(connection));}
```
>What we are doing is letting our program know that we want to use this context we have created

Right click the `Controllers` folder and select Add->New Scaffold Item-> Select API Controller with actions, using Entity Framework. Here select your model and context you create previously.
![scaffolding nav](./img/api%20%2821%29.png)
![Api controller with](./img/api%20%2822%29.png)
![model and context](./img/api%20%2823%29.png)
![results](./img/api%20%2824%29.png)
>It should generate the API for you. This is very basic api but it will give us the some boiler plate code to work with. You can run then program again but go to one of the api/Students. 

This isn’t very visual pleasing to work with so we will add some UI in the next step.

# 5. Time to Code – Swagger.
### 5.1 Setting up Swagger
Install the nuget package Swashbuckle.AspNetCore

Add the following code to Startup.cs in the ConfigureServices and fix the errors that pop up
```C#
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "StudentSIMS", Version = "v1" });
});
```
Add the following to Configure
```C#
app.UseSwagger();
// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
// specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My first API V1");
    c.RoutePrefix = string.Empty; // launch swagger from root
});
```
In `Properties/launchsetting.json` edit the launchUrl to be `“”`

Run the program and you should now be greeted with a nice Swagger UI
![swagger ui](./img/api%20%2825%29.png)
### 5.2 Testing our API
Time to see if our API is working.

Click on `POST api/Students` and click `Try it out`
![post](./img/api%20%2828%29.png)
>POST is an HTTP method used to send data to a server to create a new record

Edit the string so that it has a first and last name and email are filled out. It doesn't matter what StudentId is when we post because it will be ignored and autogenerated for us.
![edit post](./img/api%20%2826%29.png)
Click `Execute` if the response is 201 then we have successfully added some data to our database. See [here](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status) for common HTTP response codes
![response](./img/api%20%2827%29.png)
We can check if our data was added in our database by this executing our `GET api/students` if it returns this then we know our API is fully functional.
![get](./img/api%20%2829%29.png)

# 6. Time to Code – Updating our model
If your model needs to change we can simply add it to our existing model. I will add a timestamp and phone and middle name. I will also make it a so that the first and lastname are required fields and firstname has a max length allowable. (Click [here](https://docs.microsoft.com/en-us/ef/core/modeling/entity-properties?tabs=data-annotations,without-nrt) to see more data annotations you can apply to the model)
```C#
public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int studentId { get; set; }
        [Required, MaxLength(100)]
        public string firstName { get; set; }
        public string midlleName { get; set; }
        [Required]
        public string lastName { get; set; }
        public string emailAddress { get; set; }
        public int phoneNumber { get; set; }
        [Timestamp]
        public DateTime timeCreated { get; set; }
    }
```
![migration update](./img/api%20%2830%29.png)
Go to package manger console and run ```Add-Migration UpdatedStudentModel``` and ```Update-Database```.

If you make a mistake with the model you can call roll back the migration by calling Update-Database with the name of the previous migration. Take a look [here](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=vs#remove-a-migration)  and [here](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli) for more Migration functionality

# 7. Assignments for API + Database module

## 7.1 Submission
Students will need to submit a link to GitHub repository. README.md file will contain the following contents:
- All the screenshots and explanations/notes
- URLs of your APIs that have been hosted on Azure

## 7.2 Project Guidelines
- Create a code-first API server with Azure SQL Database
  - Database:
    - Create another table named "Address" with attributes: StudentId, Street Number, Street, Suburb, City, Postcode and Country. The Student table would have one-to-many relationship with this table. Please assign appropriate datatype for each of the attributes.
    - Show SQL database through the Query editor (screenshots) for both tables with rows of example instances
  - API manipulate the created Azure Database using Code-First migration:
    - Create basic CRUD requests for Student and Address
    - Create an API that add new address for a student using his/her StudentId.
    - Create an API that change the address of a student using his/her StudentId.
    - Screenshot of Swagger UI for each of the CRUD requests with header, body, and successful response status
- MS Learn
Student will need to finish 1 compulsory and 1 optional module and submit the screenshots:
  - Compulsory: [Create a web API with ASP.NET Core](https://docs.microsoft.com/en-us/learn/modules/build-web-api-net-core/?fbclid=IwAR0YijdrKtl3UUkQLVTUw3i6RTJbkxLte7RbZhD2aBPYvZva-Pp-_WRYbJM)
  - Optional: Choose 1 out of the following two modules:
    - [Provision an Azure SQL database to store application data](https://docs.microsoft.com/en-us/learn/modules/provision-azure-sql-db/?fbclid=IwAR0k7zN0rgLgISyDoSZP7l3Mm1nEUjUY9nJJS0TnVEPjdn78xzWThfJesLk)
    - [Develop and configure an ASP.NET application that queries an Azure SQL database](https://docs.microsoft.com/en-us/learn/modules/develop-app-that-queries-azure-sql/?fbclid=IwAR2j2JDWm8dfkpOV8T-QYu6M1VHw6cFgvRBYF03K_ZXUerX2HJ28O2OUWBo)

