//all this work is jeremy's, except for sam's api call to create user

const baseurl = 'https://localhost:7139/api/';
const userUrl = baseurl + 'Users';
let users = getEmployees();

function submitRegistrationForm() {
    let user = {
        userid: document.getElementById('userid').value,
        pass: document.getElementById('passid').value,
        userfname: document.getElementById('userfname').value,
        userlname: document.getElementById('userlname').value,

    };

    authenticateAllFieldsFilled(user);
}

function authenticateAllFieldsFilled(user) {
    if (user.userfname != '' && user.userlname != '' && user.userid.toLowerCase() != '' && user.pass != '') {
        authenticatePasswordUppercase(user);

    } else {
        alert('All fields must be filled');
        location.reload();
    }
}

function authenticatePasswordUppercase(user) {
    if (user.pass.toLowerCase() != user.pass) {
        authenticatePasswordLength(user);

    } else {
        alert('Password must have at least one uppercase character');
        location.reload();
    }
}

function authenticatePasswordLength(user) {
    if (user.pass.length >= 8 && user.pass.length <= 20) {
        authenticatePasswordNumbers(user);

    } else {
        alert('Password must be more than 8 characters and less than 20 characters');
        location.reload();
    }
}

function authenticatePasswordNumbers(user) {
    found = false;
    for (i = 0; i < user.pass.length; i++) {
        char = user.pass[i];
        myarray = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9];
        for (j = 0; j < myarray.length; j++) {
            if (char == myarray[j]) {
                found = true;
                break;
            }
        }
    }

    if (found) {
        authenticateUniqueUsername(user);

    } else {
        alert('Password must have at least one number');
        location.reload();
    }
}

function authenticateUniqueUsername(user) {
    userfound = false;
    for (k = 0; k < users.length; k++) {
        if (users[k] == user.userid.toLowerCase()) {
            userfound = true;
            break;
        }
    }

    if (!userfound) {
        sendNewUser(user);

    } else {
        alert('Username already taken. Use a different username');
        location.reload();
    }
}

function sendNewUser(user) {
    //users controller api
    const postUrl = baseurl + 'Users';

    //see User.cs
    //all fields are required except "UserId" and "IsManager"
    const sendUser = {
        "UserId": 0,
        "FirstName": user.userfname,
        "LastName": user.userlname,
        "UserName": user.userid.toLowerCase(),
        "Password": user.pass,
        "IsManager": 0
    };

    fetch(postUrl, {
        method: 'POST',
        headers: {
            "Accept": 'application/json',
            "Content-Type": 'application/json'
        },
        body: JSON.stringify(sendUser)
    }).then((response) => {
        if (response.status == 200) {
            window.alert('Your account has been created!');
            RettoHome();
        } else {
            window.alert('There was an issue with your registration. Please contact your admin.');
        }
    });
}

function RettoHome() {
    window.location = 'LogIn.html';
}

function getEmployees() {
    let users = [];
    fetch(userUrl).then(function (response) {
        return response.json();
    }).then(function (json) {
        for (let i = 0; i < json.length; i++) {
            users.push(json[i].userName);
        };
    });
    return users;
}