using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VTBHackaton.CORE.EF;

namespace VTBHackaton.API.Configurations
{
    public static class ConnectionConfiguration
    {
        public static IServiceCollection AddConnectionProvider(this IServiceCollection services, IConfiguration conf)
        {
            services.AddDbContext<VTBHackatonContext>(opt =>
                opt.UseSqlServer(conf.GetConnectionString("Hack"), b => b.MigrationsAssembly("VTBHackaton.API"))
               // .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
               );

            return services;
        }
    }

}
