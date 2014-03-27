$(function () {
    'use strict';

    var url = '/Backload/UploadHandler';

    $('#fileupload').fileupload({
        url: url,
        dataType: "json",
        dropZone: $('#dropzone'),
        autoUpload: true,
        add: function (e, data) {
            alert(data.files[0].name);
        }
    });  
});
