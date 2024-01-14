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

function checkOut() {
    var email = document.querySelector('email');
    var name = document.querySelector('name');
    var phone = document.querySelector('phone');
    var address = document.querySelector('address');
    var city = document.querySelector('city');
    var country = document.querySelector('country');
    var note = document.querySelector('note');

    if (email.value == "" || email.value == null) {
        email.style.border = '1px solid #B80000';
        return false;
    } else {
        email.style.border = '1px solid green';
        if (name.value == "" || name.value == null) {
            name.style.border = '1px solid #B80000';
            return false;
        } else if (phone.value == "" || phone.value == null) {
            name.style.border = '1px solid green';
            phone.style.border = '1px solid #B80000';
            return false;
        } else if (address.value == "" || address.value == null) {
            phone.style.border = '1px solid green';
            address.style.border = '1px solid #B80000';
            return false;
        } else if (city.value == "" || city.value == null) {
            address.style.border = '1px solid green';
            city.style.border = '1px solid #B80000';
            return false;
        } else if (country.value == "" || country.value == null) {
            city.style.border = '1px solid green';
            country.style.border = '1px solid #B80000';
            return false;
        } else if (note.value == "" || note.value == null) {
            country.style.border = '1px solid green';
            note.style.border = '1px solid #B80000';
            return false;
        } else {
            note.style.border = '1px solid green';
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
    }
    else if (!pattern.test(phone.value)) {
        phone.style.border = '1px solid #B80000';
        return false;
    }
    else {
        if (phone.value.trim()) {
            if (iti.isPossibleNumber()) {
                phone.style.border = "1px solid green";
                return true;
            } else {
                phone.style.border = "1px solid red";
                return false;
            }
        }
        //phone.addEventListener('change', reset);
        //phone.addEventListener('keyup', reset);
    
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

function checkName() {
    var name = document.querySelector('.name');

    if (name.value == "" || name.value == null) {
        name.style.border = '1px solid #B80000';
        return false;
    } else {
        name.style.border = '1px solid green';
        return true;
    }
}

function checkCountry() {
    var country = document.querySelector('.country');

    if (country.value == "" || country.value == null) {
        country.style.border = '1px solid #B80000';
        return false;
    } else {
        country.style.border = '1px solid green';
        return true;
    }
}

function checkNote() {
    var note = document.querySelector('.note');

    if (note.value == "" || note.value == null) {
        note.style.border = '1px solid #B80000';
        return false;
    } else {
        note.style.border = '1px solid green';
        return true;
    }
}

function checkNumber() {
    var numbercard = document.querySelector('.numbercard');

    if (numbercard.value == "" || numbercard == null) {
        numbercard.style.border = '1px solid #B80000';
        return false;
    } else if (numbercard.value.length != 16) {
        numbercard.style.border = '1px solid #B80000';
        return false;
    } else {
        umbercard.style.border = '1px solid green';
        return true;
    }
}

function checkCgv() {
    var cgv = document.querySelector('.cgv');

    if (cgv.value == "" || cgb.value == null) {
        cgv.style.border = '1px solid #B80000';
        return false;
    } else if (cgv.value.length > 3 && cgv.value.length < 3) {
        cgv.style.border = '1px solid #B80000';
        return false;
    } else {
        cgv.style.border = '1px solid green';
        return true;
    }
}

function checkDateEnd() {
    var dateend = document.querySelector('.dateend');
    var pattern = /^\d{2}[.-/]\d{2}[.-/]\d{4}$/;

    if (dateend.value == "" || dateend.value == null) {
        dateend.style.border = '1px solid #B80000';
        return false;
    } else if (!dateend.matches(pattern)) {
        dateend.style.border = '1px solid #B80000';
        return false;
    } else {
        dateend.style.border = '1px solid green';
        return true;
    }
}

function checkForget() {
    var news = document.querySelector(".news");
    var confirm = document.querySelector('.confirm');
    if (news.value == "" && confirm.value.length < 8) {
        news.style.border = '1px solid #B80000';
        return false;
    } else {
        news.style.border = '1px solid green';
        if (confirm.value == "" && confirm.value.length < 8) {
            confirm.style.border = '1px solid #B80000';
            return false;
        } else {
            confirm.style.border = '1px solid green';
            return true;
        }
        return true;
    }

}
function checkConfirm() {
    var newpass = document.querySelector('.newpass');
    var newpasscon = document.querySelector(".confirm");

    if (newpasscon.value == "" && newpasscon.value.length < 8) {
        newpasscon.style.border = '1px solid #B80000';
        return false;

    }
    else if (newpasscon.value != newpass.value) {
        newpasscon.style.border = '1px solid #B80000';
        return false;
    }
    else if (newpasscon.value == newpass.value) {
        newpasscon.style.border = '1px solid green';
        return true;
    }
}
function checkPassNew() {
    var passnew = document.querySelector('.newpass');
    if (passnew.value == "" || passnew.value == null) {
        passnew.style.border = '1px solid #B80000';
        return false;
    } else if (passnew.value.length < 8) {
        passnew.style.border = '1px solid #B80000';
        return false
    }
    else {
        passnew.style.border = '1px solid green';
        return true;
    }
}

