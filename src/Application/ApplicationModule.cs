using Application.Features.Notes;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationModule
{
    public static void AddApplicationModule(this ServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(ApplicationModule).Assembly);
        });
        services.AddScoped<NoteProjectionsUnitOfWork>();
    }
}