using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Fusap.Common.Mediator;
using Fusap.Common.Model;
using Fusap.TimeSheet.Application.Notifications;
using Fusap.TimeSheet.Data;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Entities;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;
using Microsoft.Extensions.Logging;

namespace Fusap.TimeSheet.Application.UseCases.Expenses.V1.Insert
{
    public class AddExpenseHandler : Handler<AddExpenseRequest, ExpenseIdResponse>
    {
        private readonly ILogger<AddExpenseHandler> _logger;
        private readonly IService _service;
        private readonly IMapper _mapper;

        public AddExpenseHandler(ILogger<AddExpenseHandler> logger, IService service, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<Result<ExpenseIdResponse>> Handle(AddExpenseRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var expense = _mapper.Map<Expense>(request);
                expense.CreatedOn = DateTime.Now;

                var expenseId = await _service.InsertAsync(expense: expense);

                return new ExpenseIdResponse() { ExpenseId = expenseId };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Source: {source} - Message: {message}", ex.Source, ex.Message);
                return Error(ErrorCatalog.TimeSheet.Exception);
            }
        }
    }
}
