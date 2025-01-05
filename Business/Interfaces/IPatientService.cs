using FisioScan.Models;

namespace FisioScan.Business;

public interface IPatientService
{
    public IEnumerable<Patient> GetPatients(string? dni = null);
}