using Core.Domain.Model;

namespace Domain.Model.Notes.Events;

public record NoteTitleChanged(string Title) : Event<NoteId>;