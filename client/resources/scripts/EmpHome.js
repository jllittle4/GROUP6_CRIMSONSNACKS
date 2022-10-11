`use strict`
var datetime = new Date();
console.log(datetime);
document.getElementById("time").textContent = datetime; //it will print on html page

function refreshTime() {
    const timeDisplay = document.getElementById("time");
    const dateString = new Date().toLocaleString();
    const formattedString = dateString.replace(", ", " - ");
    timeDisplay.textContent = formattedString;
  }
    setInterval(refreshTime, 1000);




function handleClick() {

    var btn = document.getElementById("timepunch");

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

function getDepartment(){
    let dept = prompt("Please enter the Department you worked");

    


    switch(dept.toLowerCase()){
        case "stocking":
            
            console.log(capFirst(dept.toLowerCase()));
            alert('I made it bitch');
        break;
        case "marketing":
            
            console.log(capFirst(dept.toLowerCase()));
            alert('I made it bitch');
        break;
        case "ordering":
            
            console.log(capFirst(dept.toLowerCase()));
            alert('I made it bitch');
        break;
        case "sales":
            
            console.log(capFirst(dept.toLowerCase()));
            alert('I made it bitch');
        break;
        default:
            alert('Invalid input. Please enter one of the folowing: \t Stocking, Marketing, Ordering, Sales');
            getDepartment();
        break;

    }
    function capFirst(str) {
        return str[0].toUpperCase() + str.slice(1);
    }
    // if (dept = "stocking" || "marketing" || "ordering" || "sales"){
    //     dept.toUpperCase;
    //     console.log(dept);
    //     alert('I made it bitch');
    // }
    // else{
        
    //     alert('Invalid input. Please enter one of the folowing: \t Stocking, Marketing, Ordering, Sales');
    //     getDepartment();
    // }
    
   

    
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




