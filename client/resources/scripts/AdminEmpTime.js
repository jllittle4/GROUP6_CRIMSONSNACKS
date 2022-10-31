// please test out function once you have data in here to ensure it works properly. 
//I have code in place so that when the adming selects and employee, the table is only 
//filled once and cannot be filled again if the submit button is clicked. However I also have a 
//function set to where when she chooses a new employee the function will reset allowing her to 
//populate the tabe again only once though.

//can you check this jeremy?^^ it's not working for me, maybe try adding an event listener? -sam
//if you get it to work, you can delete the reLoad() function at the bottom



//POSSIBLE TO ADD AN OPTIONAL DATE SEARCH FOR ADMIN TO JUST SEARCH FOR A SPECIFIC EMPLOYEE ON A SPECIFIC DATE

//^^added -sam



//base url -jeremy
const baseUrl = 'https://localhost:7139/api/';
const userUrl = baseUrl + 'Users';
let clicked = "";
let dropDown = "";

var ran = 0;
let run = 1;

//dropdown -jeremy
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

//populate dropdown form -sam, jeremy
function getEmployees() {
    let dropDown = document.getElementById('arr');

    fetch(userUrl).then(function (response) {
        return response.json();
    }).then(function (json) {
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

//get report data -sam, jeremy
function handleTableLoad() {
    //if document.getElementById('payroll') = 0, nothing has been selected in the payroll period dropdown, and report won't run
    if (run <= 1 && document.getElementById('payroll').value != 0) {

        //reporting time controller api
        const timeUrl = baseUrl + 'ReportingTime';

        //see ReportRequest.cs in models
        //note the "type" : 'time-events-by-emp', this tells the controller what type of report to return
        //this type of report doesn't need a department, start date, or end date

        const sendReportRequest = {
            "Department": 'default',
            "Employee": document.getElementById('arr').value,
            "PayrollPeriod": document.getElementById('payroll').value,
            "StartDate": 'default',
            "EndDate": 'default',
            "Type": 'time-events-by-emp'
        };

        //call controller post method

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
                //"json" is a list of time event objects
                //the fields currently being returned (which are also in TimeEvent.cs) are...

                // TimeEventId
                // Date
                // ClockIn
                // ClockOut
                // Department                  
                // EmployeeId
                // TotalTime
                // ClockedOutCheck
                
                if (json.length != 0) {
                    loadTable(json);
                } else {
                    //alerts and reloads if no data was returned

                    window.alert('No data was returned.');
                    location.reload();
                }
            });
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

//^^dummy data -jeremy


//append report data -jeremy
function loadTable(times) {
    var myTable = document.getElementById('table');
    myTable.style.display = 'block';

    let table = document.getElementById('table');
    table.border = '1';

    let tableBody = document.createElement('TBODY');
    tableBody.id = 'TableBody';
    table.appendChild(tableBody);

    times.forEach((time) => {
        //see timeevent object properties above in handleTableLoad() function
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

//return to admin home -jeremy
function retToHome() {
    window.location = "AdminHome.html";
}

//reload page to clear report -sam
function reLoad() {
    location.reload();
}
