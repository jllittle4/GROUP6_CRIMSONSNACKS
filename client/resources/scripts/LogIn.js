const baseurl = 'https://localhost:7139/api/';
postUrl = baseurl + 'users'



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
   


    // const users = {firstname: 'Jeremy', lastname: 'Little', usernameid: 'jllittle4', passwordid: 'Jman040402$'};

    // if ( username == users.usernameid && password == users.passwordid){
    //     alert ("Login successfully");
    //     window.location = "EmpHome.html"; // Redirecting to other page.
    //     return false;
    // }
    // else{
        
    //     alert("Incorrect, please try again");
    //     console.log(users.usernameid)
    
    // }

    let username = document.getElementById("username").value;
    let password = document.getElementById("password").value;
    const sendUser ={
        "UserName": username.toLowerCase(),
        "Password": password
    }
    
    fetch(postUrl, {
        method: 'POST',
                headers: {
        "Accept": 'application/json',
        "Content-Type": 'application/json'
        },
        body: JSON.stringify(sendUser)
    }).then((response)=> {
        console.log(sendUser);
        if(response.status == 200){
            window.alert('Login is valid');
            window.localStorage.setItem('username', username);
    
            window.location = "EmpHome.html"; // Redirecting to other page.

        }
        else{
            window.alert('Login credentials were invalid, plase try again')
        }
        console.log('response from the save ', response);
    });
}

function SignUp(){
    window.location = "SignUp.html";
}


// function go () {
//     // (A) VARIABLES TO PASS
    
  
//     // (B) URL PARAMETERS
//     // var params = new URLSearchParams();
//     // params.append("username", username);
//     window.localStorage.setItem('username', username);
//     console.log(username);
  
//     // (C) GO!
//     // var url = "https://localhost:7139/api/" + params.toString();
//     // location.href = url;
//     // window.open(url);

// }

