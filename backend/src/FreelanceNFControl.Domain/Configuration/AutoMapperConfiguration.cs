using AutoMapper;
using FreelanceNFControl.Domain.Entities;
using FreelanceNFControl.Infra.Core.Requests.Expense;
using FreelanceNFControl.Infra.Core.Requests.Invoice;

namespace FreelanceNFControl.Domain.Configuration
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<AddInvoiceRequest, Invoice>()
                .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => DateTime.Parse(src.PaymentDate)));

            CreateMap<AddExpenseRequest, Expense>()
                .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => DateTime.Parse(src.PaymentDate)))
                .ForMember(dest => dest.CompetenceDate, opt => opt.MapFrom(src => DateTime.Parse(src.CompetenceDate)));
        }
    }
}
