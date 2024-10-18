using MediatR;

namespace Core.Messaging;

/// <summary>
/// Allows for generic type constraints of objects implementing ICommand or ICommand{TResponse}
/// </summary>
public interface ICommandBase : IBaseRequest;

/// <summary>
/// Marker interface to represent a command with a void response
/// </summary>
public interface ICommand : ICommandBase, IRequest;

/// <summary>
/// Marker interface to represent a command with a response
/// </summary>
/// <typeparam name="TResponse">Response type</typeparam>
public interface ICommand<out TResponse> : ICommandBase, IRequest<TResponse>;