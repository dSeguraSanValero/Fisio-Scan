using FisioScan.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace FisioScan.Data;

public class PatientRepository : IPatientRepository
{
    private readonly FisioScanContext _context;

    public PatientRepository(FisioScanContext context)
    {
        _context = context;
    }


    public IEnumerable<Patient> GetAllPatients(string? dni = null)
    {
        var query = _context.Patients.AsQueryable();

        if (!string.IsNullOrEmpty(dni))
        {
            query = query.Where(p => p.Dni == dni);
        }

        return query.ToList();
    }
}
