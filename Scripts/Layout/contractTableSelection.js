$(function () {
    $("#select-column-dialog").dialog({
        autoOpen: false
    });
    $("#select-column-btn").click(function () {
        $("#select-column-dialog").dialog("open");
        updateColumnsCheckbox();
    });
});

$('div#select-column-dialog').on('dialogclose', function (event) {
    updateSelectedColumns();
});

function readSelectedColumns() {
    if (readCookie("contract-columns") == null) {

        var initCookie = "false|true|true|false|true|true|false|true|false|false|false|false|true"
        createCookie("contract-columns", initCookie, 365);
    }
    var value = readCookie("contract-columns");
    var valList = value.split('|');
    for (var i = 0; i < valList.length - 1; i++) {
        if (valList[i] != 'true') {
            $('td:nth-child(' + (i + 1) + '), th:nth-child(' + (i + 1) + ')').hide();
        } else {
            $('td:nth-child(' + (i + 1) + '), th:nth-child(' + (i + 1) + ')').show();
        }
    }
    }

function updateColumnsCheckbox() {
    if (readCookie("contract-columns") == null) {
        var value = "";
        //Iterates through each Checkbox, to set Value
        $('#select-column-dialog').find('input:checkbox').each(function () {
            $(this).prop('checked', true); // "this" is the current Checkbox in the loop
            value += $(this).is(':checked') + "|";
        });

        createCookie("contract-columns", value, 365);
    }

    var value = readCookie("contract-columns");
    var valList = value.split('|');
    var i = 0;
    $('#select-column-dialog').find('input:checkbox').each(function () {
        $(this).prop('checked', valList[i] == 'true');
        i += 1;
    });
}

function updateSelectedColumns() {
    var value = "";
    var i = 0;
    //Iterates through each Checkbox, to set Value
    $('#select-column-dialog').find('input:checkbox').each(function () {
        $(this).is(':checked')
        value += $(this).is(':checked') + "|";
        if ($(this).is(':checked') != true) {
            $('td:nth-child(' + (i + 1) + '), th:nth-child(' + (i + 1) + ')').hide();
        } else {
            $('td:nth-child(' + (i + 1) + '), th:nth-child(' + (i + 1) + ')').show();
        }
        i += 1;
    });
    createCookie("contract-columns", value, 365);
    console.log(value);
}

function createCookie(name, value, days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else expires = "";
    document.cookie = name + "=" + value + expires + "; path=/";
}

function readCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

window.onload = function (e) {
    readSelectedColumns();
}
/*
window.onunload = function (e) {
    updateSelectedColumns();
}*/