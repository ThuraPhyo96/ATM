﻿using ATM.AppServices.CustomerSetup.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATM.AppServices.CustomerSetup
{
    public interface ICustomerAppService
    {
        Task<CustomerDto> GetDetailById(int id);
        Task<CustomerDto> GetDetailByGuid(string guid);
        List<CustomerDto> GetAll(SearchCustomerDto input);

        Task<CustomerDto> CreateCustomer(CreateCustomerDto input);
        Task<string> CheckDuplicateOnCreate(string nric);

        Task<CustomerDto> UpdateCustomer(UpdateCustomerDto input);
        Task<string> CheckDuplicateOnUpdate(int id, string nric);
    }
}
