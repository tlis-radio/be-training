using Application.Model;
using Core.Messaging;

namespace Application.Features.Queries.List;

public record ListAllNotes : IQuery<List<Note>>;