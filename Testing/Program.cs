using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySql.Data.MySqlClient;
using System.Data;
using NOGAsteWebApp;

//This creates the **Container** for the WebApplication
//ASP.NET Core includes dependency injection (DI) that makes
//configured services available throughout an app. Services
//are added to the DI container with WebApplicationBuilder.Services,
//builder.Services in the preceding code. When the WebApplicationBuilder
//is instantiated, many framework-provided services are added
var builder = WebApplication.CreateBuilder(args);

//Add Controller services to the container.
builder.Services.AddControllersWithViews();


//Add Database Connectivity
builder.Services.AddScoped<IDbConnection>((s) =>
{
    IDbConnection conn = new MySqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"));
    conn.Open();
    return conn;
});

//AddTransient is used to register services that are created each
//time they are requested. In the context of repository registration,
//this means a new instance of the repository is created every time
//it is injected into a component such as a controller or a service

//The repository create an abstraction layer between the data access
//layer and the business logic layer of an application
builder.Services.AddTransient<IEventsDBRepository, EventsDBRepository>();



//builder.Build()
//is a method call that finalizes the application configuration and builds
//the IHost for your ASP.NET Core application. The IHost represents
//the host environment for your application, including the services,
//configurations, and other settings.
//The "app" variable
//now holds the reference to this built IHost object.
//This app instance represents your application and is used to run your
//ASP.NET Core application.
var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//middleware
//used to redirect HTTP requests to HTTPS
app.UseHttpsRedirection();

//middleware
//serving static files, such as CSS, JavaScript, and images,
//directly from the web server without requiring special
//handling by your application code. It's important for
//serving client-side assets and resources.
app.UseStaticFiles();

//middleware
//sets up routing for your application. It defines how
//incoming HTTP requests should be routed to the appropriate
//controllers and action methods based on the requested URL
app.UseRouting();

//middleware
//adds support for authentication and authorization
app.UseAuthorization();

//sets up routing for MVC controllers. It defines a default
//route for handling requests to controller actions. In this
//case, it's configuring a default route named "default" that
//matches URLs with a specific pattern. The parameters in
//this method allow you to define the route's name, pattern,
//and other routing options.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


//Actually starts the web applicaiton
app.Run();