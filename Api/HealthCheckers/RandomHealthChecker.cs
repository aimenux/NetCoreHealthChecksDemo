using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Api.HealthCheckers
{
    public class RandomHealthChecker : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            var next = RandomNumberGenerator.GetInt32(1, 1000);

            var result = next switch
            {
                var n when (n % 2 == 0) => HealthCheckResult.Healthy(),
                var n when (n % 5 == 0) => HealthCheckResult.Degraded(),
                _ => HealthCheckResult.Unhealthy()
            };

            return Task.FromResult(result);
        }
    }
}
