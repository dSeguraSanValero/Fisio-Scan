(() => {
    'use strict'
  
    const forms = document.querySelectorAll('.needs-validation')
  
    Array.from(forms).forEach(form => {
      form.addEventListener('submit', event => {
        if (!form.checkValidity()) {
          event.preventDefault()
          event.stopPropagation()
        }
  
        form.classList.add('was-validated')
      }, false)
    })
})()

async function registerPhysio() {
  // Capturamos los valores de los campos del formulario
  const name = document.getElementById('physioName').value;
  const lastName = document.getElementById('physioLastName').value;
  const email = document.getElementById('email').value;
  const registrationNumber = parseInt(document.getElementById('registrationNumber').value);
  const password = document.getElementById('password').value;

  // Construimos el objeto con los datos
  const physioData = {
      name: name,
      lastName: lastName,
      email: email,
      registrationNumber: registrationNumber,
      password: password
  };

  try {
      // Realizamos la solicitud POST al servidor
      const response = await fetch('http://localhost:7238/Physio/register', {
          method: 'POST',
          headers: {
              'Content-Type': 'application/json'
          },
          body: JSON.stringify(physioData)
      });

      if (response.ok) {
          // Si el registro es exitoso
          alert('Registro exitoso');
          document.getElementById('registerPhysioForm').reset();
      } else {
          // Si hay un error en la respuesta
          const errorData = await response.json();
          alert(`Error en el registro: ${errorData.message || response.statusText}`);
      }
  } catch (error) {
      // En caso de error de red u otro problema
      console.error('Error al enviar los datos:', error);
      alert('Error al registrar el fisioterapeuta. Por favor, intenta de nuevo.');
  }
}