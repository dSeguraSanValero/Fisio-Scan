using FisioScan.Models;

namespace FisioScan.Data;
public interface IPatientRepository
{
    public IEnumerable<Patient> GetAllPatients(string? dni = null);
}