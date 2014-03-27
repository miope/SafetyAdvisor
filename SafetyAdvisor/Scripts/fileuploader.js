$(function () {
    'use strict';

    var url = '/Backload/UploadHandler';

    $('#fileupload').fileupload({
        url: url,
        dataType: 'json',
        dropZone: $('#dropzone'),
        autoUpload: true,
        maxFileSize: 10485760,
        uploadTemplateId: null,
        start: function (e, data) {
            $('.progress').removeClass('hide');
            $('#overallbar').css('width', '0%');
        },

        progressall: function (e, data) {
            var progress = parseInt(data.loaded / data.total * 100, 10);
            var progressPercent = progress + '%';
            $('#overallbar').css('width', progressPercent).text(progressPercent);
        },

        done: function (e, data) {
            $('.progress').addClass('hide');
            $('#filerows').append(tmpl("template-download", data.result));
        }
    });

    $('#fileupload').addClass('fileupload-processing');
    $.ajax({
        // Uncomment the following to send cross-domain cookies:
        //xhrFields: {withCredentials: true},
        url: url,
        dataType: 'json',
        context: $('#fileupload')[0],
        data: { objectContext: $('#objectContext').val() },
    }).always(function () {
        $(this).removeClass('fileupload-processing');
    }).done(function (result) {
        $(this).fileupload('option', 'done')
            .call(this, null, { result: result });
    });


    $(document).bind('dragover', function (e) {
        var dropZone = $('#dropzone'),
            timeout = window.dropZoneTimeout;
        if (!timeout) {
            dropZone.addClass('in');
        } else {
            clearTimeout(timeout);
        }
        var found = false,
            node = e.target;
        do {
            if (node === dropZone[0]) {
                found = true;
                break;
            }
            node = node.parentNode;
        } while (node != null);
        if (found) {
            dropZone.addClass('hover');
        } else {
            dropZone.removeClass('hover');
        }
        window.dropZoneTimeout = setTimeout(function () {
            window.dropZoneTimeout = null;
            dropZone.removeClass('in hover');
        }, 100);
    });

});
