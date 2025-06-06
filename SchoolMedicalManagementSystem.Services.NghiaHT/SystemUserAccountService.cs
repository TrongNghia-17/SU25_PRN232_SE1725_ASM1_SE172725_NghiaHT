using SMMS.Repositories.NghiaHT.Models;
using SMMS.Repositories.NghiaHT;

namespace SMMS.Services.NghiaHT;

public class SystemUserAccountService
{
    private readonly SystemUserAccountRepository _repository;
    public SystemUserAccountService()
    {
        _repository = new SystemUserAccountRepository();
    }

    public async Task<SystemUserAccount> GetUserAccountAsync(string userName, string password)
    {
        return await _repository.GetUserAccountAsync(userName, password);
    }
}
