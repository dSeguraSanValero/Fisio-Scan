using FisioScan.Data;
using FisioScan.Models;

namespace FisioScan.Business;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _repository;

    public PatientService(IPatientRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }


    public IEnumerable<Patient> GetPatients(string? dni = null)
    {
        return _repository.GetAllPatients(dni);
    }
}