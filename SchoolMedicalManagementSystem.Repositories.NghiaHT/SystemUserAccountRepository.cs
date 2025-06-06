using Microsoft.EntityFrameworkCore;
using SMMS.Repositories.NghiaHT.Basic;
using SMMS.Repositories.NghiaHT.DBContext;
using SMMS.Repositories.NghiaHT.Models;

namespace SMMS.Repositories.NghiaHT;

public class SystemUserAccountRepository : GenericRepository<SystemUserAccount>
{
    public SystemUserAccountRepository()
    {
        _context ??= new DBContext.SU25_PRN232_SE1725_G2_SMMSContext();
    }

    public SystemUserAccountRepository(SU25_PRN232_SE1725_G2_SMMSContext context)
    {
        _context = context;
    }

    public async Task<SystemUserAccount> GetUserAccountAsync(string userName, string password)
    {
        return await _context
            .SystemUserAccounts
            .FirstOrDefaultAsync(x => x.UserName == userName && x.Password == password);

        //return await _context
        //    .SystemUserAccounts
        //    .FirstOrDefaultAsync(x => x.UserName == userName && x.Password == password && x.IsActive == true);
    }
}
