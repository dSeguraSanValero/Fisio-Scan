using FisioScan.Models;

namespace FisioScan.Business;

public interface IPatientService
{
    public IEnumerable<Patient> GetPatients(string? dni, int? createdBy, string? name, string? firstSurname, string? secondSurname, DateTime birthDate);
    public void RegisterPatient(string name, string firstSurname, string secondSurname, string dni);
    public void DeletePatient(Patient patient);
}