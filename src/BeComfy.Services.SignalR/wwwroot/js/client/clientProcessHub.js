"use strict";

var connection = new signalR.HubConnectionBuilder()
                    .withUrl("/processHub")
                    .configureLogging(signalR.LogLevel.Information)
                    .build();

document.getElementById("connect").addEventListener("click", function (event) {
    connection.start().then(function () {
        var jwt = document.getElementById("jwtInput").value;
        if (jwt === "")
            return;
    
        connection.invoke("Authenticate", jwt);
    }).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

connection.on("connected", function () {
    var li = document.createElement("li");
    li.textContent = "User connected!";
    document.getElementById("messagesList").appendChild(li);
})

connection.on("disconnected", function () {
    var li = document.createElement("li");
    li.textContent = "User disconnected!";
    document.getElementById("messagesList").appendChild(li);
})