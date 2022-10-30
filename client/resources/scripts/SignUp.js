let users = [];
const baseurl = 'https://localhost:7139/api/';
var uid = document.getElementById('userid');
var passid = document.getElementById('passid');
var ufname = document.getElementById('userfname');
var ulname = document.getElementById('userlname');


function formValidation() {
    // var uid = document.registration.userid;
    // var passid = document.registration.passid;
    // var ufname = document.registration.userfname;
    // var ulname = document.registration.userlname;

    if (userid_validation(uid, 5, 12)) {
        if (passid_validation(passid, 7, 12)) {

            return true;
        }
    }
    return false;

}

function userid_validation(uid, mx, my) {
    var uid_len = uid.value.length;
    if (uid_len == 0 || uid_len >= my || uid_len < mx) {
        alert("User Id should not be empty / length be between " + mx + " to " + my);
        uid.focus();
        return false;
    }
    return true;
}

function passid_validation(passid, mx, my) {
    var passid_len = passid.value.length;
    if (passid_len == 0 || passid_len >= 8) {
        alert("Password should not be empty / length be between " + mx + " to " + my);
        passid.focus();
        return false;
    }
    return true;
}


function RettoLogin() {
    alert('Form Succesfully Submitted');
    // form.addEventListener('submit', function(e){ //e i sa parameter
    //     e.preventDefault(); //dont refresh the page
    console.log('submitted');

    let user = {
        // ufname = document.getElementById('userfname').value,
        // ulname = document.getElementById('userlname').value,
        // uid = document.getElementById('userid').value,
        // passid = document.getElementById('passid').value,

        userid: document.getElementById('userid').value,
        pass: document.getElementById('passid').value,
        userfname: document.getElementById('userfname').value,
        userlname: document.getElementById('userlname').value,

    };

    const postUrl = baseurl + 'users';

    //sam
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
        }
        console.log('response from the save ', response);
    });

    window.location = 'LogIn.html';
}

