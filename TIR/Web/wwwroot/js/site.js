// Write your JavaScript code.

let connection = new signalR.HubConnection('/hub');

connection.on('refresh', data => {
    location.reload();
});

connection.start();