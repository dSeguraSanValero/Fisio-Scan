using FisioScan.Models;

namespace FisioScan.Business;

public interface IGeneralAssessmentService
{
    public IEnumerable<GeneralAssessment> GetGeneralAssessments(int? generalAssessmentId, int? createdBy, int? treatmentId, string? usualPhysicalActivity, string? height, string? weight, string? occupation, string? medicalHistory);
    public void RegisterGeneralAssessment(string usualPhysicalActivity, string height, string weight, string occupation);
}