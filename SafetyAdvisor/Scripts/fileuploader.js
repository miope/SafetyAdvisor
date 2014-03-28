$(function () {
    'use strict';

    var url = '/Backload/UploadHandler';

    var wireUpDeleteClick = function () {
        $('.delete-file').click(function (e) {
            e.preventDefault();
            $.ajax({
                // Uncomment the following to send cross-domain cookies:
                //xhrFields: {withCredentials: true},
                url: $(this).data('url'),
                dataType: 'json',
                method: 'delete'
            }).done(function (result) {
                $('#filerows').find('td:contains(' + result.files[0].name + ')').parent().fadeOut('slow', function () { $(this).remove() });
            }).error(function (result) {
                alert(result);
            });
        });
    };

    // initialize the fileupload plugin
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
            $('#filerows').find('td:contains(' + data.result.files[0].name + ')').parent().fadeOut('slow', function () { $this.remove() });
            $(tmpl('template-download', data.result)).hide().prependTo('#filerows').fadeIn('slow');
            wireUpDeleteClick();
        }
    });

    // dropzone effects
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
    // let's grab the list of existing files and add them to the table
    $.ajax({
        // Uncomment the following to send cross-domain cookies:
        //xhrFields: {withCredentials: true},
        url: url,
        dataType: 'json',
        context: $('#fileupload')[0],
        data: { objectContext: $('#objectContext').val() },
    }).done(function (result) {
        $('#filerows').hide().html(tmpl('template-download', result)).fadeIn('slow');
        // let's wire-up the click on delete icon
        wireUpDeleteClick();
    });
});