# Deploy .NET Core Web API to Azure
In this tutorial, I will show how to deploy our finished .NET CORE Web API to Azure.

## Development Environment
1. Visual Studio Community 2019, version 16.6.2
2. .NET CORE 3.1
3. Azure Student Subscription

## Configure CORS
Firstly, we will configure the application to enable CORS policy so that we can host it on Azure with Swagger UI.

In Visual Studio, go to your `Startup.cs` file and update the Configure and ConfigureServices methods as below:
``` csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {      
       app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials()
            );
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors();
    }
```

Your two methods then will be look like:

![Configure CORS](./img/configure-cors.PNG)

## Deployment
In order to deploy our .NET Core Application to Azure, we need an Azure account with subscription. If you are a student, you can register a student subscription with Microsoft Azure.
Once you are ready with your Azure account, login into [Azure](http://portal.azure.com/) and search for "App Services"

From "App Services", we choose the "Add" to create a new App Service.

![Add App Services](./img/add-app-services.PNG)

Then we need to fill out the following fields:

![Web App Form](./img/web-app-form.PNG)

- **Subscription**: Choose "Azure for Students" if you are a student and have registered for the student subscription with Azure
- **Resource Group**: is a container that holds related resources for an Azure solution. If you don’t have existing resource group, you can click “Create new” to create one.
- **Name**: this would be your app’s name.
- **Publish**: I will choose the code option.
- **Runtime Stack**: select the correct runtime stack. For example, in this tutorial, we are using .NET Core 3.1, so I will select .NET Core 3.1.
- **Region**: select the correct region.
- **Plan**: leave as default
- **Sku and size**: Here I choose Free F1 option.

Then choose `Review and Create`, check for your configurations, and select `Create`.

Azure will then deploy your empty app to Azure server.

After we have configured the web service on Azure, we are now move to deploy our .NET Core Web API code to Azure.

Now go back to your Visual Studio, right click your API project and choose `Publish...`

![Choose Publish](./img/choose-publish.PNG)

Then choose `Azure` as our `Target` to publish

![Where to Publish](./img/where-publish.PNG)

For `Specific target`, we choose `Azure App Service(Windows)`

![Windows Publish](./img/windows-publish.PNG)

For `App Service`, we will choose the existing Azure App Service that we have just created. Make sure that you are logged in the correct account from which you created the App Service. Select the correct `Subscription` and the `App Service`

![Existing Publish](./img/existing-publish.PNG)

And then select `Finish`

In the Publish window, check to ensure that all configurations are correct. Then select `Publish` to publish your code to the existing Azure App Service.

![Publish Code](./img/publish-code.PNG)

When it is published successfully, it will automatically open up the site, else you can click the `Site URL` to open it in browser and test if your APIs work well.

![Successful Deployed](./img/successful-deployed.PNG)

Hosting APIs on Azure will allow you to call the APIs anytime on any devices. For example, developers use YouTube or Google APIs to build their applications.
