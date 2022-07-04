using Fusap.Common.Mediator;

namespace Fusap.TimeSheet.Application.UseCases.Token.V1.GenerateToken
{
    public class GenerateTokenRequest : Request<int?>
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
