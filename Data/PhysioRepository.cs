using FisioScan.Models;
using Microsoft.EntityFrameworkCore;

namespace FisioScan.Data;

public class PhysioRepository : IPhysioRepository
{
    private readonly FisioScanContext _context;

    public PhysioRepository(FisioScanContext context)
    {
        _context = context;
    } 

    
    public IEnumerable<Physio> GetAllPhysios(int? registrationNumber, string? email, string? name, string? lastName, string? sortBy, string? sortOrder)
    {
        var query = _context.Physios.AsQueryable();

        if (registrationNumber.HasValue)
        {
            query = query.Where(p => p.RegistrationNumber == registrationNumber.Value);
        }

        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(p => p.Name != null && p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(lastName))
        {
            query = query.Where(p => p.LastName != null && p.LastName.Contains(lastName, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(email))
        {
            query = query.Where(p => p.Email != null && p.Email.Contains(email, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(sortBy))
        {
            switch (sortBy.ToLower())
            {
                case "name":
                    query = sortOrder != null && sortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(p => p.Name)
                        : query.OrderBy(p => p.Name);
                    break;

                case "lastname":
                    query = sortOrder != null && sortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(p => p.LastName)
                        : query.OrderBy(p => p.LastName);
                    break;

                case "email":
                    query = sortOrder != null && sortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(p => p.Email)
                        : query.OrderBy(p => p.Email);
                    break;

                case "physioid":
                default:
                    query = sortOrder != null && sortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(p => p.PhysioId)
                        : query.OrderBy(p => p.PhysioId);
                    break;
            }
        }

        return query.ToList();
    }


    public void AddPhysio(Physio physio)
    {
        _context.Physios.Add(physio);
        _context.SaveChanges();
    }
}


