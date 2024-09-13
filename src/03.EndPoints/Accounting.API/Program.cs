using Accounting.API.Configs;
using Accounting.Infrastructures.Configs;
using Accounting.Persistence.EF;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddEnvironmentVariables()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    EnvironmentName = config.GetValue<string>("environment")
});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var connectionString =
    builder
        .Configuration
        .GetValue<string>("connectionString");

builder
    .Services
    .RegisterMessageDispatcher()
    .RegisterHangfire(connectionString!)
    .RegisterDbContext(connectionString!);

builder.Services.AddAuthentication()
    .AddCookie(IdentityConstants.ApplicationScheme)
    .AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorization();

builder.Services.AddIdentityCore<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationWriteDbContext>()
    .AddApiEndpoints();

builder
    .Host
    .ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder
            .RegisterRepository()
            .RegisterOutboxManagement()
            .RegisterUnitOfWork()
            .RegisterICommandHandler()
            .RegisterMessageHandler();
    });

builder
    .WebHost
    .UseUrls(
        "http://0.0.0.0",
        builder.Configuration.GetValue<string>("url")
        );

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseHangfire();

app.UseCors(cors =>
{
    cors.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
});

app.MapIdentityApi<IdentityUser>();

app.Run();