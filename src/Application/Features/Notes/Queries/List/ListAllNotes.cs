using Application.Features.Notes.Projections;
using Core.Messaging;

namespace Application.Features.Notes.Queries.List;

public record ListAllNotes() : IQuery<List<NoteProjection>>;