const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:51133/chatHub")
    .build();

connection.on("ReceiveMessage", (user, message) => { 
    const encodedMsg = user + " says: " + message;
    const li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

document.getElementById("sendButton").addEventListener("click", event => {
    const user = document.getElementById("userInput").value;
    const message = document.getElementById("messageInput").value;    
    connection.invoke("SendMessage", user, message).catch(err => console.error(err));
    event.preventDefault();
    document.getElementById("messageInput").value = "";
});

connection.start().catch(err => console.error(err));