using System.Threading;
using System.Threading.Tasks;
using HealthChecks.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Api.HealthCheckers
{
    public class AzureSqlHealthChecker : IHealthCheck
    {
        private readonly SqlServerHealthCheck _checker;

        public AzureSqlHealthChecker(IConfiguration configuration)
        {
            _checker = new SqlServerHealthCheck(configuration["Settings:AzureSql"], "SELECT 1;");
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            return _checker.CheckHealthAsync(context, cancellationToken);
        }
    }
}
