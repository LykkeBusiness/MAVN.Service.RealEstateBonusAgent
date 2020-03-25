using JetBrains.Annotations;
using Lykke.Sdk;
using Lykke.Service.RealEstateBonusAgent.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using AutoMapper;
using Lykke.Service.RealEstateBonusAgent.MsSqlRepositories;
using Microsoft.AspNetCore.Mvc;

namespace Lykke.Service.RealEstateBonusAgent
{
    [UsedImplicitly]
    public class Startup
    {
        private readonly LykkeSwaggerOptions _swaggerOptions = new LykkeSwaggerOptions
        {
            ApiTitle = "RealEstateBonusAgent API",
            ApiVersion = "v1"
        };

        [UsedImplicitly]
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            return services.BuildServiceProvider<AppSettings>(options =>
            {
                options.SwaggerOptions = _swaggerOptions;

                options.Extend = (collection, manager) =>
                {
                    collection.Configure<ApiBehaviorOptions>(apiBehaviorOptions =>
                    {
                        apiBehaviorOptions.SuppressModelStateInvalidFilter = true;
                    });

                    collection.AddAutoMapper(
                        typeof(AutomapperProfile));
                };

                options.Logs = logs =>
                {
                    logs.AzureTableName = "RealEstateBonusAgentLog";
                    logs.AzureTableConnectionStringResolver = settings => settings.RealEstateBonusAgentService.Db.LogsConnString;
                };
            });
        }

        [UsedImplicitly]
        public void Configure(IApplicationBuilder app)
        {
            app.UseLykkeConfiguration(options =>
            {
                options.SwaggerOptions = _swaggerOptions;
            });
        }
    }
}
