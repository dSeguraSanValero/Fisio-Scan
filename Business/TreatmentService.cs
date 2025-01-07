using FisioScan.Data;
using FisioScan.Models;

namespace FisioScan.Business;

public class TreatmentService : ITreatmentService
{
    private readonly ITreatmentRepository _repository;

    public TreatmentService(ITreatmentRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public IEnumerable<Treatment> GetTreatments(int? treatmentId, int? patientId, int? createdBy, string? treatmentCause, DateTime treatmentDate)
    {
        return _repository.GetAllTreatments(treatmentId, patientId, createdBy, treatmentCause, treatmentDate);
    }

    public void RegisterTreatment(int patientId, int createdBy, int treatmentId, string treatmentCause, DateTime treatmentDate)
    {
        var newTreatment = new Treatment
        {
            CreatedBy = createdBy,
            PatientId = patientId,
            TreatmentId = treatmentId,
            TreatmentDate = treatmentDate,
            TreatmentCause = treatmentCause
        };

        _repository.AddTreatment(newTreatment);
    }
}