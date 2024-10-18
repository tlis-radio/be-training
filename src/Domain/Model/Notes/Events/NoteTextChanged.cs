using Core.Domain.Model;

namespace Domain.Model.Notes.Events;

public record NoteTextChanged(string Text) : Event<NoteId>;