using Microsoft.EntityFrameworkCore;
using SMMS.Repositories.NghiaHT.Basic;
using SMMS.Repositories.NghiaHT.DBContext;
using SMMS.Repositories.NghiaHT.Models;

namespace SMMS.Repositories.NghiaHT;

public class MedicationCategoryQuanTnRepository : GenericRepository<MedicationCategoryQuanTn>
{
    public MedicationCategoryQuanTnRepository()
    {
        _context ??= new DBContext.SU25_PRN232_SE1725_G2_SMMSContext();
    }

    public MedicationCategoryQuanTnRepository(SU25_PRN232_SE1725_G2_SMMSContext context)
    {
        _context = context;
    }

    public async Task<List<MedicationCategoryQuanTn>> GetAllMedicationCategoryAsync()
    {
        var list = await _context.MedicationCategoryQuanTns.ToListAsync();
        return list ?? new List<MedicationCategoryQuanTn>();
    }
}
