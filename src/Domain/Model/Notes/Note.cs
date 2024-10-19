using Core.Domain.Model;
using Domain.Model.Notes.Events;

namespace Domain.Model.Notes;

public class Note : AggregateRoot<NoteId>
{
    private string? _title;

    private string? _text;

    /// <summary>
    ///     The empty constructor for the rehydration. DO NOT USE IT.
    /// </summary>
    public Note() { }

    public Note(NoteId noteId, string title, string text)
    {
        Identity = noteId;
        Title = title;
        Text = text;
        Enqueue(new NewNoteCreated(noteId, title, text));
    }

    public string Title
    {
        get => _title ?? BusynessRuleException.AccessingUninitialisedState<string>();
        private set
        {
            BusynessRuleException.ThrowIfNullOrWhiteSpace(value, 
                "Title may not be null or empty");
            BusynessRuleException.ThrowIfLongerThan(value, 50,
                "Title may not be longer than 50 characters");

            _title = value;
        }
    }

    public string Text
    {
        get => _text ?? BusynessRuleException.AccessingUninitialisedState<string>();
        private set
        {
            BusynessRuleException.ThrowIfNullOrWhiteSpace(value, 
                "Text may not be null or empty");

            _text = value;
        }
    }

    public void ChangeTitle(string title)
    {
        Title = title;
        
        Enqueue(new NoteTitleChanged(title)); 
    }

    public void ChangeText(string text)
    {
        Text = text;
        
        Enqueue(new NoteTextChanged(text));
    }

    protected override void Evolve(IEvent<NoteId> @event)
    {
        switch (@event)
        {
            case NewNoteCreated newNoteCreated:
                Identity = newNoteCreated.NoteId;
                Title = newNoteCreated.Title;
                Text = newNoteCreated.Text;
                break;
            case NoteTitleChanged noteTitleChanged:
                Title = noteTitleChanged.Title;
                break;
            case NoteTextChanged noteTextChanged:
                Text = noteTextChanged.Text;
                break;
            case NoteDeleted:
                //TODO: Ask what should I do with a deleted entity
                break;
        }
    }
}