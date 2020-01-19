using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Api.HealthPublishers
{
    public class ApplicationInsightsAvailabilityPublisher : IHealthCheckPublisher
    {
        private readonly TelemetryClient _telemetryClient;

        public ApplicationInsightsAvailabilityPublisher(TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
        }

        public Task PublishAsync(HealthReport report, CancellationToken cancellationToken)
        {
            foreach (var (key, value) in report.Entries)
            {
                var availabilityTelemetry = GetAvailabilityTelemetry(key, value);
                _telemetryClient.TrackAvailability(availabilityTelemetry);
            }

            return Task.CompletedTask;
        }

        private AvailabilityTelemetry GetAvailabilityTelemetry(string name, HealthReportEntry entry)
        {
            var availabilityTelemetry = new AvailabilityTelemetry
            {
                Name = name,
                Duration = entry.Duration,
                Message = entry.Description,
                Timestamp = DateTimeOffset.UtcNow,
                Success = entry.Status == HealthStatus.Healthy
            };

            availabilityTelemetry.Properties.Add(nameof(entry.Status), entry.Status.ToString());

            foreach (var (key, value) in entry.Data)
            {
                availabilityTelemetry.Properties.Add(key, value?.ToString());
            }

            return availabilityTelemetry;
        }
    }

    public static class HealthChecksBuilderExtensions
    {
        public static IHealthChecksBuilder AddApplicationInsightsAvailabilityPublisher(this IHealthChecksBuilder builder)
        {
            builder.Services.AddSingleton((Func<IServiceProvider, IHealthCheckPublisher>) (sp => new ApplicationInsightsAvailabilityPublisher(sp.GetRequiredService<TelemetryClient>())));
            return builder;
        }
    }
}
