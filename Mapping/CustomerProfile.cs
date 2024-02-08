using ATM.AppServices.Authentication.Dtos;
using ATM.AppServices.CustomerSetup.Dtos;
using ATM.Areas.Identity.Data;
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

            // Added by TR on 08-Feb-2024
            CreateMap<ApplicationUserDto, ApplicationUser>();
            CreateMap<UpdateLoginAccountDto, Customer>();
            #endregion

            #region Map To Dto
            CreateMap<Customer, CustomerDto>();

            // Added by TR on 08-Feb-2024
            CreateMap<ApplicationUser, ApplicationUserDto>();
            #endregion

        }
    }
}
