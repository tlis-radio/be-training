using Core.Messaging;
using Domain.Model.Notes;

namespace Application.Features.Notes.Queries.Get;

public record ExistQuery(NoteId NoteId) : IQuery<bool>;