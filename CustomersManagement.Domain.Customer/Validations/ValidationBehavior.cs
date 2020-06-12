using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CustomersManagement.Domain.Customer.Validations
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            this.validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var cntx = new ValidationContext(request);
            var failures = validators.Select(m => m.Validate(cntx))
                .SelectMany(m => m.Errors)
                .Where(m => m != null)
                .ToList();

            if (failures.Any())
                throw new ValidationException(failures);

            //pre
            //TODO : log
            return next();
            //post
        }
    }
}
