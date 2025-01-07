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

function pinkBackground() {
  document.body.classList.remove('green-background');
  document.body.classList.add('pink-background');
}

function greenBackground() {
  document.body.classList.remove('pink-background');
  document.body.classList.add('green-background');
}

async function registerPhysio() {
  const name = document.getElementById('physioName').value;
  const firstSurname = document.getElementById('physioFirstSurname').value;
  const secondSurname = document.getElementById('physioSecondSurname').value;
  const email = document.getElementById('email').value;
  const registrationNumber = parseInt(document.getElementById('registrationNumber').value);
  const password = document.getElementById('password').value;

  const physioData = {
      name: name,
      firstSurname: firstSurname,
      secondSurname: secondSurname,
      email: email,
      registrationNumber: registrationNumber,
      password: password
  };

  try {
      const response = await fetch('http://localhost:7238/Physio', {
          method: 'POST',
          headers: {
              'Content-Type': 'application/json'
          },
          body: JSON.stringify(physioData)
      });

      if (response.ok) {
          alert('Registro exitoso');
          document.getElementById('registerPhysioForm').reset();
      } else {
          const errorData = await response.json();
          alert(`Error en el registro: ${errorData.message || response.statusText}`);
      }
  } catch (error) {
      console.error('Error al enviar los datos:', error);
      alert('Error al registrar el fisioterapeuta. Por favor, intenta de nuevo.');
  }
}

document.addEventListener("DOMContentLoaded", function () {
  // Seleccionar el formulario de inicio de sesión y añadirle el evento de submit
  const signInForm = document.querySelector("#nav-home form");

  signInForm.addEventListener("submit", function (event) {
      // Evitar que el formulario se envíe y recargue la página
      event.preventDefault();

      // Recoger los valores de los campos de email y password
      const email = document.getElementById("loginEmail").value;
      const password = document.getElementById("loginPassword").value;

      // Imprimir los valores en consola para verificación (puedes enviar estos datos al servidor aquí)
      console.log("Email:", email);
      console.log("Password:", password);

      // Aquí puedes llamar a tu función de login, por ejemplo:
      loginPhysio(email, password);
  });
});

async function loginPhysio(email, password) {
    try {
        const response = await fetch("http://localhost:7238/Auth/login-fisioterapeuta", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({ email, password }),
        });
  
        // Verificar si la respuesta es exitosa (200-299)
        if (!response.ok) {
            // Si no es exitosa, solo registrar el error y no mostrar el alert
            console.error("Error durante el login. Código de estado: " + response.status);
            return; // Terminar la ejecución si hubo un error
        }
  
        // Intentar parsear la respuesta a JSON
        let data;
        try {
            data = await response.json();
        } catch (jsonError) {
            // Si la respuesta no está en formato JSON, registrar el error
            console.error("La respuesta no está en formato JSON:", jsonError);
            return; // Terminar la ejecución si hubo un error de formato
        }
  
        // Verificar si el token está presente en la respuesta
        if (data.token) {
            console.log("Token recibido:", data.token);
            sessionStorage.setItem("jwtToken", data.token); // Cambié localStorage por sessionStorage
        
            // Redirigir a la página privateZone.html
            window.location.href = "privateZone.html";
        
        } else {
            // Si no hay token, registrar el error
            console.error("No se recibió un token. Respuesta del servidor:", data);
        }
    } catch (error) {
        // Capturar errores y solo registrar el error en la consola (sin alert)
        console.error("Error durante el login:", error);
    }
}
  







