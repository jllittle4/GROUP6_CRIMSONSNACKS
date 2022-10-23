`use strict`;
var datetime = new Date();
let timeeventtime = new Date().toLocaleTimeString();
let timeeventdate = new Date().toLocaleDateString();

//console.log(timeeventdate);
timeeventdate = timeeventdate.slice(6, 10) + '-' + timeeventdate.slice(0, 2) + '-' + timeeventdate.slice(3, 5);
//console.log(timeeventdate);
//console.log(datetime);
document.getElementById("time").textContent = datetime; //it will print on html page
const baseUrl = 'https://localhost:7139/api/'; //sam



let username = window.localStorage.getItem('username');
let header = document.getElementById('welcome');
header.textContent = 'Welcome, ' + username;
//console.log(username);
let empid = getEmployeeID();
let timeeventid = 0;
let departmentid = 0;
let allDeps = [];

function refreshTime() {
    const timeDisplay = document.getElementById("time");
    const dateString = new Date().toLocaleString();
    const formattedString = dateString.replace(", ", " - ");
    timeDisplay.textContent = formattedString;
}
setInterval(refreshTime, 60000);

setInterval(refreshTime, 1000);

getAllDepartments();


function handleClick() {
    var btn = document.getElementById("timePunch");
    //getAllDepartments();
    if (btn.value == "Clock In") {
        btn.value = "Clock Out";
        btn.innerHTML = "Clock Out";
        //getAllDepartments();
        //createTimeEvent(); //sam
    }
    else {
        btn.value = "Clock In";
        btn.innerHTML = "Clock In";

        // getDepartment();
        //closeTimeEvent();
    }
}

//sam
function createTimeEvent() {
    //console.log('made it here');
    const postUrl = baseUrl + 'TimeKeeping';
    //console.log(postUrl);
    // let employeeid = getEmployeeID();
    //console.log(empid);
    //console.log(driver);
    if (empid != -1) {
        //console.log('this was true');
        const sendEvent = {
            "TimeEventId": 0,
            "Date": timeeventdate,
            "ClockIn": timeeventtime,
            "ClockOut": timeeventtime,
            "DepartmentId": null,
            "EmployeeId": empid,
            "TotalTime": null
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
            //console.log('Response from the save', response);
        });
        getTimeEventId();
    }
}

//sam - find employeeid
function getEmployeeID() {
    const usersUrl = baseUrl + 'Users';
    //let empid = -1;

    fetch(usersUrl).then(function (response) {
        //console.log(response);
        return response.json();
    }).then(function (json) {
        //console.log(json);
        //console.log(json.length);
        //console.log(json[0].userId);
        //var results = []
        //var searchVal = username;
        for (var i = 0; i < json.length; i++) {
            if (json[i].userName == username) {
                //results.push(json[i]);
                empid = json[i].userId;
            }
        }
        //empid = results.userId;
    });
    // console.log(empid);
    // return empid;
}

function getTimeEventId() {
    const timeUrl = baseUrl + 'TimeKeeping';

    console.log('made it here again again again');
    findeventiddate = new Date().toDateString();
    console.log(timeeventdate);
    fetch(timeUrl).then(function (response) {
        return response.json();
    }).then(function (json) {
        console.log(json);
        for (var i = 0; i < json.length; i++) {
            if (json[i].date == timeeventdate && json[i].employeeId == empid) {
                timeeventid = json[i].timeEventId;
                console.log(json[i].timeEventId);
                break;
            }
        }
    });
}

function getDepartment() {
    let selection = document.getElementById('ratingInput');
    //alert( selection.options[selection.selectedIndex].value);

    for (var i = 0; i < allDeps.length; i++) {
        if (allDeps[i].depName == selection.options[selection.selectedIndex].value) {
            departmentid = allDeps[i].depId;
        }
    }
}

function closeTimeEvent() {
    //console.log('made it here as well smarty pants');
    //getTimeEventId();
    getDepartment();
    console.log(timeeventid);
    //console.log(timeeventid,timeeventdate,timeeventtime,departmentid,empid);
    const sendEvent = {
        "TimeEventId": timeeventid,
        "Date": timeeventdate,
        "ClockIn": timeeventtime,
        "ClockOut": timeeventtime,
        "DepartmentId": departmentid,
        "EmployeeId": empid,
        "TotalTime": "clock-out"
    };


    const putUrl = baseUrl + 'TimeKeeping/' + timeeventid;

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
        //console.log('Response from the update', response);
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
        //closeTimeEvent();
        // getDepartment();
    }
    // if(btn.value == "Clock Out"){
    //     modal.style.display = "block";
    // }
};

closeBtn.onclick = function () {
    modal.style.display = "none";
};

window.onclick = function (e) {
    if (e.target == modal) {
        modal.style.display = "none";
    }
};

// function get () {
//     // (A) GET THE PARAMETERS
//     var params = new URLSearchParams(window.location.search),
//         username = params.get("username");
//         window.localStorage.getItem('username');

//     // (B) IT WORKS!
//     console.log(username);  // Foo Bar
//      // ["Hello", "World"]
//      return username;
//   }

// function getDepartment(){
//     let dept = prompt("Please enter the Department you worked");




//     switch(dept.toLowerCase()){
//         case "stocking":

//             console.log(capFirst(dept.toLowerCase()));
//             alert('I made it');
//         break;
//         case "marketing":

//             console.log(capFirst(dept.toLowerCase()));
//             alert('I made it');
//         break;
//         case "ordering":

//             console.log(capFirst(dept.toLowerCase()));
//             alert('I made it');
//         break;
//         case "sales":

//             console.log(capFirst(dept.toLowerCase()));
//             alert('I made it ');
//         break;
//         default:
//             alert('Invalid input. Please enter one of the folowing: \t Stocking, Marketing, Ordering, Sales');
//             getDepartment();
//         break;

//     }
//     function capFirst(str) {
//         return str[0].toUpperCase() + str.slice(1);
//     }
// if (dept = "stocking" || "marketing" || "ordering" || "sales"){
//     dept.toUpperCase;
//     console.log(dept);
//     alert('I made it bitch');
// }
// else{

//     alert('Invalid input. Please enter one of the folowing: \t Stocking, Marketing, Ordering, Sales');
//     getDepartment();
// }





// function loadTable(){
//     let table = document.getElementById('table');
//     table.border = '1';
//     let tableBody = document.createElement('TBODY');
//     tableBody.id = 'TableBody';
//     table.appendChild(tableBody);
//     times.forEach((time) => {
//         let tr = document.createElement('TR');
//         tableBody.appendChild(tr);

//         let td1 = document.createElement('TD');
//         td1.width = 300;
//         td1.appendChild(document.createTextNode(time.date));
//         tr.appendChild(td1);


//         let td2 = document.createElement('TD');
//         td2.width = 500;
//         td2.appendChild(document.createTextNode(time.clockIn));
//         tr.appendChild(td2);


//         let td3 = document.createElement('TD');
//         td3.width = 150;
//         td3.appendChild(document.createTextNode(time.clockOut));
//         tr.appendChild(td3);

//         let td4 = document.createElement('TD');
//         td4.width = 150;
//         td4.appendChild(document.createTextNode(time.dept));
//         tr.appendChild(td4);

//         let td5 = document.createElement('TD');
//         td5.width = 150;
//         td5.appendChild(document.createTextNode(time.totalTime));
//         tr.appendChild(td5);

//     }
//     )}

function retToLogin() {
    window.location = "LogIn.html";
}

function getAllDepartments() {
    const depUrl = baseUrl + 'Departments';

    console.log('made it here');
    fetch(depUrl).then(function (response) {
        //console.log(response);
        return response.json();
    }).then(function (json) {
        //console.log(json);

        // json.forEach((department) =>{
        //     addDepOption(department);
        // })

        addDepOptions(json);
        allDeps = json;
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
// function show_list() {
//     var courses = document.getElementById("courses_id");

//     if (courses.style.display == "block") {
//         courses.style.display = "none";
//     } else {
//         courses.style.display = "block";
//     }
// }
// window.onclick = function (event) {
//     if (!event.target.matches('.dropdown_button')) {
//         document.getElementById('courses_id')
//             .style.display = "none";
//     }
// }   




