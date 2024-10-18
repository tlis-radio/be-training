using Core.Domain.Model;

namespace Domain.Model.Notes;

public record NoteId(Guid Value) : AbstractIdentity<Guid>(Value);