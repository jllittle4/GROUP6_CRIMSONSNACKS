// please test out function once you have data in here to ensure it works properly. 
//I have code in place so that when the adming selects and employee, the table is only 
//filled once and cannot be filled again if the submit button is clicked. However I also have a 
//function set to where when she chooses a new employee the function will reset allowing her to 
//populate the tabe again only once though.




//POSSIBLE TO ADD AN OPTIONAL DATE SEARCH FOR ADMIN TO JUST SEARCH FOR A SPECIFIC EMPLOYEE ON A SPECIFIC DATE




let employees = []
const baseurl = 'https://localhost:7139/api/'; /// ENTER BASE URL HERE
const allTaskUrl = baseurl + 'Users';
let clicked = "";
let dropDown = "";

var ran = 0;
let run = 1;


function handleOnLoad(){
    if(ran <= 1){

    
    let dropDown = document.getElementById('arr');
    


    dropDown.length = 0;
    let defaultOption = document.createElement('option');
    defaultOption.text = 'Choose Employee';

    dropDown.add(defaultOption);

    dropDown.selectedIndex = 0;
    getEmployees();
    ran ++;
    }
    return ran
    
}


   



function getEmployees(){ //SIMPLY POPULATING FORM FOR DROPDOWN TO SELECT AN EMPLOYEE


    let dropDown = document.getElementById('arr');


    fetch(allTaskUrl).then(function(response){
    console.log(response);
        return response.json();
    }).then(function(json){
        console.log(json);
        employees = json;
    })
    let option = '';
    for(let i = 0; i < employees.length; i++) {
        option = document.createElement('option');
        option.text = employees[i].empName;
        option.value = employees[i].empId;  // MAY NEED TO CHANGE THESE DEPENDING ON THE DATA YOU GET
       

        
      dropDown.appendChild(option);

      run = 1;

      
    };
    

    return dropDown;
      


}



function retToHome(){
    window.location = "AdminHome.html";
}




function handleTableLoad(){
    if(run <= 1){

    loadTable();
    run ++;
    }
    return run
    
}



let times = [
    {'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5'},
    {'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5'},
    {'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5'},
    {'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5'},
    {'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5'},
    {'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5'},
    {'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5'},
]
// NEED TO FILL ARRAY FOR TIMES OF A SPECIFIC EMPLOYEE THATS SELECTED FROM THE DROPDOWN
function loadTable(){
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
    )}
