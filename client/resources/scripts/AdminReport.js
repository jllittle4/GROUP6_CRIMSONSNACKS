const baseUrl = 'https://localhost:7139/api/';

function makeTables(){
    getTablesData('employee-total-time','employee-table');
    getTablesData('department-total-time','department-table');
}

function getTablesData(type, tableid) {
  const timeUrl = baseUrl + "ReportingTotalTime";

  const sendReportRequest = {
    "Department": 'default',
    "Employee": 'default',
    "PayrollPeriod": document.getElementById("payroll").value,
    "StartDate": 'default',
    "EndDate": 'default',
    "Type": type,
  };

  fetch(timeUrl, {
    method: "POST",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
    body: JSON.stringify(sendReportRequest),
  })
    .then(function (response) {
      return response.json();
    })
    .then(function (json) {
      //console.log(json);
      if (json.length != 0) {
        loadTable(tableid,json);
      } else {
        window.alert("No data was returned.");
        location.reload();
      }
    });
}

function loadTable(tableid, reports) {
    var myTable = document.getElementById(tableid);
    myTable.style.display = 'block';

    let table = document.getElementById(tableid);
    table.border = '1';

    let tableBody = document.createElement('TBODY');
    tableBody.id = 'TableBody';
    table.appendChild(tableBody);

    reports.forEach((report) => {
        let tr = document.createElement('TR');
        tableBody.appendChild(tr);

        let td1 = document.createElement('TD');
        td1.width = 150;
        td1.appendChild(document.createTextNode(report.category));
        tr.appendChild(td1);


        let td2 = document.createElement('TD');
        td2.width = 150;
        td2.appendChild(document.createTextNode(report.time));
        tr.appendChild(td2);

    });
}

function reLoad() {
    location.reload();
}
