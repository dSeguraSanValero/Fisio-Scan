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
    
    getPhysioName(token);
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

async function getPhysioName(token) {
    try {
        const response = await fetch("http://localhost:7238/Physio", {
            method: "GET",
            headers: {
                "Authorization": `Bearer ${token}`,
                "Content-Type": "application/json"
            }
        });

        if (!response.ok) {
            console.error("Error al obtener los datos del fisioterapeuta. Código de estado: " + response.status);
            return;
        }

        const data = await response.json();
        console.log("Datos del fisioterapeuta:", data);

        const container = document.getElementById('physioName-container');

        if (!container) {
            console.error("No se encontró un elemento con el ID 'physioName-container' en el DOM.");
            return;
        }

        if (data && data.length > 0) {
            const physio = data[0];

            const physioDiv = document.createElement('div');

            physioDiv.innerHTML = `
                <p><strong>¡Bienvenido ${physio.name}!</strong></p>
            `;

            container.appendChild(physioDiv);
        } else {
            console.error("No se encontraron fisioterapeutas en los datos.");
        }
    } catch (error) {
        console.error("Ocurrió un error al intentar obtener el nombre del fisioterapeuta:", error);
    }
}


async function fetchPatients() {
    try {

        const token = sessionStorage.getItem("jwtToken");
        
        if (!token) {
            console.error("Token no encontrado. Redirigiendo a la página de login.");
            window.location.href = "index.html";
            return;
        }

        const nameInput = document.querySelector('input[placeholder="Name"]');
        const firstSurnameInput = document.querySelector('input[placeholder="First Surname"]');
        const secondSurnameInput = document.querySelector('input[placeholder="Second Surname"]');
        const nifInput = document.querySelector('input[placeholder="NIF"]');

        const name = nameInput.value.trim();
        const firstSurname = firstSurnameInput.value.trim();
        const secondSurname = secondSurnameInput.value.trim();
        const nif = nifInput.value.trim();

        let url = "http://localhost:7238/Patient";
        if (name) {
            url += `?name=${encodeURIComponent(name)}`;
        }

        if (firstSurname) {
            url += `?firstSurname=${encodeURIComponent(firstSurname)}`;
        }

        if (secondSurname) {
            url += `?secondSurname=${encodeURIComponent(secondSurname)}`;
        }

        if (nif) {
            url += `?dni=${encodeURIComponent(nif)}`;
        }

        const response = await fetch(url, {
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

            const container = document.getElementById('patients-container');
            container.innerHTML = '';

            data.forEach(patient => {
                const patientDiv = document.createElement('div');
                patientDiv.className = 'patient-data';

                patientDiv.innerHTML = `
                    <p><strong>Name:</strong> ${patient.name}</p>
                    <p><strong>First Surname:</strong> ${patient.firstSurname}</p>
                    <p><strong>Second Surname:</strong> ${patient.secondSurname}</p>
                    <div class="patient-details" style="display: none;">
                        <p><strong>DNI:</strong> ${patient.dni}</p>
                        <p><strong>Birth Date:</strong> ${patient.birthDate}</p>
                    </div>
                    <button class="toggle-details">View Details</button>
                `;

                const button = patientDiv.querySelector('.toggle-details');
                const details = patientDiv.querySelector('.patient-details');
                button.addEventListener('click', () => {
                    const isVisible = details.style.display === 'block';
                    details.style.display = isVisible ? 'none' : 'block';
                    button.textContent = isVisible ? 'View Details' : 'Hide Details';
                });

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