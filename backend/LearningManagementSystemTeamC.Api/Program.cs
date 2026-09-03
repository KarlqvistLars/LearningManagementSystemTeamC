using LearningManagementSystemTeamC.Api.Common.Extensions;
using LearningManagementSystemTeamC.Application;
using LearningManagementSystemTeamC.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.ActiveSwaggerAuthentication();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseApiMiddlewares();

await app.SeedDatabaseAsync();

app.MapControllers();

app.Run();