window.onload = function() {
    const token = sessionStorage.getItem("jwtToken");
    
    if (!token) {
        window.location.href = "index.html";
        return;
    }

    const patientData = sessionStorage.getItem("patientData");

    if (patientData) {
        const patient = JSON.parse(patientData);

        const container = document.getElementById('patientName-container');

        const patientDiv = document.createElement('div');

        patientDiv.innerHTML = `
            <p><strong>Paciente: ${patient.name} ${patient.firstSurname} ${patient.secondSurname}.</strong></p>
        `;

        container.appendChild(patientDiv);
    } else {
        console.log("No se encontraron datos del paciente en el sessionStorage.");
    }
};

