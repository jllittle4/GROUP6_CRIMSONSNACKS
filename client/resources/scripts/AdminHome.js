`use strict`;

//everything below is jeremy's work, sam just put it in its own js file
const baseUrl = "https://localhost:7139/api/";

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

//display notification if there are new pending requests -sam
function adminHandleOnLoad() {
    //requests controller api
    const requestsUrl = baseUrl + 'Requests';

    //call requests controller get method
    fetch(requestsUrl)
        .then(function (response) {
            return response.json();
        })
        .then(function (json) {
            //json is a list of all requests
            //see Request.cs, the fields being returned are...

            // RequestId
            // Date 
            // ClockIn 
            // ClockOut 
            // Reason 
            // DepartmentId
            // EmployeeId
            // Status 
            // TotalTime 
            // Department 
            // EmployeeName

            //below is a condition to find if there are any pending requests, and if so, an alert will be displayed
            for (var i = 0; i < json.length; i++) {
                if (json[i].status.toLowerCase() == 'pending') {
                    window.alert('There are new time change requests to review');
                    break;
                }
            }
        });

}