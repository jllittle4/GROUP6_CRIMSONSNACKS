
const baseurl = 'https://localhost:7139/api/';

// // Below function Executes on click of login button.
function handleOnLoad(){

    //getUsers();
    
        
}
// function getUsers(){
//     const allTaskUrl = baseurl + 'users';

//     fetch(allTaskUrl).then(function(response){
//         console.log(response);
//         return response.json();
//     }).then(function(json){
//         console.log(json);
//         users = json;
//     })
// }
function validate(){
    var username = document.getElementById("username").value;
    var password = document.getElementById("password").value;


    const users = {firstname: 'Jeremy', lastname: 'Little', usernameid: 'jllittle4', passwordid: 'Jman040402$'};

    if ( username == users.usernameid && password == users.passwordid){
        alert ("Login successfully");
        window.location = "EmpHome.html"; // Redirecting to other page.
        return false;
    }
    else{
        
        alert("Incorrect, please try again");
        console.log(users.usernameid)
    
    }
}


function SignUp(){
    window.location = "SignUp.html";
}