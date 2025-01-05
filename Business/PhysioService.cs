using Microsoft.Extensions.Configuration;
using FisioScan.Data;
using FisioScan.Models;

namespace FisioScan.Business
{
    public class PhysioService : IPhysioService
    {
        private readonly IPhysioRepository _repository;
        private readonly IConfiguration _configuration;

        public PhysioService(IPhysioRepository repository, IConfiguration configuration)
        {
            _configuration = configuration;
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public IEnumerable<Physio> GetPhysios()
        {
            return _repository.GetAllPhysios(null, null, null, null, null, null);
        }


        // Método actualizado de registro que utiliza las propiedades actuales de Physio, incluyendo Email
        public void RegisterPhysio(string name, string lastName, string email, int registrationNumber, string password)
        {
            var newPhysio = new Physio
            {
                Name = name,
                LastName = lastName,
                Email = email, // Nueva propiedad Email
                RegistrationNumber = registrationNumber,
                Password = password
            };

            _repository.AddPhysio(newPhysio);
        }


        public Physio? ValidatePhysio(string email, string password)
        {
            // Utilizar GetPhysios con el filtro de email
            var physio = _repository.GetAllPhysios(null, null, null, null, null, email).FirstOrDefault();

            // Comprobar si la contraseña coincide
            return physio != null && physio.Password == password ? physio : null;
        }

    }
}

