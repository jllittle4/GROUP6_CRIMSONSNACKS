//SENDING DEPT NAME AND HOUR TOTAL FOR INPUTTED MONTH

//SENDING EMP FULL NAME AND HOUR TOTAL FOR INPUTTED MONTH


// var names = dept and total


/// going to need some testing with the server running on this one 

// let data = []
const baseurl = 'https://localhost:7139/api/'; /// ENTER BASE URL HERE
const allTaskUrl = baseurl + 'Users';
let clicked = "";
let dropDown = "";

var ran = 0;
let run = 1;


function handleOnLoad(){
    // if(ran <= 1){

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
                option.text = employees[i].firstName + " " + employees[i].lastName;
                option.value = employees[i].userId;  // MAY NEED TO CHANGE THESE DEPENDING ON THE DATA YOU GET
               
        
                
              dropDown.appendChild(option);
        
              run = 1;
        

    
    // let dropDown = document.getElementById('arr');
    // const sendUser ={
    //     "UserName": dropDown
    // }
    
    // fetch(postUrl, {
    //     method: 'POST',
    //             headers: {
    //     "Accept": 'application/json',
    //     "Content-Type": 'application/json'
    //     },
    //     body: JSON.stringify(sendUser)
    // }).then((response)=> {
    //     console.log(sendUser);
    //     if(response.status == 200){
    //         window.alert('Login is valid');
    //         window.localStorage.setItem('username', username);

    //     }
    //     else{
    //         window.alert('Login credentials were invalid, plase try again')
    //     }
    //     console.log('response from the save ', response);
    // });

    


    // dropDown.length = 0;
    // let defaultOption = document.createElement('option');
    // defaultOption.text = 'Choose Employee';

    // dropDown.add(defaultOption);

    // dropDown.selectedIndex = 0;
    // getEmployees();
    // ran ++;
    // }
    return ran
    
}


   



// function getTotals(){ //SIMPLY POPULATING FORM FOR DROPDOWN TO SELECT AN EMPLOYEE


//     let dropDown = document.getElementById('date');


//     fetch(allTaskUrl).then(function(response){
//     console.log(response);
//         return response.json();
//     }).then(function(json){
//         console.log(json);
//         data = json;
//     })
//     let option = '';
//     for(let i = 0; i < employees.length; i++) {
//         option = document.createElement('option');
//         option.text = employees[i].empName;
//         option.value = employees[i].empId;  // MAY NEED TO CHANGE THESE DEPENDING ON THE DATA YOU GET
       

        
//       dropDown.appendChild(option);

//       run = 1;

      
//     };
    

//     return dropDown;
      


// }



function retToHome(){
    window.location = "AdminHome.html";
}




// function handleTableLoad(){
//     if(run <= 1){

//     loadTable();
//     run ++;
//     }
//     return run
    
// }



// let data = [
//     {'dept': "Shipping", 'total': "38.46"},
//     {'dept': "Marketing", 'total': "22.45"},
//     {'dept': "Sales", 'total': "96.48"},
//     {'dept': "Something Random", 'total': "19.32"},
//     {'dept': "Shipping", 'total': "28.02"},
// ]
// // NEED TO FILL ARRAY FOR TIMES OF A SPECIFIC EMPLOYEE THATS SELECTED FROM THE DROPDOWN
// function loadTable(){
//     var myTable = document.getElementById('deptTable');
//     myTable.style.display = 'block';
//     let table = document.getElementById('table');
//     table.border = '1';
//     let tableBody = document.createElement('TBODY');
//     tableBody.id = 'TableBody';
//     table.appendChild(tableBody);
//     data.forEach((data) => {
//         let tr = document.createElement('TR');
//         tableBody.appendChild(tr);
    
//         let td1 = document.createElement('TD');
//         td1.width = 300;
//         td1.appendChild(document.createTextNode(data.dept));
//         tr.appendChild(td1);


//         let td2 = document.createElement('TD');
//         td2.width = 300;
//         td2.appendChild(document.createTextNode(data.total));
//         tr.appendChild(td2);

//     }
//     )}
}
