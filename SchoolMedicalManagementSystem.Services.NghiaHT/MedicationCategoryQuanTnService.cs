using SMMS.Repositories.NghiaHT;
using SMMS.Repositories.NghiaHT.Models;

namespace SMMS.Services.NghiaHT;

public class MedicationCategoryQuanTnService
{
    private readonly MedicationCategoryQuanTnRepository _medicationQuanTnRepository;

    public MedicationCategoryQuanTnService()
    {
        _medicationQuanTnRepository = new MedicationCategoryQuanTnRepository();
    }

    public async Task<List<MedicationCategoryQuanTn>> GetAllAsync()
    {
        return await _medicationQuanTnRepository.GetAllAsync();
    }
}
