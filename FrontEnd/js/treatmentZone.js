window.onload = function() {
    const token = sessionStorage.getItem("jwtToken");
    
    if (!token) {
        window.location.href = "index.html";
        return;
    }

    const patientData = sessionStorage.getItem("patientData");

    const patient = JSON.parse(patientData);

    const container = document.getElementById('patientName-container');

    const patientDiv = document.createElement('div');

    patientDiv.innerHTML = `
        <p><strong>Patient: ${patient.name} ${patient.firstSurname} ${patient.secondSurname}</strong></p>
    `;

    container.appendChild(patientDiv);

    showSection('date-card');   

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

