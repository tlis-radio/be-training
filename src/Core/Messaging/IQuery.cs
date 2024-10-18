using MediatR;

namespace Core.Messaging;

/// <summary>
/// Marker interface to represent a query with a void response
/// </summary>
public interface IQuery<out TResponse> : IRequest<TResponse>;