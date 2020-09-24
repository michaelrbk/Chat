"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/Chat").build();
var countMsg = 0;
var maxMsg = 50; //Show only the last x messages
//Disable send button until connection is established  
document.getElementById("sendBtn").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&").replace(/</g, "<").replace(/>/g, ">");
    var encodedMsg = user + "-" + new Date().toUTCString() + " :" + msg;
    var li = document.createElement("li");
    countMsg++;
    if (countMsg >= maxMsg) {
        document.getElementById("ulmessages").lastChild.remove();
    };
    li.textContent = encodedMsg;
    document.getElementById("ulmessages").prepend(li);

});

connection.start().then(function () {
    document.getElementById("sendBtn").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendBtn").addEventListener("click", function (event) {
    var message = document.getElementById("txtmessage").value;
    connection.invoke("SendMessage", message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});  