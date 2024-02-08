using ATM.AppServices.CustomerSetup.Dtos;
using ATM.Data;
using AutoMapper;

namespace ATM.Mapping
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            #region Map To Object
            // Added by TR on 07-Feb-2024
            CreateMap<CustomerDto, Customer>();
            CreateMap<CreateCustomerDto, Customer>();
            CreateMap<UpdateCustomerDto, Customer>();
            #endregion

            #region Map To Dto
            CreateMap<Customer, CustomerDto>();
            #endregion

        }
    }
}
