function checkSuccess() {
    toast({
        title: "Success!",
        message: "Add Product successful.",
        type: "success",
        duration: 100000
    });
};

function checkDelete() {
    toast({
        title: "Success!",
        message: "Delete Product in cart successful.",
        type: "success",
        duration: 100000
    });
};

function checkNews() {
    toast({
        title: "Success!",
        message: "Send successful.",
        type: "success",
        duration: 100000
    });
};


function checkLoginError() {
    toast({
        title: "Error!",
        message: "Login fail.",
        type: "error",
        duration: 100000
    });
};


function checkOrder() {
    toast({
        title: "Success!",
        message: "Order successful.",
        type: "success",
        duration: 100000
    });
};

function checkErrorOrder() {
    toast({
        title: "Error!",
        message: "Order fail.",
        type: "error",
        duration: 100000
    });
};

function checkRegister() {
    toast({
        title: "Success!",
        message: "Register Account successful.",
        type: "success",
        duration: 100000
    });
};

function checkRegisterError() {
    toast({
        title: "Error!",
        message: "Register Account fail.",
        type: "error",
        duration: 100000
    });
};

function checkGeneral() {
    toast({
        title: "Success!",
        message: "Change Information successful.",
        type: "success",
        duration: 100000
    });
};

function checkGeneralError() {
    toast({
        title: "Error!",
        message: "Change Information fail.",
        type: "error",
        duration: 100000
    });
};


function checkPassGeneral() {
    toast({
        title: "Success!",
        message: "Change Password successful.",
        type: "success",
        duration: 100000
    });
};


function checkPassGeneralError() {
    toast({
        title: "Error!",
        message: "Change Password fail.",
        type: "error",
        duration: 100000
    });
};


function checkVeriError() {
    toast({
        title: "Error!",
        message: "Verification Code not match.",
        type: "error",
        duration: 100000
    });
};


function checkChangeFrom() {
    toast({
        title: "Success!",
        message: "Change Password successful.",
        type: "success",
        duration: 100000
    });
};


function checkMailer() {
    toast({
        title: "Success!",
        message: "Email has been sent.",
        type: "success",
        duration: 100000
    });
};


function checkCheckMailError() {
    toast({
        title: "Error!",
        message: "Email don't exist.",
        type: "error",
        duration: 100000
    });
};

function checkChangeFromErrorMg() {
    toast({
        title: "Error!",
        message: "Change Password fail.",
        type: "error",
        duration: 100000
    });
};

function checkCollection() {
    toast({
        title: "Success!",
        message: "Send Information successful.",
        type: "success",
        duration: 100000
    });
};

function checkError() {
    toast({
        title: "Fail",
        message: "Add Product fail.",
        type: "error",
        duration: 100000
    });
};

function checkStatus() {
    const success = document.querySelector('.success').value;
    const fail = document.querySelector('.fail');
    console.log(success);
    if (success != "") {
        checkSuccess();
    } else if (fail != "") {
        checkError();
    }
};

function toast({ title = "", message = "", type = "info", duration = 10000000 }) {
    const main = document.getElementById("toast");
    if (main) {
        const toast = document.createElement("div");

        // Auto remove toast
        const autoRemoveId = setTimeout(function () {
            main.removeChild(toast);
        }, duration + 1000);

        // Remove toast when clicked
        toast.onclick = function (e) {
            if (e.target.closest(".toast__close")) {
                main.removeChild(toast);
                clearTimeout(autoRemoveId);
            }
        };

        const icons = {
            success: "fas fa-check-circle",
            info: "fas fa-info-circle",
            warning: "fas fa-exclamation-circle",
            error: "fas fa-exclamation-circle"
        };
        const icon = icons[type];
        const delay = (duration / 100).toFixed(2);

        toast.classList.add("toast", `toast--${type}`);
        toast.style.animation = `slideInLeft ease 2s`;
        toast.style.display = 'block';
        toast.innerHTML = `
                <div class="toast__icon">
                    <i class="${icon}"></i>
                </div>
                <div class="toast__body">
                    <h3 class="toast__title"><b>${title}</b></h3>
                    <p class="toast__msg">${message}</p>
                </div>
                <div class="toast__close">
                    <i class="fas fa-times"></i>
                </div>
            `;
        main.appendChild(toast);
    }
};