using Microsoft.AspNetCore.Mvc;
using BookingSystem.Application.Error;
using BookingSystem.Domin.Interfaces;
using BookingSystem.Infrastructure.Repositories;
using BookingSystem.Application.IServices.Auth;
using MyHero.Application.Services;
using BookingSystem.Application.Application.Services.Auth;
using BookingSystem.Application.Services.Rooms;
using BookingSystem.Application.IServices.IHotelBranchServices;
using BookingSystem.Application.IServices.IHotelServies;
using BookingSystem.Application.Services.HotelServices;
using BookingSystem.Application.Services.HotelBranchServices;

namespace BookingSystem.API.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IRolesService, RolesService>();

            services.AddScoped<IPasswordService, PasswordService>();

            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IHotelBranchService, HotelBranchService>();

            services.AddScoped<RoomServices>();

            services.AddScoped<IHotelService,HotelService>();


            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(x => x.Value!.Errors.Count > 0)
                    .SelectMany(x => x.Value!.Errors)
                    .Select(e => e.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors,
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });

            return services;
        }
    }
}

