using FisioScan.Models;

namespace FisioScan.Business;

public interface IPhysioService
{
    IEnumerable<Physio> GetPhysios();

    void RegisterPhysio(string name, string lastName, string email, int registrationNumber, string password);
    
    Physio? ValidatePhysio(string email, string password);
}
