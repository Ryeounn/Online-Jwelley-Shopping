var dropZone = document.getElementById('dropZone');
var userID = document.getElementById('id').value;
var uploadedImage = $('#uploadedImage');

dropZone.addEventListener('dragover', function (e) {
    e.preventDefault();
    dropZone.classList.add('drag-over');
});

dropZone.addEventListener('dragleave', function (e) {
    e.preventDefault();
    dropZone.classList.remove('drag-over');
});

dropZone.addEventListener('drop', function (e) {
    e.preventDefault();
    dropZone.classList.remove('drag-over');

    var files = e.dataTransfer.files;
    handleFiles(files);
});

function handleFiles(files) {
    for (var i = 0; i < files.length; i++) {
        var file = files[i];
        console.log('File name: ' + file.name);
        console.log(userID);

        // Tạo FormData để gửi file lên server
        var formData = new FormData();
        formData.append('file', file);
        formData.append('userID', userID);

        // Sử dụng Ajax để gửi file lên server
        $.ajax({
            url: '/Account/UploadFiles?id=' + userID,
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
                console.log(response.Message);
                /*uploadedImage.attr('src', response.imageUrl);*/
                window.location.href = '/Account/General?id=' + userID;
            },
            error: function (error) {
                console.error('Error uploading files: ' + error.responseText);
            }
        });
    }
};

$(document).ready(function () {
    var openFileInput = $("#dropZone");
    var fileInput = $("#fileInput");
    var isClickHandled = false;

    openFileInput.on('click', function () {
        if (!isClickHandled) {
            isClickHandled = true;
            fileInput.click();
        }
    });

    fileInput.on('change', function () {
        var selectedFileName = $(this).val();
        alert('Selected File: ' + selectedFileName);
        // Thực hiện xử lý file ở đây nếu cần
        isClickHandled = false; // Đặt lại trạng thái để chuẩn bị cho lần click tiếp theo
    });
});