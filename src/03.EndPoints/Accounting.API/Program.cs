using Accounting.API.Configs;
using Accounting.Infrastructures.Configs;
using Accounting.Persistence.EF;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

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
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
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

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationWriteDbContext>();

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