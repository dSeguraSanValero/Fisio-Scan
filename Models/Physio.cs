namespace FisioScan.Models;

public class Physio
{
   public int PhysioId { get; set; }
   public string? Name { get; set; }
   public string? LastName { get; set; }
   public int RegistrationNumber { get; set; }
   public string Email { get; set; }
   public string? Password { get; set; }
   public static int PhysioIdSeed { get; set; }

   public Physio() {

   }

   public Physio(string name, string lastName, string email, int registrationNumber, string password) 
   {
      PhysioId = PhysioIdSeed++;
      Name = name;
      LastName = lastName;
      Email = email;
      RegistrationNumber = registrationNumber;
      Password = password;
   }
}

