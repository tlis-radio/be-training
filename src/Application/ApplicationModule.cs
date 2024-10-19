using Application.Features.Notes;
using Application.PipelineBehaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationModule
{
    public static void AddApplicationModule(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(ApplicationModule).Assembly);
        });
        services.AddValidatorsFromAssembly(typeof(ApplicationModule).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddScoped<NoteProjectionsUnitOfWork>();
    }
}