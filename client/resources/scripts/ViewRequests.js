//base url -jeremy
const baseUrl = "https://localhost:7139/api/";
//requests controller api -sam
const requestsUrl = baseUrl + 'Requests';

//global array of possible time events -jeremy
let pendingReq = [];
//global variable for request -sam
let globalRequest = {};

//load all three tables when page loads
function handleOnLoad() {
    let modal = document.querySelector(".modal");
    modal.style.display = "none";
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
    if (type == 'pending') {
        pendingReq.push(request);

        let td7 = document.createElement("TD");
        td7.id = 'check';
        td7.width = 400;

        let approve = document.createElement('input');
        approve.type = 'button';
        approve.value = 'Approve';
        approve.style = 'margin: 10px;';
        approve.id = request.requestId;
        approve.addEventListener("click", first, false);

        let deny = document.createElement('input');
        deny.type = 'button';
        deny.value = 'Deny';
        deny.style = 'margin: 10px;';
        deny.id = request.requestId;
        deny.addEventListener("click", second, false);
        td7.appendChild(approve);
        td7.appendChild(deny);

        tr.appendChild(td7);
    }
}

//function that logs the APPROVE -jeremy, sam
//"e" argument is the event listener argument
function first(e) { 
    console.log(e.target.id);
    for (var i = 0; i < pendingReq.length; i++) {
        if (e.target.id = pendingReq[i].requestId) {
            pendingReq[i].status = 'Attempt-To-Be-Approved';

            //id of 0 to tell controller to try creating or updating the respective time event
            let putUrl = requestsUrl + '/0';
            fetch(putUrl, {
                method: 'PUT',
                headers: {
                    "Accept": 'application/json',
                    "Content-Type": 'application/json'
                },
                body: JSON.stringify(pendingReq[i])
            }).then((response) => {
                return response.json();
                
            }).then(function (response) {
                requestStatus = response;
                globalRequest = requestStatus;
                if (requestStatus.status == 'warning') {
                    //create form with confirm or deny
                    let modal = document.querySelector(".modal");
                    modal.style.display = "block";
                }
                else {
                    handleUpdate(requestStatus);
                }
            });
            break;
        }
    }
}

//update request in database to be 'Approved' -sam
//"requestStatus" argument is a request object
function handleUpdate(requestStatus) {
    putUrl = requestsUrl + `/${requestStatus.requestId}`;
    requestStatus.status = 'Approved';

    //call requests controller put method
    fetch(putUrl, {
        method: 'PUT',
        headers: {
            "Accept": 'application/json',
            "Content-Type": 'application/json'
        },
        body: JSON.stringify(requestStatus)
    }).then((response) => {
        return response.json();
    }).then(function (response) {
        successfulUpdate = response;
        if (successfulUpdate.status == 'updated' || successfulUpdate.status == 'created') {
            window.alert(`Request approved!`);
        } else {
            window.alert('There was an error approving the request.');
        }
    });

    updateView();
}

//function that logs the DENY -jeremy, sam
//"e" argument is the event listener argument
function second(e) { 
    for (var i = 0; i < pendingReq.length; i++) {
        if (e.target.id = pendingReq[i].requestId) {
            pendingReq[i].status = 'Denied';
            const putUrl = requestsUrl + `/${pendingReq[i].requestId}`;

            //call requests controller put method
            fetch(putUrl, {
                method: 'PUT',
                headers: {
                    "Accept": 'application/json',
                    "Content-Type": 'application/json'
                },
                body: JSON.stringify(pendingReq[i])
            }).then((response) => {
                if (response.status == 200) {
                    window.alert(`Request denied.`);
                } else {
                    window.alert('There was an error denying the request.');
                }
            });
            break;
        }
    }

    updateView();
}

//function of approve button on modal, overwrites conflicting time events in database -sam
function secondApprove() {
    globalRequest.status = 'Approved';

    //id of 0 to tell controller to try creating or updating the respective time event
    putUrl = requestsUrl + '/0';

    //call requests controller put method
    fetch(putUrl, {
        method: 'PUT',
        headers: {
            "Accept": 'application/json',
            "Content-Type": 'application/json'
        },
        body: JSON.stringify(requestStatus)
    }).then(function () {
        handleUpdate(globalRequest);
    });
}

//function of deny button on modal, sets request status to denied -sam
function secondDeny() {
    const putUrl = requestsUrl + `/${globalRequest.requestId}`;
    globalRequest.status = 'Denied';

    //call requests controller put method
    fetch(putUrl, {
        method: 'PUT',
        headers: {
            "Accept": 'application/json',
            "Content-Type": 'application/json'
        },
        body: JSON.stringify(globalRequest)
    }).then((response) => {
        return response.json();
    }).then(function (response) {
        let myResponse = response;
        if (myResponse.status == 'updated') {
            window.alert(`Request denied.`);
        } else {
            window.alert('There was an error denying the request.');
        }
    });

    updateView();
}


//return to admin dashboard -jeremy
function retToHome() {
    window.location = 'AdminHome.html';
}

// reloads the page so the requests can go where they belong after they have been handled -jeremy
function updateView() { 
    setTimeout(() => {
        window.location.reload();
    }, 2000);
}