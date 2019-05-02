const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:51133/stockHub")
    .build();

connection.on("UpdateStocks", (stocksJson) => {
    var stocks = JSON.parse(stocksJson);
    stocks.forEach(stock => {
        updateStock(stock);
    });

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

function updateStock(stock){
    var stockElement = document.getElementById(stock.symbol);
    if(stockElement){
        stockElement.cells[0].innerHTML = stock.symbol;
        stockElement.cells[1].innerHTML = stock.bid;
        stockElement.cells[2].innerHTML = stock.ask;
        
        // Update background color
        stockElement.style.backgroundColor = "yellow";

        // SetTimeout to revert color
        setTimeout(function(){ UpdateBackgroundColor(stockElement); }, 1500);
    }
    else{
        var table = document.getElementById("stocksTable");
        console.log(table);
        var row = table.insertRow(1);
        row.id = stock.symbol;
        var symbolCell = row.insertCell(0);
        symbolCell.innerHTML = stock.symbol;

        var bidCell = row.insertCell(1);
        bidCell.innerHTML = stock.bid;
        
        var askCell = row.insertCell(2);
        askCell.innerHTML = stock.ask;
    }
}

function UpdateBackgroundColor(element){
    element.style.backgroundColor = "white";
}