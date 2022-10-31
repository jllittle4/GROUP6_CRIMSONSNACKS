// let times = [
//     { 'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5' },
//     { 'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5' },
//     { 'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5' },
//     { 'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5' },
//     { 'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5' },
//     { 'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5' },
//     { 'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5' },
// ];

//^^dummy date -jeremy

//base url -jeremy
const baseUrl = "https://localhost:7139/api/";

getEmpTimeEvents();

//get all time events for logged in employee -sam
function getEmpTimeEvents() {
  //clocking time controller api
  const timeUrl = baseUrl + 'ClockingTime';

  //call clockingtime controller get method
  fetch(timeUrl)
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

      json.forEach(event => {
        addRow(event);
      });
    });
}

//all of the below work is jeremy
function loadTable() {
  console.log("made it here");
  let table = document.getElementById("table");
  table.border = "1";
  let tableBody = document.createElement("TBODY");
  tableBody.id = "TableBody";
  table.appendChild(tableBody);
}

function retToHome() {
  window.location = "EmpHome.html";
}

function addRow(result) {
  let tableBody = document.getElementById("TableBody");
  let tr = document.createElement("TR");
  tableBody.appendChild(tr);

  let td1 = document.createElement("TD");
  td1.width = 200;
  td1.appendChild(document.createTextNode(result.date));
  tr.appendChild(td1);

  let td2 = document.createElement("TD");
  td2.width = 200;
  td2.appendChild(document.createTextNode(result.clockIn));
  tr.appendChild(td2);

  let td3 = document.createElement("TD");
  td3.width = 200;
  td3.appendChild(document.createTextNode(result.clockOut));
  tr.appendChild(td3);

  let td4 = document.createElement("TD");
  td4.width = 200;
  td4.appendChild(document.createTextNode(result.department));
  tr.appendChild(td4);

  let td5 = document.createElement("TD");
  td5.width = 200;
  td5.appendChild(document.createTextNode(result.totalTime));
  tr.appendChild(td5);
}
