const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:51133/graphHub")
    .build();

connection.on("LogWork", (work) => {
    console.log(work);
    myData.push(work);
    if (myData.length > 50) {
        myData.shift();
    }

    myChart.update();
});

connection.start().catch(err => console.error(err));

connection.onclose(function () {
    console.log("closing connection");
    timeoutConnection();
});

function timeoutConnection(){
    setTimeout(function(){ startConnection(); }, 3000);
}

function startConnection(){
    connection.start().catch(err => timeoutConnection());
}

