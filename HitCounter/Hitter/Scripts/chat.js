﻿"use strict";

//var connection = new signalR.HubConnectionBuilder().withUrl("/ChatHub").build();
var contosoChatHubProxy = $.hubConnection();
//var connection = $.hubConnection();
var connection = contosoChatHubProxy.createHubProxy('chatMessage');
//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("send", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

contosoChatHubProxy.start(function () {
    document.getElementById("sendButton").disabled = false;
});

//    .catch(function (err) {
//    return console.error(err.toString());
//});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});