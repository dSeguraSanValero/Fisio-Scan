using FisioScan.Data;
using FisioScan.Models;

namespace FisioScan.Business;

public class MuscularAssessmentService : IMuscularAssessmentService
{
    private readonly IMuscularAssessmentRepository _repository;

    public MuscularAssessmentService(IMuscularAssessmentRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public IEnumerable<MuscularAssessment> GetMuscularAssessments(int? muscularAssessmentId, int? createdBy, int? treatmentId, string? muscleName, string? muscleAssessment)
    {
        return _repository.GetAllMuscularAssessments(muscularAssessmentId, createdBy, treatmentId, muscleName, muscleAssessment);
    }

    public void RegisterMuscularAssessment(int createdBy, int treatmentId, string muscleName, string muscleAssessment)
    {
        var newMuscularAssessment = new MuscularAssessment
        {
            CreatedBy = createdBy,
            TreatmentId = treatmentId,
            MuscleName = muscleName,
            MuscleAssessment = muscleAssessment
        };

        _repository.AddMuscularAssessment(newMuscularAssessment);
    }
}