// chat.js

// Toggle del Sidebar
const toggleButton = document.querySelector('.toggle-button');
const sidebar = document.querySelector('.sidebar');
const chatMain = document.querySelector('.chat-main');

toggleButton.addEventListener('click', () => {
    sidebar.classList.toggle('collapsed');
    chatMain.classList.toggle('collapsed');
    
    // Cambiar la dirección de la flecha
    const icon = toggleButton.querySelector('i');
    if (sidebar.classList.contains('collapsed')) {
        icon.classList.remove('fa-chevron-left');
        icon.classList.add('fa-chevron-right');
    } else {
        icon.classList.remove('fa-chevron-right');
        icon.classList.add('fa-chevron-left');
    }
});



// Manejo de las acciones rápidas
const actionButtons = document.querySelectorAll('.action-button');
const chatMessages = document.getElementById('chatMessages');

actionButtons.forEach(button => {
    button.addEventListener('click', () => {
        const question = button.querySelector('span').textContent.trim();
        addMessage(question, 'user');
        
        // Simular respuesta del sistema (puedes reemplazar esto con tu lógica real)
        setTimeout(() => {
            const response = `Aquí está la información sobre: ${question}`;
            addMessage(response, 'system');
        }, 1000);
    });
});

// Función para agregar mensajes al chat
function addMessage(text, sender) {
    const messageElement = document.createElement('div');
    messageElement.classList.add('message', `${sender}-message`);
    
    const messageContent = document.createElement('div');
    messageContent.classList.add('message-content');
    messageContent.textContent = text;
    
    messageElement.appendChild(messageContent);
    chatMessages.appendChild(messageElement);
    
    // Auto-scroll al último mensaje
    chatMessages.scrollTop = chatMessages.scrollHeight;
}