//base url -jeremy
const baseurl = 'https://localhost:7139/api/';
//loggingin controller api -sam
const postUrl = baseurl + 'LoggingIn';



// // Below function Executes on click of login button.

function handleOnLoad() {

}

//check whether the login credentials are correct -sam, jeremy
function validate() {
    let username = document.getElementById("username").value;
    let password = document.getElementById("password").value;

    //see User.cs in models
    //only userName and password fields are needed

    const sendUser = {
        "UserId": 0,
        "FirstName": 'default',
        "LastName": 'default',
        "UserName": username.toLowerCase(),
        "Password": password,
        "IsManager": 0,
    };

    //call loggingin controller post method
    fetch(postUrl, {
        method: 'POST',
        headers: {
            "Accept": 'application/json',
            "Content-Type": 'application/json'
        },
        body: JSON.stringify(sendUser)
    }).then((response) => {
        return response.json();
    }).then(function (json) {
        //"json" is a login result object (LoginResult.cs), all fields are returned

        //CheckPassword
        //CheckUserName
        //IsAdmin

        if (json.checkUserName && json.checkPassword && json.isAdmin) {
            window.alert('Login is valid');
            window.localStorage.setItem('username', username);
            window.location = "AdminHome.html"; // Redirecting to other page.
        } else if (json.checkUserName && json.checkPassword) {
            window.alert('Login is valid');
            window.localStorage.setItem('username', username);
            window.location = "EmpHome.html";
        } else if (!json.checkUserName) {
            window.alert('Incorrect username. Please try again');
        }
        else if (!json.checkPassword) {
            window.alert('Incorrect password. Please try again');
        }
    });
}

//go to sign up page -jeremy
function SignUp() {
    window.location = "SignUp.html";
}
