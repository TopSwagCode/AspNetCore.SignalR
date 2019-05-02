const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:51133/processHub")
    .build();

connection.on("process", (progressValue) => {
    document.getElementById("progressBar").value = progressValue
});

connection.on("processDone", (progressResult) => {
    toggleProggress();
    toggleDownload(progressResult);
});

document.addEventListener("DOMContentLoaded", function(event) {
    document.querySelector('button').addEventListener("click", function(event){
        connection.invoke("StartProcessing").catch(err => console.error(err));
        toggleButton();
        toggleProggress();
    });
  });

connection.start().catch(err => console.error(err));

function toggleProggress() {
    var x = document.getElementById("progressBar");
    if (x.style.display === "none") {
        x.style.display = "block";
    } else {
        x.style.display = "none";
    }
} 

function toggleButton() {
    var x = document.getElementById("processButton");
    if (x.style.display === "none") {
        x.style.display = "block";
    } else {
        x.style.display = "none";
    }
} 

function toggleDownload(progressResult) {
    var x = document.getElementById("download");
    x.href = progressResult;
    if (x.style.display === "none") {
        x.style.display = "block";
    } else {
        x.style.display = "none";
    }
} 