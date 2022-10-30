const baseurl = 'https://localhost:7139/api/';
postUrl = baseurl + 'users';



// // Below function Executes on click of login button.

function handleOnLoad() {

}

function validate() {
    let username = document.getElementById("username").value;
    let password = document.getElementById("password").value;

    const sendUser = {
        "UserId": 0,
        "FirstName": 'default',
        "LastName": 'default',
        "UserName": username.toLowerCase(),
        "Password": password,
        "IsManager": 0,
    };

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

function SignUp() {
    window.location = "SignUp.html";
}
