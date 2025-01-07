window.onload = function() {
    const token = sessionStorage.getItem("jwtToken");
    
    if (!token) {
        window.location.href = "index.html";
        return;
    }

    setTimeout(() => {
        document.querySelectorAll('.first-cards').forEach((section, index) => {
            section.style.opacity = '0';
            section.style.transform = 'translateY(20px)';
            section.style.transition = 'opacity 0.5s ease, transform 0.5s ease';
    
            setTimeout(() => {
                section.classList.add('active');
            
                setTimeout(() => {
                    section.style.opacity = '1';
                    section.style.transform = 'translateY(0)';
                }, 300);
            }, index * 300);
        });
    }, 300);
    
    fetchPatients(token);
};

function showSection(sectionClass) {
    document.querySelectorAll('.card').forEach(section => {

        section.style.opacity = '0';
        section.style.transform = 'translateY(20px)';
        section.style.transition = 'opacity 0.5s ease, transform 0.5s ease';

        section.classList.remove('active');

        setTimeout(() => {
            section.style.opacity = '1';
            section.style.transform = 'translateY(0)';
        }, 300);
    });
    
    document.querySelectorAll(`.${sectionClass}`).forEach(section => {
        section.classList.add('active');
    });
}

async function fetchPatients(token) {
    try {
        const response = await fetch("http://localhost:7238/Patient", {
            method: "GET",
            headers: {
                "Authorization": `Bearer ${token}`,
                "Content-Type": "application/json"
            }
        });

        if (!response.ok) {
            console.error("Error al obtener los pacientes. Código de estado: " + response.status);
            return;
        }

        const data = await response.json();

        if (data && Array.isArray(data)) {
            console.log("Pacientes recibidos:", data);

            // Mostrar los pacientes en el HTML
            const container = document.getElementById('patients-container'); // Contenedor principal en HTML
            container.innerHTML = ''; // Limpia el contenedor antes de agregar nuevos elementos

            data.forEach(patient => {
                const patientDiv = document.createElement('div');
                patientDiv.className = 'patient-card'; // Clase opcional para estilos

                // Contenido del div con los atributos del paciente
                patientDiv.innerHTML = `
                    <p><strong>Name:</strong> ${patient.name}</p>
                    <p><strong>First Surname:</strong> ${patient.firstSurname}</p>
                    <p><strong>Second Surname:</strong> ${patient.secondSurname}</p>
                    <p><strong>DNI:</strong> ${patient.dni}</p>
                    <p><strong>Birth Date:</strong> ${patient.birthDate}</p>
                `;

                container.appendChild(patientDiv);
            });
        } else {
            console.error("No se recibieron pacientes o el formato de respuesta es incorrecto");
        }
    } catch (error) {
        console.error("Error al obtener los pacientes:", error);
    }
}


function logOff() {

    sessionStorage.clear();
    localStorage.clear();

    window.location.href = "index.html";

    return;
}