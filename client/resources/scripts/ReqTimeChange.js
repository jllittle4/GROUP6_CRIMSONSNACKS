const baseUrl = "https://localhost:7139/api/"; //sam
let username = window.localStorage.getItem('username');


let empid = -1;
getEmployeeID();
let allDeps = [];
getAllDepartments();
let departmentid = -1;


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

    getDepartment();

    //console.log(date.value, dept.value, clockIn.value, clockOut.value, reason.value);

    const sendReq = {
        "requestId": -1,
        "employeeId": empid,
        "date": date.value,
        "departmentId": departmentid,
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

function getEmployeeID() {
    const usersUrl = baseUrl + "Users";
    empid = -1;
  
    fetch(usersUrl)
      .then(function (response) {
        return response.json();
      })
      .then(function (json) {
        for (var i = 0; i < json.length; i++) {
          if (json[i].userName == username) {
            empid = json[i].userId;
            console.log(empid);
            //return empid;
          }
        }
      });
    // console.log(empid);
    //return empid;
  }

  function getDepartment() {
    let selection = document.getElementById('dept');
    //alert( selection.options[selection.selectedIndex].value);

    for (var i = 0; i < allDeps.length; i++) {
        if (allDeps[i].depName == selection.options[selection.selectedIndex].value) {
            departmentid = allDeps[i].depId;
            console.log(departmentid);
        }
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