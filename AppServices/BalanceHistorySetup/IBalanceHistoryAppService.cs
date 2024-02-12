using ATM.AppServices.BalanceHistorySetup.Dtos;
using ATM.AppServices.CustomerSetup.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATM.AppServices.BalanceHistorySetup
{
    public interface IBalanceHistoryAppService
    {
        IReadOnlyList<BalanceHistoryDto> GetAllByAccountId(SearchBalanceHistoryDto input);
        IReadOnlyList<DashboardItemDto> GetAllWithdraw();
        IReadOnlyList<DashboardItemDto> GetAllDeposit();
    }
}
