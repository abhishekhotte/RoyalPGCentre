
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
}

function validateDate(currVal) {
    if (currVal == '') return false;
    //Declare Regex  
    var rxDatePattern = /^(\d{1,2})(\/|-)(?:(\d{1,2})|(jan)|(feb)|(mar)|(apr)|(may)|(jun)|(jul)|(aug)|(sep)|(oct)|(nov)|(dec))(\/|-)(\d{4})$/i;
    var dtArray = currVal.match(rxDatePattern);
    if (dtArray == null) return false;
    var dtDay = parseInt(dtArray[1]);
    var dtMonth = parseInt(dtArray[3]);
    var dtYear = parseInt(dtArray[17]);
    if (isNaN(dtMonth)) {
        for (var i = 4; i <= 15; i++) {
            if ((dtArray[i])) {
                dtMonth = i - 3;
                break;
            }
        }
    }
    if (dtMonth < 1 || dtMonth > 12) return false;
    else if (dtDay < 1 || dtDay > 31) return false;
    else if ((dtMonth == 4 || dtMonth == 6 || dtMonth == 9 || dtMonth == 11) && dtDay == 31) return false;
    else if (dtMonth == 2) {
        var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0));
        if (dtDay > 29 || (dtDay == 29 && !isleap)) return false;
    }
    return true;
}


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

