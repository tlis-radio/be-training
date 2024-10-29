using Core.Messaging;

namespace Application.Features.Queries.Get;

public record ExistQuery(Guid NoteId) : IQuery<bool>;