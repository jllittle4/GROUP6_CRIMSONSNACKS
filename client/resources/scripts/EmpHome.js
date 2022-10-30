`use strict`;
var datetime = new Date();
document.getElementById("time").textContent = datetime; //it will print on html page

const baseUrl = 'https://localhost:7139/api/'; //sam
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



//populate list of departments in departments selector - sam
getAllDepartments();

function getAllDepartments() {
    const depUrl = baseUrl + 'Departments';

    fetch(depUrl).then(function (response) {
        return response.json();
    }).then(function (json) {
        addDepOptions(json);
        //allDeps = json;
    });
}

function addDepOptions(json) {
    let selector = document.getElementById('ratingInput');

    json.forEach((department) => {
        let depOption = document.createElement('option');
        depOption.appendChild(document.createTextNode(department.depName));
        selector.appendChild(depOption);
    });
}



//determine whether button should display clock in or clock out - sam
function handleButtonLoad() {
    var btn = document.getElementById("timePunch");
    const timeUrl = baseUrl + 'TimeKeeping/0';

    fetch(timeUrl).then(function (response) {
        return response.json();
    }).then(function (json) {
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
    const postUrl = baseUrl + 'TimeKeeping';


    const sendEvent = {
        "TimeEventId": 0,
        "Date": 'default',
        "ClockIn": 'default',
        "ClockOut": 'default',
        "DepartmentId": 0,
        "EmployeeId": 0,
        "TotalTime": 'default'
    };

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
        }
    });
}



//clocking out - sam
function closeTimeEvent() {
    const putUrl = baseUrl + 'TimeKeeping/0';

    
    const sendEvent = {
        "TimeEventId": 0,
        "Date": 'default',
        "ClockIn": 'default',
        "ClockOut": 'default',
        "Department": document.getElementById('ratingInput').value,
        "EmployeeId": 0,
        "TotalTime": 'default'
    };

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
        }
    }).then(function () {
        location.reload();
    });
}



let btn = document.getElementById("timePunch");
let modal = document.querySelector(".modal");
let closeBtn = document.querySelector(".close-btn");

btn.onclick = function () {
    if (btn.value == "Clock In") {
        btn.value = "Clock Out";
        btn.innerHTML = "Clock Out";
        createTimeEvent(); //sam
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