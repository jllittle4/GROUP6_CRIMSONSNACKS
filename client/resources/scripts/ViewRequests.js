const baseUrl = "https://localhost:7139/api/"; //sam
const requestsUrl = baseUrl + 'Requests';

function handleOnLoad() {
    loadTable('pending');
    loadTable('approved');
    loadTable('denied');

    getRequests('pending');
    getRequests('approved');
    getRequests('denied');
}


function getRequests(type) {

    fetch(requestsUrl)
        .then(function (response) {
            return response.json();
        })
        .then(function (json) {
            console.log(json);
            json.forEach(request => {
                if (request.status.toLowerCase() == type) {
                    //console.log('made it here')
                    addRow(type, request);
                }
            });
        });
}

function loadTable(type) {
    //console.log("made it here");
    let table = document.getElementById(type);
    table.border = '1';
    let tableBody = document.createElement("TBODY");
    tableBody.id = `${type}-tbody`;
    console.log(tableBody.id);
    table.appendChild(tableBody);
}

function retToHome() {
    window.location = "EmpHome.html";
}

function addRow(type, request) {
    let tableId = `${type}-tbody`;
    //console.log(tableId);
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

function retToHome() {
    window.location = 'AdminHome.html';
}