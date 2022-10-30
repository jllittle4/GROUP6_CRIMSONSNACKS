// let times = [
//     { 'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5' },
//     { 'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5' },
//     { 'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5' },
//     { 'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5' },
//     { 'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5' },
//     { 'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5' },
//     { 'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5' },
// ];
const baseUrl = "https://localhost:7139/api/"; //sam
let username = window.localStorage.getItem("username");
let empid = -1;
empid = getEmployeeID();
console.log(empid);
let results = [];
getTimeKeepingEventsByEmpID();
//console.log(username);

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

function getTimeKeepingEventsByEmpID() {
  const timeUrl = baseUrl + "TimeKeeping";

  fetch(timeUrl)
    .then(function (response) {
      return response.json();
    })
    .then(function (json) {
      console.log(json[0].employeeId);
      for (var i = 0; i < json.length; i++) {
        if (json[i].employeeId == empid) {
          console.log(json[i].employeeId == empid);
          addRow(json[i]);
          results.push(json[i]);
          //console.log(json[i]);
        }
      }
    });
}

function loadTable() {
  console.log("made it here");
  let table = document.getElementById("table");
  table.border = "1";
  let tableBody = document.createElement("TBODY");
  tableBody.id = "TableBody";
  table.appendChild(tableBody);
  //console.log(results);
  //addRows(results);
}

function retToHome() {
  window.location = "EmpHome.html";
}

// function addRows(results) {
//   results.forEach((result) => {
//     let tr = document.createElement("TR");
//     tableBody.appendChild(tr);

//     let td1 = document.createElement("TD");
//     td1.width = 300;
//     td1.appendChild(document.createTextNode(result.date));
//     tr.appendChild(td1);

//     let td2 = document.createElement("TD");
//     td2.width = 500;
//     td2.appendChild(document.createTextNode(result.clockIn));
//     tr.appendChild(td2);

//     let td3 = document.createElement("TD");
//     td3.width = 150;
//     td3.appendChild(document.createTextNode(result.clockOut));
//     tr.appendChild(td3);

//     let td4 = document.createElement("TD");
//     td4.width = 150;
//     td4.appendChild(document.createTextNode(result.departmentId));
//     tr.appendChild(td4);

//     let td5 = document.createElement("TD");
//     td5.width = 150;
//     td5.appendChild(document.createTextNode(result.totalTime));
//     tr.appendChild(td5);
//   });
// }

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
