using System.Threading;
using System.Threading.Tasks;
using FluentValidation;

namespace Fusap.Common.Mediator.FluentValidation
{
    public interface IValidationContextFilter
    {
        Task ApplyAsync(IValidationContext context, CancellationToken cancellationToken);
    }
}
