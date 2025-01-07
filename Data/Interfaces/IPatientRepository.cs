using FisioScan.Models;

namespace FisioScan.Data;
public interface IPatientRepository
{
    public IEnumerable<Patient> GetAllPatients(string? dni = null, int? createdBy = null, string? name = null, string? firstSurname = null, string? secondSurname = null);
    void AddPatient(Patient patient);
    public void RemovePatient(Patient patient);
}