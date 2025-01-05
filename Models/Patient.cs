namespace FisioScan.Models;
using System.ComponentModel.DataAnnotations;

public class Patient
{
   public int PatientId { get; set; }
   public string? Name { get; set; }
   public static int PatientIdSeed { get; set; }
   public string? Dni { get; set; }

   public Patient() {
      
   }

   public Patient(string name, string dni) 
   {
      PatientId = PatientIdSeed++;
      Name = name;
      
   }
}
