const baseUrl = "https://localhost:7139/api/"; //sam
let username = window.localStorage.getItem('username');

let allDeps = [];
getAllDepartments();


function retToHome() {
    window.location = 'EmpHome.html';
}

function onSubmit() {
    const postUrl = baseUrl + 'Requests';

    let date = document.getElementById('date');
    let dept = document.getElementById('dept');
    let clockIn = document.getElementById('clockIn');
    let clockOut = document.getElementById('clockOut');
    let reason = document.getElementById('reason');


    //console.log(date.value, dept.value, clockIn.value, clockOut.value, reason.value);

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
    console.log(clockIn.value, reason.value);
    if(reason.value != '' && clockIn.value != '' && clockOut.value != '' && date.value != '' && dept.value != 'Choose A Department'){
        fetch(postUrl, {
            method: 'POST',
            headers: {
                "Accept": 'application/json',
                "Content-Type": 'application/json'
            },
            body: JSON.stringify(sendReq)
        }).then((response) => {
            if (response.status == 200) {
                window.alert('User has been saved');
            }
            console.log('response from the save ', response);
        }).then(function() {
            retToHome();
        });
    } else {
        alert('All fields are required')
    }

    
}

function getAllDepartments() {
    const depUrl = baseUrl + 'Departments';

    //console.log('made it here');
    fetch(depUrl).then(function (response) {
        //console.log(response);
        return response.json();
    }).then(function (json) {
       

        addDepOptions(json);
        allDeps = json;
    });
}

function addDepOptions(json) {
    let selector = document.getElementById('dept');

    json.forEach((department) => {
        let depOption = document.createElement('option');
        depOption.appendChild(document.createTextNode(department.depName));
        selector.appendChild(depOption);
    });

}