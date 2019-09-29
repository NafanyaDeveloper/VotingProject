using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VTBHackaton.API.Requirement;
using VTBHackaton.CORE.EF;
using VTBHackaton.CORE.Repositories;
using VTBHackaton.DATA.Enteties;
using VTBHackaton.DATA.Repositories;

namespace VTBHackaton.API.Configurations
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services
                .AddTransient<IUserRepository, UserRepository>()
                .AddTransient<IRoomRepository, RoomRepository>()
                .AddTransient<IPollRepository, PollRepository>()
                .AddTransient<IVariantRepository, VariantRepository>()
                .AddTransient<IUserRoomRepository, UserRoomRepository>()
                .AddTransient<IUserVariantRepository, UserVariantRepository>()
                .AddTransient<IDocumentRepository, DocumentRepository>();

            return services;
        }

        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", new Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicyBuilder()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .AllowCredentials()
                    .Build());
            });

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Hack", new Info { Title = "Hack", Version = "1.0" });
            });

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            
            return services;
        }

        public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole<Guid>>(o =>
            {
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireLowercase = false;
            })
                .AddEntityFrameworkStores<VTBHackatonContext>()
                .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection PolicyConfiguration(this IServiceCollection services)
        {
            services.AddTransient<IAuthorizationHandler, RoleHandler>();
            services.AddAuthorization(opts => {

                opts.AddPolicy("RoleLimit",
                    policy => policy.Requirements.Add(new RoleRequirement()));
            });
            return services;
        }

        public static IServiceCollection AddMail(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMailKit(optionBuilder =>
            {
                optionBuilder.UseMailKit(new MailKitOptions()
                {
                    Server = configuration["Server"],
                    Port = Convert.ToInt32(configuration["Port"]),
                    SenderName = configuration["SenderName"],
                    SenderEmail = configuration["SenderEmail"],

                    Account = configuration["Account"],
                    Password = configuration["Password"],
                    Security = true
                });
            });


            return services;
        }
    }

}
