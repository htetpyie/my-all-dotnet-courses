"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/balanceHub").build();

getCurrentBalance();

connection.on("RecieveBalance", function (balance) {
    setCurrentBalance(balance.value);
});

connection.start().then(function () {
    //something that starts connection
}).catch(function (err) {
    return console.error(err.toString());
});

function getCurrentBalance() {
    $.ajax({
        url: "/Dashboard/CurrentBalance",
        type: "GET",
        dataType: "json",
        success: function (result) {
            setCurrentBalance(result)
        },
        error: function (err) {
            console.error(err);
        }
    });
}

function setCurrentBalance(balance) {
    $('.header #currentBalance').text(balance);
}
