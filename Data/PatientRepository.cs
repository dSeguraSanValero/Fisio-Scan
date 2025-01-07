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


    public IEnumerable<Patient> GetAllPatients(string? dni, int? createdBy, string? name, string? firstSurname, string? secondSurname)
    {
        var query = _context.Patients.AsQueryable();

        if (!string.IsNullOrEmpty(dni))
        {
            query = query.Where(p => p.Dni != null && p.Dni.Contains(dni, StringComparison.OrdinalIgnoreCase));
        }

        if (createdBy.HasValue)
        {
            query = query.Where(p => p.CreatedBy == createdBy.Value);
        }

        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(p => p.Name != null && p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(firstSurname))
        {
            query = query.Where(p => p.FirstSurname != null && p.FirstSurname.Contains(firstSurname, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(secondSurname))
        {
            query = query.Where(p => p.SecondSurname != null && p.SecondSurname.Contains(secondSurname, StringComparison.OrdinalIgnoreCase));
        }

        return query.ToList();
    }



    public void AddPatient(Patient patient)
    {
        _context.Patients.Add(patient);
        _context.SaveChanges();
    }


    public void RemovePatient(Patient patient)
    {
        _context.Patients.Remove(patient);
        _context.SaveChanges();
    }
}
