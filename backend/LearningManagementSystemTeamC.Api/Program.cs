using LearningManagementSystemTeamC.Api.Common.Extensions;
using LearningManagementSystemTeamC.Application;
using LearningManagementSystemTeamC.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Handlers register
//builder.Services.AddApplication();
// Repositories register
//builder.Services.AddInfrastructure(builder.Configuration);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseApiMiddlewares();

app.MapControllers();

app.Run();