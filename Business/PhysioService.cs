using Microsoft.Extensions.Configuration;
using FisioScan.Data;
using FisioScan.Models;

namespace FisioScan.Business
{
    public class PhysioService : IPhysioService
    {
        private readonly IPhysioRepository _repository;

        public PhysioService(IPhysioRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public IEnumerable<Physio> GetPhysios(int? physioId, int? registrationNumber, string? email, string? name, string? firstSurname, string? secondSurname, string? sortBy, string? sortOrder, string? role)
        {
            return _repository.GetAllPhysios(physioId, registrationNumber, email, name, firstSurname, secondSurname, sortBy, sortOrder, role);
        }

        public void RegisterPhysio(string name, string firstSurname, string secondSurname, int registrationNumber, string email, string password)
        {
            var newPhysio = new Physio
            {
                Name = name,
                FirstSurname = firstSurname,
                SecondSurname = secondSurname,
                Email = email,
                RegistrationNumber = registrationNumber,
                Password = password
            };

            _repository.AddPhysio(newPhysio);
        }

        public void DeletePhysio(Physio physio)
        {
            _repository.RemovePhysio(physio);
        }

        public void UpdatePhysio(Physio physio, string password, string email)
        {
            physio.Password = password;
            physio.Email = email;

            _repository.UpdatePhysioDetails(physio);
        }
    }
}

