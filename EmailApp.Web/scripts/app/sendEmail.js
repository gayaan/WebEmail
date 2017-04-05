$(document).ready(function () {
    $('#btnSendEmail').on('click', function (e) {
        e.preventDefault();

        //TODO: User input validation required

        if (!validateEmail()) {
            return;
        }

        var url = "/EmailApp/Api/email";
        var formData = getEmailData($('#toAddresses').val(), $('#ccAddresses').val(), $('#bccAddresses').val(), $('#emailSubject').val(), $('#emailMessage').val());
        var data = JSON.stringify(formData);

        $('#statusText').html('');
        $('#btnSendEmail').prop('disabled', true);

        $.ajax({
            type: "POST",
            url: url,
            data: data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $('#statusText').append('<i>Your email is sent successfully.</i>');
                enableSubmit();
            },
            error: function (jqXHR, status, error) {
                handleError(jqXHR);
                enableSubmit();
            }
        });
    });
});

function validateEmail() {
    var toAddress = $('#toAddresses').val();

    if (toAddress == "") {
        alert("Please enter To email address");
        return false;
    }

    return true;
}

function getEmailData(to, cc, bcc, subject, message) {
    var toAddresses = to ? to.split(';') : [];
    var ccAddresses = cc ? cc.split(';') : [];
    var bccAddresses = bcc ? bcc.split(';') : [];

    return { to: toAddresses, cc: ccAddresses, bcc: bccAddresses, subject: subject, message: message }
}

function handleError(jqXHR) {
    $('#statusText').html('');

    if (jqXHR.status === 400) {
        $('#statusText').append('<i>Email not sent. Please check your message details and try again.</i>');
        return;
    }

    $('#statusText').append('<i>An unexpected error occured. Please try again later.</i>');
}

function enableSubmit() {
    $('#btnSendEmail').prop('disabled', false);
}