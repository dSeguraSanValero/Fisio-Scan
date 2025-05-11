using FisioScan.Data;
using FisioScan.Models;

namespace FisioScan.Business;

public class GeneralAssessmentService : IGeneralAssessmentService
{
    private readonly IGeneralAssessmentRepository _repository;

    public GeneralAssessmentService(IGeneralAssessmentRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public IEnumerable<GeneralAssessment> GetGeneralAssessments(int? generalAssessmentId, int? createdBy, int? treatmentId, string? usualPhysicalActivity, string? height, string? weight, string? occupation, string? medicalHistory)
    {
        return _repository.GetAllGeneralAssessments(generalAssessmentId, createdBy, treatmentId, usualPhysicalActivity, height, weight, occupation, medicalHistory);
    }

    public void RegisterGeneralAssessment(string usualPhysicalActivity, string height, string weight, string occupation)
    {
        var newGeneralAssessment = new GeneralAssessment
        {
            UsualPhysicalActivity = usualPhysicalActivity,
            Height = height,
            Weight = weight,
            Occupation = occupation
        };

        _repository.AddGeneralAssessment(newGeneralAssessment);
    }
}