using AutoMapper;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Entities;

namespace Fusap.TimeSheet.Application.UseCases.Expenses.V1.Insert
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddExpenseRequest, Expense>()
                .ForMember(x => x.ExpenseId, opt => opt.Ignore())
                .ForMember(x => x.CreatedOn, opt => opt.Ignore());
        }
    }
}
