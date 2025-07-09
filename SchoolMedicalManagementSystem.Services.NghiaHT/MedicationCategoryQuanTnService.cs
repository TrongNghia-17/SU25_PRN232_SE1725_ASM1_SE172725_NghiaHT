using SMMS.Repositories.NghiaHT;
using SMMS.Repositories.NghiaHT.Models;

namespace SMMS.Services.NghiaHT;

public class MedicationCategoryQuanTnService
{
    private readonly MedicationCategoryQuanTnRepository _medicationCategoryQuanTnRepository;

    public MedicationCategoryQuanTnService()
    {
        _medicationCategoryQuanTnRepository = new MedicationCategoryQuanTnRepository();
    }

    public async Task<List<MedicationCategoryQuanTn>> GetAllAsync()
    {
        return await _medicationCategoryQuanTnRepository.GetAllAsync();
    }

    public async Task<int> CreateAsync(MedicationCategoryQuanTn medicationCategoryQuanTn)
    {
        return await _medicationCategoryQuanTnRepository.CreateAsync(medicationCategoryQuanTn);
    }

    public async Task<MedicationCategoryQuanTn> GetByIdAsync(int id)
    {
        return await _medicationCategoryQuanTnRepository.GetByIdAsync(id);
    }

    public async Task<int> UpdateAsync(MedicationCategoryQuanTn medicationCategoryQuanTn)
    {
        return await _medicationCategoryQuanTnRepository.UpdateAsync(medicationCategoryQuanTn);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var medicationCategoryQuanTn = await _medicationCategoryQuanTnRepository.GetByIdAsync(
            id
        );
        return await _medicationCategoryQuanTnRepository.RemoveAsync(medicationCategoryQuanTn);
    }
}
