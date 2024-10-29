using Application.Features.Repositories;
using Infrastructure.DataAccess;
using Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureModule
{
    public static void AddInfrastructureModule(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(builder => builder.UseInMemoryDatabase("NotesInMemoryDatabase"));
        services.AddScoped<INotesRepository, NotesRepository>();
    }
}