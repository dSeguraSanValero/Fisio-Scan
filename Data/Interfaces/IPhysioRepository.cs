using FisioScan.Models;

namespace FisioScan.Data
{
    public interface IPhysioRepository
    {
        IEnumerable<Physio> GetAllPhysios(
            int? registrationNumber, string? email, string? name, 
            string? lastName, string? sortBy, string? sortOrder);
        
        void AddPhysio(Physio physio);
    }
}

