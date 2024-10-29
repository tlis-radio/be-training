using Application.Model;
using Core.Messaging;

namespace Application.Features.Queries.Get;

public record GetByIdQuery(Guid NoteId) : IQuery<Note>;