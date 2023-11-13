using AutoMapper;
using Vidly.Models;


namespace Vidly.Dtos
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<CustomerDto, Customer>();
        }

    }
}
