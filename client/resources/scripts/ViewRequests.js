//base url -jeremy
const baseUrl = "https://localhost:7139/api/";
//requests controller api -sam
const requestsUrl = baseUrl + 'Requests';

//load all three tables when page loads
function handleOnLoad() {
    //arguments described at loadTable() function 
    loadTable('pending');
    loadTable('approved');
    loadTable('denied');

    //arguments described at getRequests() function
    getRequests('pending');
    getRequests('approved');
    getRequests('denied');
}

//get requests data for each table -sam
//"type" argument is the criteria for sorting out which requests to add to each table
function getRequests(type) {
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

            //below is a condition to sort out all the requests by the "Status" of each request, based on the "type" passed in, and add those requests to the appropriate table
            json.forEach(request => {
                if (request.status.toLowerCase() == type) {
                    addRow(type, request);
                }
            });
        });
}

//load each requests table -sam, jeremy
//"type" argument is the tableid for each table that is created on load
function loadTable(type) {
    let table = document.getElementById(type);
    table.border = '1';
    let tableBody = document.createElement("TBODY");
    tableBody.id = `${type}-tbody`;
    console.log(tableBody.id);
    table.appendChild(tableBody);
}

//add data to each table -sam, jeremy
//"type" argument allows the function to find the table body id, "request" argument is the object to add to the table
function addRow(type, request) {
    let tableId = `${type}-tbody`;
    let tableBody = document.getElementById(tableId);
    let tr = document.createElement("TR");
    tableBody.appendChild(tr);

    let td1 = document.createElement("TD");
    td1.width = 150;
    td1.appendChild(document.createTextNode(request.employeeName));
    tr.appendChild(td1);

    let td2 = document.createElement("TD");
    td2.width = 150;
    td2.appendChild(document.createTextNode(request.date));
    tr.appendChild(td2);

    let td3 = document.createElement("TD");
    td3.width = 150;
    td3.appendChild(document.createTextNode(request.clockIn));
    tr.appendChild(td3);

    let td4 = document.createElement("TD");
    td4.width = 150;
    td4.appendChild(document.createTextNode(request.clockOut));
    tr.appendChild(td4);

    let td5 = document.createElement("TD");
    td5.width = 200;
    td5.appendChild(document.createTextNode(request.department));
    tr.appendChild(td5);

    let td6 = document.createElement("TD");
    td6.width = 400;
    td6.appendChild(document.createTextNode(request.reason));
    tr.appendChild(td6);
}

//return to admin dashboard -jeremy
function retToHome() {
    window.location = 'AdminHome.html';
}