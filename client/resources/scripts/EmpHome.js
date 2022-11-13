`use strict`;
var datetime = new Date();
document.getElementById("time").textContent = datetime; //it will print on html page

const baseUrl = 'https://mis321-fall2022-riddlemes4m.herokuapp.com/api';
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

//all of the above work is jeremy



getAllDepartments();

//get list of departments for departments selector - sam
function getAllDepartments() {
    //departments controller api
    const depUrl = baseUrl + 'Departments';

    //call departments controller get method
    fetch(depUrl).then(function (response) {
        return response.json();
    }).then(function (json) {
        addDepOptions(json);
    });
}

//populate departments selector -sam, jeremy
function addDepOptions(json) {
    let selector = document.getElementById('ratingInput');

    json.forEach((department) => {
        let depOption = document.createElement('option');
        depOption.appendChild(document.createTextNode(department.depName));
        selector.appendChild(depOption);
    });
}



//determine whether button should display clock in or clock out - sam, jeremy
function handleButtonLoad() {
    var btn = document.getElementById("timePunch");

    //clocking time controller api
    //id of 0 to tell controller to find most recent time event for logged in employee (handled completely in the backend)
    const timeUrl = baseUrl + 'ClockingTime/0';

    //call clocking time controller get/id method
    fetch(timeUrl).then(function (response) {
        return response.json();
    }).then(function (json) {
        //"json" one time event objects
        //the fields currently being returned (which are also in TimeEvent.cs) are...

        // TimeEventId
        // Date
        // ClockIn
        // ClockOut
        // Department                  
        // EmployeeId
        // TotalTime
        // ClockedOutCheck

        let clockedout = json.clockedOutCheck;
        return clockedout;
    }).then(function (clockedout) {
        if (clockedout == 'n') {
            btn.value = 'Clock Out';
        } else {
            btn.value = 'Clock In';
        };
    });
}



//clocking in - sam
function createTimeEvent() {
    //clocking time controller api
    const postUrl = baseUrl + 'ClockingTime';

    //see TimeEvent.cs in models
    //this is just a generic clock in event for the logged in employee, so no fields  are required, (ids, dates, times, etc. handled completely in the backend)

    const sendEvent = {
        "TimeEventId": 0,
        "Date": 'default',
        "ClockIn": 'default',
        "ClockOut": 'default',
        "DepartmentId": 0,
        "EmployeeId": 0,
        "TotalTime": 'default'
    };

    //call clocking time controller post method
    fetch(postUrl, {
        method: 'POST',
        headers: {
            "Accept": 'application/json',
            "Content-Type": 'application/json'
        },
        body: JSON.stringify(sendEvent)
    }).then((response) => {
        if (response.status == 200) {
            window.alert(`You have successfully clocked in.`);
        } else {
            window.alert('There was an error clocking in.');
        }
    });
}



//clocking out - sam
function closeTimeEvent() {
    //clocking time controller api
    //id of 0 because time event id is not needed, it will clock out the most recent time event for that employee (handled completely in the backend)
    const putUrl = baseUrl + 'ClockingTime/0';

    //see TimeEvent.cs in models
    //only department field is required, since the department is submitted on clock out, no ids, times, or dates needed (handled completely in the backend)

    const sendEvent = {
        "TimeEventId": 0,
        "Date": 'default',
        "ClockIn": 'default',
        "ClockOut": 'default',
        "Department": document.getElementById('ratingInput').value,
        "EmployeeId": 0,
        "TotalTime": 'default'
    };

    //call clockingtime controller put method

    fetch(putUrl, {
        method: 'PUT',
        headers: {
            "Accept": 'application/json',
            "Content-Type": 'application/json'
        },
        body: JSON.stringify(sendEvent)
    }).then((response) => {
        if (response.status == 200) {
            window.alert(`You have successfully clocked out.`);
        } else {
            window.alert('There was an error clocking out.');
        }
    }).then(function () {
        //currently reloading page after clockout request submitted to get rid of modal, maybe you don't need this, jeremy?

        location.reload();
    });
}

//all of the below work is jeremy

let btn = document.getElementById("timePunch");
let modal = document.querySelector(".modal");
let closeBtn = document.querySelector(".close-btn");

btn.onclick = function () {
    if (btn.value == "Clock In") {
        btn.value = "Clock Out";
        btn.innerHTML = "Clock Out";
        createTimeEvent(); //sam's clockin function
    }
    else {
        btn.value = "Clock In";
        btn.innerHTML = "Clock In";
        modal.style.display = "block";
    }
};

closeBtn.onclick = function () {
    modal.style.display = "none";
};

window.onclick = function (e) {
    if (e.target == modal) {
        modal.style.display = "none";
    }
};

function retToLogin() {
    window.location = "LogIn.html";
}