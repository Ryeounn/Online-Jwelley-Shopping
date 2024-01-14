
    var colorp = document.getElementById("qq");
    colorp.style.backgroundColor = "#E32636";
    var color = document.getElementById("reneuve");
    color.style.backgroundColor = "#0095B6";
    var coloro = document.getElementById("toiben");
    coloro.style.backgroundColor = "#1B9C85";
    console.log("hello");
    $.ajax({
        url: '/Admin/HomeAdmin/GetChartData',
    type: 'GET',
    success: function (data) {
        handleChartData(data);
        },
    error: function (error) {
        console.error(error);
        }
    });

    $.ajax({
        url: '/Admin/HomeAdmin/getProfitData',
    type: 'GET',
    success: function (data) {
        ProfitInfor(data);
        },
    error: function (error) {
        console.error(error);
        }
    });
    $.ajax({
        url: '/Admin/HomeAdmin/getReneuveData',
    type: 'GET',
    success: function (data) {
        ReneuveInfor(data);
        },
    error: function (error) {
        console.error(error);
        }
    });
    $.ajax({
        url: '/Admin/HomeAdmin/getOrderData',
    type: 'GET',
    success: function (data) {
        OrderInfor(data);
        },
    error: function (error) {
        console.error(error);
        }
    });
    $.ajax({
        url: '/Admin/HomeAdmin/getProductData',
    type: 'GET',
    success: function (data) {
        ProductInfor(data);
        },
    error: function (error) {
        console.error(error);
        }
    });
    function handleChartData(data) {
        console.log(data);
    var ctx3 = document.getElementById('total');
    var colorctx3 = ["red", "blue","green","yellow"];
    var resultData = data.Result;
        var labels = resultData.map(item => item.Year + '/' + item.Month);
        var revenueData = resultData.map(item => item.TotalRevenue);

    new Chart(ctx3, {
        type: 'line',
    data: {
        labels: labels,
    datasets: [{
        label: 'Revenue',
    data: revenueData,
    borderWidth: 1,
    backgroundColor: colorctx3
                }]
            },
    options: {
        scales: {
        y: {
        beginAtZero: true
                    }
                }
            }
        });
    }
    function ProfitInfor(data) {   
        var profit = document.getElementById('profit');
    var proper = document.getElementById('proper');
    /* var revenueData = resultData(item => item.TotalRevenue);*/
        profit.textContent = data.Total + "$";
    console.log(data.Total);

    console.log(data.Totalyear);

    console.log(data.TotalRevenue);
    var total = data.TotalRevenue - data.Totalyear;
    console.log(total);
    var per = ((data.Total * 100) / data.TotalRevenue).toFixed(2);
    console.log(per)
    proper.textContent = per+"%";
    var rangeBar = document.getElementById('rangeBar');
    var percentageWidth = per;
    rangeBar.style.width = percentageWidth + '%';
    }
    function ReneuveInfor(data) {

        var reneuve = document.getElementById('Renueve');
    var reper = document.getElementById('reper');
       
    /* var revenueData = resultData(item => item.TotalRevenue);*/
        reneuve.textContent = data.TotalRevenue + "$";
    var per = ((1 * 100)).toFixed(2);
    console.log(per)
    reper.textContent = per + "%";
    var rangereBar = document.getElementById('rangereBar');
    var percentageWidth = per;
    rangereBar.style.backgroundColor = "#0095B6";
    rangereBar.style.width = percentageWidth + '%';
    }
    function ProductInfor(data) {
    var product = document.getElementById('product');
    var prper = document.getElementById('prper');
    console.log(data.TotalRevenue);
        product.textContent = data.TotalRevenue + " Quantity";
    //tai vi item dau co ngay tao voi lai tinh phan tram cung phai co chi tieu nen cho dai 500 san pham / 1 nam
    var perpro = ((data.TotalRevenue * 100) / 500).toFixed(2);
    prper.textContent = perpro +"%";
    var rangeproBar = document.getElementById('rangeproBar');
    var percentageWidth = perpro;
    rangeproBar.style.backgroundColor = "#E32636";
    rangeproBar.style.width = percentageWidth + '%';
    }
    function OrderInfor(data) {
        var order = document.getElementById('order');
    var orper = document.getElementById('orper');
        order.textContent = data.TotalRevenue + " Quantity";
    // like product :3
    var per_ = ((data.TotalRevenue * 100) / 500).toFixed(2);
    orper.textContent = per_ + "%";
    var rangeBar_ = document.getElementById('rangeorBar');
    var percentageWidth = per_;
        rangeBar_.style.backgroundColor = "#1B9C85";
    rangeBar_.style.width = percentageWidth + '%';
    }