var dropZone1 = document.getElementById('dropZone1');
var pic1 = document.getElementById('id1').value;
var uploadedImage = $('#uploadedImage');

dropZone1.addEventListener('dragover', function (e) {
    e.preventDefault();
    dropZone1.classList.add('drag-over');
});

dropZone1.addEventListener('dragleave', function (e) {
    e.preventDefault();
    dropZone1.classList.remove('drag-over');
});

dropZone1.addEventListener('drop', function (e) {
    e.preventDefault();
    dropZone1.classList.remove('drag-over');

    var files = e.dataTransfer.files;
    handleFiles(files);
});

function handleFiles(files) {
    for (var i = 0; i < files.length; i++) {
        var file = files[i];
        console.log('File name: ' + file.name);
        console.log(pic1);

        // Tạo FormData để gửi file lên server
        var formData = new FormData();
        formData.append('file', file);
        formData.append('userID', pic1);

        // Sử dụng Ajax để gửi file lên server
        $.ajax({
            url: '/Account/UploadFiles?id=' + pic1,
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
                console.log(response.Message);
                /*uploadedImage.attr('src', response.imageUrl);*/
                window.location.href = '/Account/Add';
            },
            error: function (error) {
                console.error('Error uploading files: ' + error.responseText);
            }
        });
    }
};

$(document).ready(function () {
    var openFileInput1 = $("#dropZone1");
    var fileInput1 = $("#fileInput1");
    var isClickHandled = false;

    openFileInput1.on('click', function () {
        if (!isClickHandled) {
            isClickHandled = true;
            fileInput1.click();
        }
    });

    fileInput1.on('change', function () {
        var selectedFileName = $(this).val();
        alert('Selected File: ' + selectedFileName);
        // Thực hiện xử lý file ở đây nếu cần
        isClickHandled = false; // Đặt lại trạng thái để chuẩn bị cho lần click tiếp theo
    });
});