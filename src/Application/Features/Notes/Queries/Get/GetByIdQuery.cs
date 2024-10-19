using Application.Features.Notes.Projections;
using Core.Messaging;
using Domain.Model.Notes;

namespace Application.Features.Notes.Queries.Get;

public record GetByIdQuery(NoteId NoteId) : IQuery<NoteProjection>;