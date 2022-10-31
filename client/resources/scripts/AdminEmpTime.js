// please test out function once you have data in here to ensure it works properly. 
//I have code in place so that when the adming selects and employee, the table is only 
//filled once and cannot be filled again if the submit button is clicked. However I also have a 
//function set to where when she chooses a new employee the function will reset allowing her to 
//populate the tabe again only once though.

//can you check this jeremy?^^ it's not working for me, maybe try adding an event listener?




//POSSIBLE TO ADD AN OPTIONAL DATE SEARCH FOR ADMIN TO JUST SEARCH FOR A SPECIFIC EMPLOYEE ON A SPECIFIC DATE




//let employees = [];
const baseUrl = 'https://localhost:7139/api/'; /// ENTER BASE URL HERE
const userUrl = baseUrl + 'Users';
let clicked = "";
let dropDown = "";

var ran = 0;
let run = 1;


function handleOnLoad() {
    if (ran <= 1) {
        let dropDown = document.getElementById('arr');
        dropDown.length = 0;

        let defaultOption = document.createElement('option');
        defaultOption.text = 'Choose Employee';

        dropDown.add(defaultOption);

        dropDown.selectedIndex = 0;
        getEmployees();
        ran++;
    }
    return ran;
}

//SIMPLY POPULATING FORM FOR DROPDOWN TO SELECT AN EMPLOYEE
function getEmployees() {
    let dropDown = document.getElementById('arr');

    fetch(userUrl).then(function (response) {
        //console.log(response);
        return response.json();
    }).then(function (json) {
        //console.log(json);
        for (let i = 0; i < json.length; i++) {
            let option = document.createElement('option');
            let name = `${json[i].firstName} ${json[i].lastName}`;
            option.text = name;
            option.value = json[i].userId;

            dropDown.appendChild(option);

            run = 1;
        };
    });

    return dropDown;
}

function handleTableLoad() {
    if (run <= 1 && document.getElementById('payroll').value != 0) {
        const timeUrl = baseUrl + 'ReportingTime';
        //console.log(document.getElementById('arr').value);
        //console.log(document.getElementById('payroll').value);

        const sendReportRequest = {
            "Department": 'default',
            "Employee": document.getElementById('arr').value,
            "PayrollPeriod": document.getElementById('payroll').value,
            "StartDate": 'default',
            "EndDate": 'default',
            "Type" : 'time-events-by-emp'
        };

        fetch(timeUrl, {
            method: 'POST',
            headers: {
                "Accept": 'application/json',
                "Content-Type": 'application/json'
            },
            body: JSON.stringify(sendReportRequest)
        })
            .then(function (response) {
                return response.json();
            })
            .then(function (json) {
                //console.log(json);
                if(json.length != 0){
                    loadTable(json);
                } else {
                    window.alert('No data was returned.');
                    location.reload();
                }
            });
        //loadTable();
        run++;
    } else {
        window.alert('Report could not be run');
    }
    return run;
}



// let times = [
//     { 'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5' },
//     { 'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5' },
//     { 'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5' },
//     { 'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5' },
//     { 'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5' },
//     { 'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5' },
//     { 'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5' },
// ];


// NEED TO FILL ARRAY FOR TIMES OF A SPECIFIC EMPLOYEE THATS SELECTED FROM THE DROPDOWN
function loadTable(times) {
    var myTable = document.getElementById('table');
    myTable.style.display = 'block';

    let table = document.getElementById('table');
    table.border = '1';

    let tableBody = document.createElement('TBODY');
    tableBody.id = 'TableBody';
    table.appendChild(tableBody);

    times.forEach((time) => {
        let tr = document.createElement('TR');
        tableBody.appendChild(tr);

        let td1 = document.createElement('TD');
        td1.width = 150;
        td1.appendChild(document.createTextNode(time.date));
        tr.appendChild(td1);


        let td2 = document.createElement('TD');
        td2.width = 150;
        td2.appendChild(document.createTextNode(time.clockIn));
        tr.appendChild(td2);


        let td3 = document.createElement('TD');
        td3.width = 150;
        td3.appendChild(document.createTextNode(time.clockOut));
        tr.appendChild(td3);

        let td4 = document.createElement('TD');
        td4.width = 150;
        td4.appendChild(document.createTextNode(time.department));
        tr.appendChild(td4);

        let td5 = document.createElement('TD');
        td5.width = 150;
        td5.appendChild(document.createTextNode(time.totalTime));
        tr.appendChild(td5);

    });
}

function retToHome() {
    window.location = "AdminHome.html";
}

function reLoad() {
    location.reload();
}
