"use strict";

var connection = new signalR.HubConnectionBuilder()
                    .withUrl("/processHub")
                    .configureLogging(signalR.LogLevel.Information)
                    .build();

document.getElementById("btnLogin").addEventListener("click", function (event) {
        connection.start().then(function () {
            var email = document.getElementById("userEmail").value;
            var password = document.getElementById("userPassword").value;

            var request = new XMLHttpRequest();
            request.open("POST", "http://localhost:5010/sign-in", false);
            request.setRequestHeader("Content-type", "application/json");
            request.setRequestHeader("Accept", "*/*");

            var person = 
            {
                email: email, 
                password: password
            };

            request.send(JSON.stringify(person));
            var response = JSON.parse(request.responseText);

            if(request.status == 200) {
                connection.invoke("InitializeAsync", response.accessToken);
            }

            document.getElementById("userJWT").append("JWT: " + response.accessToken)
            document.getElementById("userJWTExpires").append("Expires: " + convertToTime(response.expires))
            document.getElementById("userJWTId").append("UID: " + response.id);

        }).catch(function (err) {
            return console.error(err.toString());
        });
    event.preventDefault();
});

function convertToTime(unixTimestamp) { 
    return new Date(unixTimestamp).toLocaleTimeString()
}

connection.on("connected", function (userGroup) {
    var li = document.createElement("li");
    li.textContent = "User connected!";
    document.getElementById("messagesList").appendChild(li);

    document.getElementById("userGroup").append("UserGroup: " + userGroup);
})

connection.on("disconnected", function () {
    var li = document.createElement("li");
    li.textContent = "User disconnected!";
    document.getElementById("messagesList").appendChild(li);
})

// Operation result

connection.on("operation_result_success", function (operationResult) {
    var li = document.createElement("li");
    li.textContent = JSON.stringify(operationResult);
    document.getElementById("messagesList").appendChild(li);
})

connection.on("operation_result_rejected", function (operationResult) {
    var li = document.createElement("li");
    li.textContent = JSON.stringify(operationResult);
    document.getElementById("messagesList").appendChild(li);
})

// End of Operation result