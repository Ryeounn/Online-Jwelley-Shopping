
function togglePasswordVisibilityChangeNews() {
    var passwordField = document.getElementById("news");
    var showIcon = document.getElementById("showIcon1");
    var hideIcon = document.getElementById("hideIcon1");

    if (passwordField.type === "password") {
        passwordField.type = "text";
        showIcon.style.display = "none";
        hideIcon.style.display = "inline";
    } else {
        passwordField.type = "password";
        showIcon.style.display = "inline";
        hideIcon.style.display = "none";
    }
}


function togglePasswordVisibilityChangeConfirm() {
    var passwordField = document.getElementById("confirm");
    var showIcon = document.getElementById("showIcon2");
    var hideIcon = document.getElementById("hideIcon2");

    if (passwordField.type === "password") {
        passwordField.type = "text";
        showIcon.style.display = "none";
        hideIcon.style.display = "inline";
    } else {
        passwordField.type = "password";
        showIcon.style.display = "inline";
        hideIcon.style.display = "none";
    }
}


function togglePasswordVisibilityCurrentChange() {
    var passwordField = document.getElementById("current");
    var showIcon = document.getElementById("showIcon");
    var hideIcon = document.getElementById("hideIcon");

    if (passwordField.type === "password") {
        passwordField.type = "text";
        showIcon.style.display = "none";
        hideIcon.style.display = "inline";
    } else {
        passwordField.type = "password";
        showIcon.style.display = "inline";
        hideIcon.style.display = "none";
    }
}