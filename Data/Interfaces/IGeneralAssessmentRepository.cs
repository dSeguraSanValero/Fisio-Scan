using FisioScan.Models;

namespace FisioScan.Data;
public interface IGeneralAssessmentRepository
{
    public IEnumerable<GeneralAssessment> GetAllGeneralAssessments(int? generalAssessmentId, int? createdBy, int? treatmentId, int? painLevel, string? usualPhysicalActivity, string? height, string? weight, string? occupation, string? medicalHistory);
    public void AddGeneralAssessment(GeneralAssessment generalAssessment);
}