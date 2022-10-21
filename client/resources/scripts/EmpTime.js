let times = [
    { 'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5' },
    { 'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5' },
    { 'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5' },
    { 'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5' },
    { 'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5' },
    { 'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5' },
    { 'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5' },
];

function loadTable() {
    let table = document.getElementById('table');
    table.border = '1';
    let tableBody = document.createElement('TBODY');
    tableBody.id = 'TableBody';
    table.appendChild(tableBody);
    times.forEach((time) => {
        let tr = document.createElement('TR');
        tableBody.appendChild(tr);

        let td1 = document.createElement('TD');
        td1.width = 300;
        td1.appendChild(document.createTextNode(time.date));
        tr.appendChild(td1);


        let td2 = document.createElement('TD');
        td2.width = 500;
        td2.appendChild(document.createTextNode(time.clockIn));
        tr.appendChild(td2);


        let td3 = document.createElement('TD');
        td3.width = 150;
        td3.appendChild(document.createTextNode(time.clockOut));
        tr.appendChild(td3);

        let td4 = document.createElement('TD');
        td4.width = 150;
        td4.appendChild(document.createTextNode(time.dept));
        tr.appendChild(td4);

        let td5 = document.createElement('TD');
        td5.width = 150;
        td5.appendChild(document.createTextNode(time.totalTime));
        tr.appendChild(td5);

    }
    );
}

function retToHome() {
    window.location = "EmpHome.html";
}