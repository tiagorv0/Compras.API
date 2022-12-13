using ComprasSolution.Application.Mapper;
using ComprasSolution.Application.Services;
using ComprasSolution.Application.Services.Interfaces;
using ComprasSolution.Application.Validations;
using ComprasSolution.Domain.Integrations;
using ComprasSolution.Domain.Interfaces;
using ComprasSolution.Infra.Data.Authentication;
using ComprasSolution.Infra.Data.Context;
using ComprasSolution.Infra.Data.Integrations;
using ComprasSolution.Infra.Data.Repositories;
using ComprasSolution.Infra.Data.UoW;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ComprasSolution.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<ApplicationContext>(options => 
                                 options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPersonImageRepository, PersonImageRepository>();
            services.AddScoped<ISavePersonImage, SavePersonImage>();

            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPersonImageService, PersonImageService>();

            services.AddAutoMapper(typeof(MapperProfile));
            services.AddValidatorsFromAssemblyContaining<PersonValidator>();

            return services;
        }
    }
}
