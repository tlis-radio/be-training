using Core.Domain.Model;

namespace Domain.Model.Notes.Events;

public record NoteDeleted : Event<NoteId>;