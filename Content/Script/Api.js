function checkLogin() {
    var user = document.querySelector('.username');
    var pass = document.querySelector('.password');

    if (user.value == "" || user.value == null && pass.value == "" || pass.value == null) {
        user.style.border = '1px solid #B80000';
        pass.style.border = '1px solid #B80000';
        return false;
    }
    else
    {
        user.style.border = '1px solid green';
    }

    if (pass.value == "" || pass.value == null) {
        pass.style.border = '1px solid #B80000';
        return false;
    } else if (pass.value.length < 8) {
        pass.style.border = '1px solid #B80000';
        return false
    }
    else {
        pass.style.border = '1px solid green';
        return true;
    }
    return true;
}

function checkRegister() {
    var fname = document.querySelector('.userFname');
    var lname = document.querySelector('.userLname');
    var email = document.querySelector('.emailID');
    var phone = document.querySelector('.mobNo');
    var dob = document.querySelector('.dob');
    var add = document.querySelector('.address');
    var city = document.querySelector('.city');
    var user = document.querySelector('.username');
    var pass = document.querySelector('.password');

    if (fname.value == "" || fname.value == null) {
        fname.style.border = '1px solid #B80000';
        return false;
    } else {
        fname.style.border = '1px solid green';
        if (lname.value == "" || lname.value == null) {
            lname.style.border = '1px solid #B80000';
            return false;
        } else if (email.value == "" || email.value == null) {
            lname.style.border = '1px solid green';
            email.style.border = '1px solid #B80000';
            return false;
        } else if (phone.value == "" || phone.value == null) {
            email.style.border = '1px solid green';
            phone.style.border = '1px solid #B80000';
            return false;
        } else if (dob.value == "" || dob.value == null) {
            phone.style.border = '1px solid green';
            dob.style.border = '1px solid #B80000';
            return false;
        } else if (add.value == "" || add.value == null) {
            dob.style.border = '1px solid green';
            add.style.border = '1px solid #B80000';
            return false;
        } else if (city.value == "" || city.value == null) {
            add.style.border = '1px solid green';
            city.style.border = '1px solid #B80000';
            return false;
        } else if (user.value == "" || user.value == null) {
            city.style.border = '1px solid green';
            user.style.border = '1px solid #B80000';
            return false;
        } else if (pass.value == "" || pass.value == null) {
            user.style.border = '1px solid green';
            pass.style.border = '1px solid #B80000';
            return false;
        } else if (pass.value.length < 8) {
            pass.style.border = '1px solid green';
            pass.style.border = '1px solid #B80000';
            return false;
        } else {
            pass.style.border = '1px solid green';
            return true;
        }
        return true;
    }
}

function checkUser() {
    var user = document.querySelector('.username');
    if (user.value == "" || user.value == null) {
        user.style.border = '1px solid #B80000';
        return false;
    } else {
        user.style.border = '1px solid green';
        return true;
    }
}

function checkPass() {
    var pass = document.querySelector('.password');

    if (pass.value == "" || pass.value == null) {
        pass.style.border = '1px solid #B80000';
        return false;
    } else if (pass.value.length < 8) {
        pass.style.border = '1px solid #B80000';
        return false
    }
    else {
        pass.style.border = '1px solid green';
        return true;
    }
}

function checkFName() {
    var fname = document.querySelector('.userFname');

    if (fname.value == "" || fname.value == null) {
        fname.style.border = '1px solid #B80000';
        return false;
    } else {
        fname.style.border = '1px solid green';
        return true;
    }
}

function checkLName() {
    var lname = document.querySelector('.userLname');

    if (lname.value == "" || lname.value == null) {
        lname.style.border = '1px solid #B80000';
        return false;
    } else {
        lname.style.border = '1px solid green';
        return true;
    }
}

function checkEmail() {
    var email = document.querySelector('.emailID');
    var pattern = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

    if (email.value == "" || email.value == null) {
        email.style.border = '1px solid #B80000';
        return false;
    } else if (!pattern.test(email.value)) {
        email.style.border = '1px solid #B80000';
        return false;
    } else {
        email.style.border = '1px solid green';
        return true;
    }
}

function checkPhone() {
    var phone = document.querySelector('.mobNo');
    var pattern = /\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4})/;

    if (phone.value == "" || phone.value == null) {
        phone.style.border = '1px solid #B80000';
        return false;
    } else if (!pattern.test(phone.value)) {
        phone.style.border = '1px solid #B80000';
        return false;
    } else {
        phone.style.border = '1px solid green';
        return true;
    }
}

function checkDob() {
    var dob = document.querySelector('.dob');

    if (dob.value == "" || dob.value == null) {
        dob.style.border = '1px solid #B80000';
        return false;
    } else {
        dob.style.border = '1px solid green';
        return true;
    }
}

function checkAdd() {
    var add = document.querySelector('.address');

    if (add.value == "" || add.value == null) {
        add.style.border = '1px solid #B80000';
        return false;
    } else {
        add.style.border = '1px solid green';
        return true;
    }
}

function checkCity() {
    var city = document.querySelector('.city');

    if (city.value == "" || city.value == null) {
        city.style.border = '1px solid #B80000';
        return false;
    } else {
        city.style.border = '1px solid green';
        return true;
    }
}

