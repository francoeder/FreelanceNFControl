using AutoMapper;
using FreelanceNFControl.Domain.Entities;
using FreelanceNFControl.Infra.Core.Requests.Invoice;

namespace FreelanceNFControl.Domain.Configuration
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<AddInvoiceRequest, Invoice>()
                .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => DateTime.Parse(src.PaymentDate)));
        }
    }
}
