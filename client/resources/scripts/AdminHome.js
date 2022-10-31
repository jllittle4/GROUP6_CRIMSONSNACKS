`use strict`;
var datetime = new Date();
document.getElementById("time").textContent = datetime;

let username = window.localStorage.getItem('username');

let header = document.getElementById('welcome');
header.textContent = 'Welcome, ' + username;

function refreshTime() {
    const timeDisplay = document.getElementById("time");
    const dateString = new Date().toLocaleString();
    const formattedString = dateString.replace(", ", " - ");
    timeDisplay.textContent = formattedString;
}

setInterval(refreshTime, 60000);
setInterval(refreshTime, 1000);

function handleClick() {
    window.location = 'ViewRequests.html';
}