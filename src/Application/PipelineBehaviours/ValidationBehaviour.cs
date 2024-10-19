using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Application.PipelineBehaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IBaseRequest
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);
        List<ValidationFailure> errors = _validators
            .Select(validator => validator.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(failure => failure is not null)
            .ToList();

        if (errors.Count != 0)
            throw new ValidationException(errors);

        return await next();
    }
}