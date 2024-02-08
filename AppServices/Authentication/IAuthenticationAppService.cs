using ATM.AppServices.Authentication.Dtos;
using System.Threading.Tasks;

namespace ATM.AppServices.Authentication
{
    public interface IAuthenticationAppService
    {
        Task<string> CreateUser(CreateApplicationUserDto input);
        Task<string> CheckDuplidcateUser(string username);
    }
}
