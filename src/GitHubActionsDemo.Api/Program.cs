using GitHubActionsDemo.Service.Infrastructure;
using GitHubActionsDemo.Persistance.Infrastructure;
using Serilog;
using FluentValidation;
using GitHubActionsDemo.Api.Sdk.Authors;
using GitHubActionsDemo.Api.Sdk.Books;
using GitHubActionsDemo.Api.Sdk.Shared;
using GitHubActionsDemo.Api.Models.Validators;
using GitHubActionsDemo.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppSettings();

builder.Logging.ClearProviders();
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
builder.Logging.AddSerilog(logger);

builder.Services.AddScoped<IValidator<AuthorRequest>, AuthorRequestValidator>();
builder.Services.AddScoped<IValidator<BookRequest>, BookRequestValidator>();
builder.Services.AddScoped<IValidator<PageRequest>, PageParametersValidator>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings"));

// DI
builder.Services.AddServiceDependencies();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

{
    await app.InitDatabase();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
