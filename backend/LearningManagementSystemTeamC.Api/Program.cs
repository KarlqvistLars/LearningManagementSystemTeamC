using LearningManagementSystemTeamC.Api.Common.Extensions;
using LearningManagementSystemTeamC.Application;
using LearningManagementSystemTeamC.Application.Auth;
using LearningManagementSystemTeamC.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.ActiveSwaggerAuthentication();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddCorsPolicy();
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseApiMiddlewares();

await app.SeedDatabaseAsync();

app.Run();