namespace LearningManagementSystemTeamC.Api.Common.Extensions
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddCorsPolicy(
            this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("Frontend", policy =>
                {
                    policy
                        .WithOrigins("http://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            return services;
        }

        public static WebApplication UseCorsPolicy(
            this WebApplication app)
        {
            app.UseCors("Frontend");

            return app;
        }
    }
}
