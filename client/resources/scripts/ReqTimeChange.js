function retToHome() {
    window.location = 'EmpHome.html';
}


// <form>
// <div>
//   <label for="date">Date</label>
//   <input type="date" data-date-inline-picker="true" />
// </div>

// <div>
//     <label for="dept">Department:</label>
//     <select name="dept" id="dept">
//     <option>Shipping</option>
//     <option>Stock</option>
//     <option>Marketing</option>
//     <option>Sales</option>
//     </select>
// </div>

// <div>
//     <label for="clockIn">Clock In:</label>

//     <input type="time" id="appt" name="clockIn"
//             min="09:00" max="18:00" required>
// </div>


// <div>
//     <label for="clockOut">Clock Out:</label>
//     <input type="time" id="appt" name="clockOut"
//             min="09:00" max="18:00" required>
// </div>


// <div>
//     <div>
//         <label for="reason">Reason:</label>
//         <input name="reason" id="reason"/>
//       </div>
//     <div>

// </div>
// </form>
// <input type = "button" onclick="retToHome();" value = 'Submit'></input>
// <input type = "button" onclick="retToHome();" value = 'Cancel'></input>

function onSubmit() {
    let date = document.getElementById('date');
    let dept = document.getElementById('dept');
    let clockIn = document.getElementById('clockIn');
    let clockOut = document.getElementById('clockOut');
    let reason = document.getElementById('reason');
    let username = window.localStorage.setItem('username', username);

    console.log(date.value, dept.value, clockIn.value, clockOut.value, reason.value);

    const sendReq = {
        "username": username,
        "date": date.value,
        "dept": dept.value,
        "clockIn": clockIn.value,
        "clockOut": clockOut.value,
        "reason": reason.value,

    };

    fetch(postUrl, {
        method: 'POST',
        headers: {
            "Accept": 'application/json',
            "Content-Type": 'application/json'
        },
        body: JSON.stringify(sendReq)
    }).then((response) => {
        if (response.status == 200) {
            window.alert('User has been saved');
        }
        console.log('response from the save ', response);
    });
}