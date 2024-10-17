using Infrastructure.DataAccess;
using Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureModule
{
    public static void AddInfrastructureModule(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<NotesRepository>();
        serviceCollection.AddDbContext<ApplicationDbContext>((provider, builder) =>
        {
            builder.UseInMemoryDatabase("NotesInMemoryDatabase");
        });
    }
}