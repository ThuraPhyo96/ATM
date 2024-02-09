using ATM.AppServices.Authentication.Dtos;
using ATM.AppServices.BankAccountSetup.Dtos;
using ATM.AppServices.BankCardSetup.Dtos;
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
            CreateMap<BankAccountDto, BankAccount>();
            CreateMap<CreateBankAccountDto, BankAccount>();
            CreateMap<UpdateBankAccountDto, BankAccount>();
            CreateMap<UpdateBalanceDto, BankAccount>();

            // Added by TR on 09-Feb-2024
            CreateMap<BankCardDto, BankCard>();
            CreateMap<CreateBankCardDto, BankCard>();
            CreateMap<UpdateBankCardDto, BankCard>();
            #endregion

            #region Map To Dto
            CreateMap<Customer, CustomerDto>();

            // Added by TR on 08-Feb-2024
            CreateMap<ApplicationUser, ApplicationUserDto>();
            CreateMap<BankAccount, BankAccountDto>();

            // Added by TR on 09-Feb-2024
            CreateMap<BankCard, BankCardDto>();
            #endregion

        }
    }
}
