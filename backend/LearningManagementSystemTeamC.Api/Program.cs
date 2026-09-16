using LearningManagementSystemTeamC.Api.Common.Extensions;
using LearningManagementSystemTeamC.Application;
using LearningManagementSystemTeamC.Application.Auth;
using LearningManagementSystemTeamC.Infrastructure;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions.Converters.Add(
        new JsonStringEnumConverter());
});
builder.Services.ActiveSwaggerAuthentication();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorizationPolicies();
builder.Services.AddCorsPolicy();
builder.Services.AddSignalR();
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseApiMiddlewares();

await app.SeedDatabaseAsync();

app.Run();