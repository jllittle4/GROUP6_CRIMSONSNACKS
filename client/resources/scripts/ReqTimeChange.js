//base url -jeremy
const baseUrl = "https://mis321-fall2022-riddlemes4m.herokuapp.com/api";
let username = window.localStorage.getItem('username');

let allDeps = [];
getAllDepartments();

//return to employee home -jeremy
function retToHome() {
    window.location = 'EmpHome.html';
}

//submit request  form -sam, jeremy
function onSubmit() {
    const postUrl = baseUrl + 'Requests';

    let date = document.getElementById('date');
    let dept = document.getElementById('dept');
    let clockIn = document.getElementById('clockIn');
    let clockOut = document.getElementById('clockOut');
    let reason = document.getElementById('reason');

    //see Request.cs
    //the only fields required to be filled are "date","department","clockIn","clockOut","reason",and "status"

    const sendReq = {
        "requestId": -1,
        "employeeId": -1,
        "date": date.value,
        "departmentid": -1,
        "department": dept.value,
        "clockIn": clockIn.value,
        "clockOut": clockOut.value,
        "reason": reason.value,
        "status": 'Pending',
        "totalTime": 'default'
    };

    //make sure that no field is blank
    if (reason.value != '' && clockIn.value != '' && clockOut.value != '' && date.value != '' && dept.value != 'Choose A Department') {
        //call requests controller post method
        fetch(postUrl, {
            method: 'POST',
            headers: {
                "Accept": 'application/json',
                "Content-Type": 'application/json'
            },
            body: JSON.stringify(sendReq)
        }).then((response) => {
            if (response.status == 200) {
                window.alert('Request successfully submitted!');
            }
            //console.log('response from the save ', response);
        }).then(function () {
            //once user has submitted request, they are returned to their home page
            retToHome();
        });
    } else {
        alert('All fields are required');
    }
}

//get list of departments for departments selector - sam
function getAllDepartments() {
    //departments controller api
    const depUrl = baseUrl + 'Departments';

    //call departments controller get method
    fetch(depUrl).then(function (response) {
        return response.json();
    }).then(function (json) {
        addDepOptions(json);
        allDeps = json;
    });
}

//populate departments selector -sam, jeremy
function addDepOptions(json) {
    let selector = document.getElementById('dept');

    json.forEach((department) => {
        let depOption = document.createElement('option');
        depOption.appendChild(document.createTextNode(department.depName));
        selector.appendChild(depOption);
    });
}