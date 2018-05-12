
$(document).ready(function () {
    $(".mandatory-field").blur(function () {
        fieldValue = $(this).val();
        if (fieldValue == '')
            $(this).addClass('is-invalid');
        else
            $(this).removeClass('is-invalid');
    });
    $("#js-spinner").spinner({ "radius": 25, "color": "#18d26e" });
});

function showAlertDialog(action,text) {
    snackbar.show({ text: text, action: action });
}

function showLoader() {
    $("#divSpinnerLoader,#divHeaderLoader").show();
}

function hideLoader() {
    $("#divSpinnerLoader,#divHeaderLoader").hide();

}

function validateEmail(email) {
    var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    return expr.test(email);
};


function checkIfDivFieldEmpty(id) {
    var isFieldEmpty = false;
    $('#' + id + " .mandatory-field").each(function () {
        fieldValue = $(this).val();
        if (fieldValue == '' || fieldValue == '0') {
            isFieldEmpty = true;
            $(this).addClass('is-invalid');
        }
        else
            $(this).removeClass('is-invalid');
    });
    return isFieldEmpty;
}

function checkIfFieldEmpty() {
    $(".mandatory-field").each(function () {
        fieldValue = $(this).val();
        if (fieldValue == '')
            $(this).addClass('is-invalid');
        else
            $(this).removeClass('is-invalid');
    })
}

