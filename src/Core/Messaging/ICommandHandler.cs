using MediatR;

namespace Core.Messaging;

/// <summary>
/// Defines a handler for a command
/// </summary>
/// <typeparam name="TCommand">The type of command being handled</typeparam>
/// <typeparam name="TResponse">The type of response from the handler</typeparam>
public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>;

/// <summary>
/// Defines a handler for a command with a void response.
/// </summary>
/// <typeparam name="TCommand">The type of command being handled</typeparam>
public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
    where TCommand : ICommand;