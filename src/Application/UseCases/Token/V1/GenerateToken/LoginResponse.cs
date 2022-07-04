using System;

namespace Fusap.TimeSheet.Application.UseCases.Token.V1.GenerateToken
{
    public class LoginResponse
    {
        public string Token { get; set; } = default!;
        public DateTime Expires { get; set; } = default!;
    }
}
