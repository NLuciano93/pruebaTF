using System;
using System.Threading;
using System.Threading.Tasks;
using Fusap.Common.Mediator;
using Fusap.Common.Model;
using Fusap.TimeSheet.Application.Notifications;
using Fusap.TimeSheet.Application.Util;
using Fusap.TimeSheet.Data;
using Microsoft.Extensions.Logging;

namespace Fusap.TimeSheet.Application.UseCases.Token.V1.GenerateToken
{
    public class GenerateTokenHandler : Handler<GenerateTokenRequest, int?>
    {
        private readonly ILogger<GenerateTokenHandler> _logger;
        private readonly IService _service;

        public GenerateTokenHandler(ILogger<GenerateTokenHandler> logger, IService service)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public override async Task<Result<int?>> Handle(GenerateTokenRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var password = EncryptionLibrary.EncryptText(request.Password);

                var result = await _service.ValidateUserAsync(request.Username, password);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Source: {source} - Message: {message}", ex.Source, ex.Message);
                return Error(ErrorCatalog.TimeSheet.Exception);
            }
        }
    }
}
