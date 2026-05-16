using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyPost.Application.Behaviors;
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is IValidatableObject validatable)
        {
            var validationContext = new ValidationContext(validatable);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(validatable, validationContext, results, true))
            {
                throw new ValidationException(string.Join(", ", results));
            }
        }
        return await next();
    }
}
