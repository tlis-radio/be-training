using Application.Features;
using Application.Features.Notes.Projections.Repositories;
using Core.Domain.Model;
using Domain.Model.Notes;
using Infrastructure.DataAccess;
using Infrastructure.DataAccess.Notes;
using Infrastructure.DataAccess.Notes.Projections;
using Infrastructure.DataAccess.Notes.Projections.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Infrastructure;

public static class InfrastructureModule
{
    public static void AddInfrastructureModule(this IServiceCollection services)
    {
        BsonSerializer.RegisterSerializer(new ObjectSerializer(ObjectSerializer.AllAllowedTypes));
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

        services.AddScoped<IEventStore, EventStore>();
        services.AddScoped<IMongoDatabase>(provider =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            string mongoDbConnectionString =
                configuration.GetConnectionString("EventsDb") ?? throw new Exception("No connection string with the name: EventsDb");
            var client = new MongoClient(mongoDbConnectionString);
            var sequence = new EventStore.Sequence();
            sequence.Insert(client.GetDatabase("NotesEventStore"));
            return client.GetDatabase("NotesEventStore");
        });
        services.AddScoped<INotesRepository, NotesRepository>();
        services.AddDbContext<NotesProjectionsDbContext>((provider, builder) =>
        {
            builder.UseInMemoryDatabase("NotesInMemoryDatabase");
        });
        services.AddScoped(typeof(IProjectionsRepository<>), typeof(DefaultProjectionsRepository<>));
        services.AddScoped<INoteProjectionsRepository, NoteProjectionRepository>();
    }
}