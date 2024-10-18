using Core.Domain.Model;

namespace Domain.Model.Notes.Events;

public record NewNoteCreated(NoteId NoteId, string Title, string Text) : Event<NoteId>;