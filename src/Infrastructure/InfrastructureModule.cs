using Application.Features;
using Application.Features.Notes.Projections.Repositories;
using Core.Domain.Model;
using Domain.Model.Notes;
using Infrastructure.DataAccess;
using Infrastructure.DataAccess.Notes;
using Infrastructure.DataAccess.Notes.Projections;
using Infrastructure.DataAccess.Notes.Projections.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureModule
{
    public static void AddInfrastructureModule(this IServiceCollection services)
    {
        services.AddScoped<IEventStore, EventStore>();
        services.AddScoped<INotesRepository, NotesRepository>();
        services.AddDbContext<NotesProjectionsDbContext>((provider, builder) =>
        {
            builder.UseInMemoryDatabase("NotesInMemoryDatabase");
        });
        services.AddDbContext<EventStoreDbContext>((provider, builder) =>
        {
            builder.UseInMemoryDatabase("EventStoreInMemoryDatabase");
        });
        services.AddScoped<INoteProjectionsRepository, NoteProjectionRepository>();
    }
}