const sideMenu = document.querySelector('aside');
const menu = document.getElementById('siderbar');
const btns = menu.getElementsByClassName('btns');
const menuBtn = document.getElementById('menu-btn');
const closeBtn = document.getElementById('close-btn');
const darkMode = document.querySelector('.dark-mode');
const body = document.querySelector("body"),
    sidebar = body.querySelector(".sidebar"),
    toggle = body.querySelector(".toggle"),
    searchBtn = body.querySelector(".search-box"),
    modeSwitch = body.querySelector(".toggle-switch"),
    modeText = body.querySelector(".mode-text");

menuBtn.addEventListener('click', () => {
    sideMenu.style.display = 'block';
});

closeBtn.addEventListener('click', () => {
    sideMenu.style.display = 'none';
    sideMenu.style.transition = '.5s all ease';
});

let mode = localStorage.getItem('darkmode');
if (mode == 'true') {
    body.classList.add("dark-mode-variables");
    modeText.innerText = "Light Mode";
} else { }
modeSwitch.addEventListener('click', () => {
    if (body.classList.contains("dark-mode-variables")) {
        modeText.innerText = "Dark Mode";
    } else {
        modeText.innerText = "Light Mode";
    }
    let mode = body.classList.toggle('dark-mode-variables');
    // save mode
    localStorage.setItem('darkmode', mode);
});

$(document).ready(function () {
    var url = window.location.pathname,
        urlRegExp = new RegExp(url.replace(/\/$/, ''));
    $('#siderbar a.btns').each(function () {
        if (urlRegExp.test(this.href)) {
            $(this).addClass('active');
        } else {
            $(this).removeClass('active');
        }
    });
});


//Orders.forEach(order => {
//    const tr = document.createElement('tr');
//    const trContent = `
//        <td>${order.productName}</td>
//        <td>${order.productNumber}</td>
//        <td>${order.paymentStatus}</td>
//        <td class="${order.status === 'Declined' ? 'danger' : order.status === 'Pending' ? 'warning' : 'primary'}">${order.status}</td>
//        <td class="primary">Details</td>
//    `;
//    tr.innerHTML = trContent;
//    document.querySelector('table tbody').appendChild(tr);
//});