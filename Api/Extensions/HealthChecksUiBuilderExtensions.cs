using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Extensions
{
    public static class HealthChecksUiBuilderExtensions
    {
        public static HealthChecksUIBuilder AddUiStorageProvider(this HealthChecksUIBuilder builder, IConfiguration configuration)
        {
            var healthChecksUiStorageType = Enum.Parse<HealthChecksUiStorageType>(configuration["Settings:HealthChecksUiStorageType"]);
            switch (healthChecksUiStorageType)
            {
                case HealthChecksUiStorageType.InMemoryStorage:
                    return builder.AddInMemoryStorage();
                case HealthChecksUiStorageType.SqlServerStorage:
                    var sqlServerConnectionString = configuration["Settings:AzureSql"];
                    return builder.AddSqlServerStorage(sqlServerConnectionString);
                default:
                    throw new ArgumentOutOfRangeException(nameof(HealthChecksUiStorageType));
            }
        }

        private enum HealthChecksUiStorageType
        {
            None,
            InMemoryStorage,
            SqlServerStorage
        }
    }
}
