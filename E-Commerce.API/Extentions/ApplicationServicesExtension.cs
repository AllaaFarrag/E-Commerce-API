using E_Commerce.API.Errors;
using E_Commerce.Core.Interfaces;
using E_Commerce.Core.Interfaces.Repositories;
using E_Commerce.Core.Interfaces.Services;
using E_Commerce.Repository.Context;
using E_Commerce.Repository.Repositories;
using E_Commerce.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Reflection;

namespace E_Commerce.API.Extentions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services , IConfiguration configuration)
        {
            // Add services to the container.

            services.AddDbContext<DataContext>(o =>
            {
                o.UseSqlServer(configuration.GetConnectionString("SQLConnections"));
            });

            services.AddDbContext<IdentityDataContext>(o =>
            {
                o.UseSqlServer(configuration.GetConnectionString("IdentitySQLConnections"));
            });

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICashService, CashService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddSingleton<IConnectionMultiplexer>(opt =>
            {
                var config = ConfigurationOptions.Parse(configuration.GetConnectionString("RedisConnection"));
                return ConnectionMultiplexer.Connect(config);
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Where(m => m.Value.Errors.Any()).SelectMany(m => m.Value.Errors).Select(e => e.ErrorMessage).ToList();

                    var response = new ApiValidationErrorResponse() { Errors = errors };
                    return new BadRequestObjectResult(response);
                };
            });

            return services;
        }
    }
}
