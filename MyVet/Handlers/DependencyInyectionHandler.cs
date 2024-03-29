﻿using Common.Utils.RestServices;
using Common.Utils.RestServices.Interface;
using Infraestructure.Core.Data;
using Infraestructure.Core.Repository;
using Infraestructure.Core.Repository.Interface;
using Infraestructure.Core.UnitOfWork;
using Infraestructure.Core.UnitOfWork.Interface;
using Microsoft.Extensions.DependencyInjection;
using MyVet.Domain.Services;
using MyVet.Domain.Services.Interface;

namespace MyVet.Handlers
{
    public static class DependencyInyectionHandler
    {
        public static void DependencyInyectionConfig(IServiceCollection services)
        {
            // Repository await UnitofWork parameter ctor explicit
            services.AddScoped<UnitOfWork, UnitOfWork>();


            // Infrastructure
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<SeedDb>();

            //Domain
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<IRolServices, RolServices>();
            services.AddTransient<IPetServices, PetServices>();
            services.AddTransient<IDatesServices, DatesServices>();

            //rest services - consumer
            services.AddTransient<IRestService, RestService>();
        }
    }
}
