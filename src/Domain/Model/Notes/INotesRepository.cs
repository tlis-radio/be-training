using Core.Domain.Model;

namespace Domain.Model.Notes;

public interface INotesRepository : IRepository<Note, NoteId>;