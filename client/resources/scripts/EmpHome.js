`use strict`
var datetime = new Date();
console.log(datetime);
document.getElementById("time").textContent = datetime; //it will print on html page

let username = window.localStorage.getItem('username');
let header = document.getElementById('welcome');
header.textContent = 'Welcome, ' + username;
console.log(username);

let times = [
    {'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5'},
    {'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5'},
    {'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5'},
    {'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5'},
    {'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5'},
    {'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5'},
    {'date': '2022-10-05', 'clockIn': '7:30', 'clockOut': '10:00', 'dept': 'Shipping', 'totalTime': '2.5'},
]

function refreshTime() {
    const timeDisplay = document.getElementById("time");
    const dateString = new Date().toLocaleString();
    const formattedString = dateString.replace(", ", " - ");
    timeDisplay.textContent = formattedString;
  }
    setInterval(refreshTime, 1000);




function handleClick() {

    var btn = document.getElementById("timePunch");

    if (btn.value == "Clock In") {
        btn.value = "Clock Out";
        btn.innerHTML = "Clock Out";
    }
    else {
        btn.value = "Clock In";
        btn.innerHTML = "Clock In";

        getDepartment();
    }

}

let btn = document.getElementById("timePunch")
let modal = document.querySelector(".modal")
let closeBtn = document.querySelector(".close-btn")

btn.onclick = function(){
    if (btn.value == "Clock In") {
        btn.value = "Clock Out";
        btn.innerHTML = "Clock Out";
    }
    else {
        btn.value = "Clock In";
        btn.innerHTML = "Clock In";
        modal.style.display = "block";

        // getDepartment();
    }
    // if(btn.value == "Clock Out"){
    //     modal.style.display = "block";
    // }
}
closeBtn.onclick = function(){
  modal.style.display = "none"
}
window.onclick = function(e){
  if(e.target == modal){
    modal.style.display = "none"
  }
}

// function get () {
//     // (A) GET THE PARAMETERS
//     var params = new URLSearchParams(window.location.search),
//         username = params.get("username");
//         window.localStorage.getItem('username');
  
//     // (B) IT WORKS!
//     console.log(username);  // Foo Bar
//      // ["Hello", "World"]
//      return username;
//   }

// function getDepartment(){
//     let dept = prompt("Please enter the Department you worked");

    


//     switch(dept.toLowerCase()){
//         case "stocking":
            
//             console.log(capFirst(dept.toLowerCase()));
//             alert('I made it');
//         break;
//         case "marketing":
            
//             console.log(capFirst(dept.toLowerCase()));
//             alert('I made it');
//         break;
//         case "ordering":
            
//             console.log(capFirst(dept.toLowerCase()));
//             alert('I made it');
//         break;
//         case "sales":
            
//             console.log(capFirst(dept.toLowerCase()));
//             alert('I made it ');
//         break;
//         default:
//             alert('Invalid input. Please enter one of the folowing: \t Stocking, Marketing, Ordering, Sales');
//             getDepartment();
//         break;

//     }
//     function capFirst(str) {
//         return str[0].toUpperCase() + str.slice(1);
//     }
    // if (dept = "stocking" || "marketing" || "ordering" || "sales"){
    //     dept.toUpperCase;
    //     console.log(dept);
    //     alert('I made it bitch');
    // }
    // else{
        
    //     alert('Invalid input. Please enter one of the folowing: \t Stocking, Marketing, Ordering, Sales');
    //     getDepartment();
    // }
    
   

    

// function loadTable(){
//     let table = document.getElementById('table');
//     table.border = '1';
//     let tableBody = document.createElement('TBODY');
//     tableBody.id = 'TableBody';
//     table.appendChild(tableBody);
//     times.forEach((time) => {
//         let tr = document.createElement('TR');
//         tableBody.appendChild(tr);
    
//         let td1 = document.createElement('TD');
//         td1.width = 300;
//         td1.appendChild(document.createTextNode(time.date));
//         tr.appendChild(td1);


//         let td2 = document.createElement('TD');
//         td2.width = 500;
//         td2.appendChild(document.createTextNode(time.clockIn));
//         tr.appendChild(td2);


//         let td3 = document.createElement('TD');
//         td3.width = 150;
//         td3.appendChild(document.createTextNode(time.clockOut));
//         tr.appendChild(td3);

//         let td4 = document.createElement('TD');
//         td4.width = 150;
//         td4.appendChild(document.createTextNode(time.dept));
//         tr.appendChild(td4);

//         let td5 = document.createElement('TD');
//         td5.width = 150;
//         td5.appendChild(document.createTextNode(time.totalTime));
//         tr.appendChild(td5);

//     }
//     )}

function retToLogin(){
    window.location = "LogIn.html";
}
// function show_list() {
//     var courses = document.getElementById("courses_id");

//     if (courses.style.display == "block") {
//         courses.style.display = "none";
//     } else {
//         courses.style.display = "block";
//     }
// }
// window.onclick = function (event) {
//     if (!event.target.matches('.dropdown_button')) {
//         document.getElementById('courses_id')
//             .style.display = "none";
//     }
// }   




