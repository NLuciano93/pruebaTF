using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Fusap.Common.Mediator;
using Fusap.Common.Model;
using Fusap.TimeSheet.Application.Notifications;
using Fusap.TimeSheet.Application.Util;
using Fusap.TimeSheet.Data;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Entities;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;
using Microsoft.Extensions.Logging;

namespace Fusap.TimeSheet.Application.UseCases.Registrations.V1.Insert
{
    public class AddRegistrationHandler : Handler<AddRegistrationRequest, RegistrationIdResponse>
    {
        private readonly ILogger<AddRegistrationHandler> _logger;
        private readonly IService _service;
        private readonly IMapper _mapper;

        public AddRegistrationHandler(ILogger<AddRegistrationHandler> logger, IService service, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<Result<RegistrationIdResponse>> Handle(AddRegistrationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var isUsernameExists = await _service.CheckUserNameExistsAsync(request.Username);

                if (isUsernameExists)
                {
                    return Conflict(ErrorCatalog.TimeSheet.UsernameAlreadyExists);
                }

                var registration = _mapper.Map<Registration>(request);

                registration.CreatedOn = DateTime.Now;
                registration.RoleId = (int)request.Roles;
                registration.Password = EncryptionLibrary.EncryptText(registration.Password);
                registration.ConfirmPassword = EncryptionLibrary.EncryptText(registration.ConfirmPassword);

                var registrationId = await _service.InsertAsync(registration);

                return new RegistrationIdResponse { RegistrationId = registrationId };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Source: {source} - Message: {message}", ex.Source, ex.Message);
                return Error(ErrorCatalog.TimeSheet.Exception);
            }
        }
    }
}
