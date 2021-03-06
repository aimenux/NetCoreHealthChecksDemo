using Api.Extensions;
using Api.HealthCheckers;
using Api.HealthPublishers;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Api
{
    public class Startup
    {
        private const string ApiVersion = "v1";
        private const string ApiName = "NetCoreHealthChecksDemo";
        private const string HealthCheckLivenessEndpointUrl = @"/live";
        private const string HealthCheckLivenessEndpointName = @"Liveness";
        private const string HealthCheckReadinessEndpointUrl = @"/ready";
        private const string HealthCheckReadinessEndpointName = @"Readiness";

        private const int MaximumHistoryEntries = 30;
        private const int EvaluationTimeInSeconds = 30;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(ApiVersion, new OpenApiInfo {Title = ApiName, Version = ApiVersion});
            });

            services.AddHealthChecks()
                .AddCheck<PingHealthChecker>(nameof(PingHealthChecker))
                .AddCheck<RandomHealthChecker>(nameof(RandomHealthChecker))
                .AddCheck<AzureSqlHealthChecker>(nameof(AzureSqlHealthChecker))
                .AddApplicationInsightsAvailabilityPublisher()
                .AddApplicationInsightsPublisher();

            services.AddHealthChecksUI(settings =>
            {
                settings.SetEvaluationTimeInSeconds(EvaluationTimeInSeconds);
                settings.MaximumHistoryEntriesPerEndpoint(MaximumHistoryEntries);
                settings.AddHealthCheckEndpoint(HealthCheckLivenessEndpointName, HealthCheckLivenessEndpointUrl);
                settings.AddHealthCheckEndpoint(HealthCheckReadinessEndpointName, HealthCheckReadinessEndpointUrl);
            }).AddUiStorageProvider(Configuration);
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{ApiVersion}/swagger.json", ApiName);
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHealthChecks(HealthCheckLivenessEndpointUrl, new HealthCheckOptions
                {
                    Predicate = check => check.Name.Equals(nameof(PingHealthChecker)),
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecks(HealthCheckReadinessEndpointUrl, new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

                endpoints.MapHealthChecksUI();
            });
        }
    }
}
