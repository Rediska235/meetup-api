using MeetupAPI.Application.Services.Implementations;
using MeetupAPI.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MeetupAPI.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IMeetupService, MeetupService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
