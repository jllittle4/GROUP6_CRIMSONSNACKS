//base url -sam
const baseUrl = 'https://localhost:7139/api/';

//load both report tables on click -sam
function makeTables() {
  //arguments described above getTablesData() function
  getTablesData('employee-total-time', 'employee-table');
  getTablesData('department-total-time', 'department-table');
}

//get report data -sam
function getTablesData(type, tableid) {
  //reporting total time controller -sam
  //different from the report time controller
  const timeUrl = baseUrl + "ReportingTotalTime";

  //see ReportRequest.cs in models
  //note the "type" field, this tells the controller what type of report to return
  //this type of report doesn't need a department, employee, start date, or end date

  const sendReportRequest = {
    "Department": 'default',
    "Employee": 'default',
    "PayrollPeriod": document.getElementById("payroll").value,
    "StartDate": 'default',
    "EndDate": 'default',
    "Type": type,
  };

  //call reporting total time controller post method

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
      //"json" is a list of time event objects
      //the fields currently being returned (which are also in Report.cs) are...

      // Category
      // Time

      if (json.length != 0) {
        loadTable(tableid, json);
      } else {
        //alerts and reloads if no data was returned

        window.alert("No data was returned.");
        location.reload();
      }
    });
}

//load report data -sam, jeremy
//"tableid" argument is the html element id for the table
//"reports" is a list of report objects described above in get tables data function
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

//reload page to clear report -sam
function reLoad() {
  location.reload();
}
