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


    public IEnumerable<Patient> GetPatients(string? dni = null, int? createdBy = null, string? name = null, string? firstSurname = null, string? secondSurname = null)
    {
        return _repository.GetAllPatients(dni, createdBy, name, firstSurname, secondSurname);
    }

    public void RegisterPatient(string name, string firstSurname, string secondSurname, string dni)
    {
        var newPatient = new Patient
        {
            Name = name,
            FirstSurname = firstSurname,
            SecondSurname = secondSurname,
            Dni = dni
        };

        _repository.AddPatient(newPatient);
    }

    public void DeletePatient(Patient patient)
    {
        _repository.RemovePatient(patient);
    }
}