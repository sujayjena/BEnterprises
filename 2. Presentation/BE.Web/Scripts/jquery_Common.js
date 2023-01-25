// Created By System : It is user defind

// CSRF (XSRF) security
function addAntiForgeryToken(data) {
    //if the object is undefined, create a new one.
    if (!data) {
        data = {};
    }
    //add token
    var tokenInput = $('input[name=__RequestVerificationToken]');
    if (tokenInput.length) {
        data.__RequestVerificationToken = tokenInput.val();
    }
    console.log(data);
    return data;
};

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

function getCookieValueByName(CookieName, CookieValue) {
    var ReturnVal = "";
    var CookieVal = readCookie(CookieName);

    if (CookieVal != null && CookieVal != '') {
        var vSplitValueByUnpecent = CookieVal.split('&');
        if (vSplitValueByUnpecent.length > 0) {
            $.each(vSplitValueByUnpecent, function (key, value) {
                var vIndexValue = value.split('=')
                if (vIndexValue[0] == CookieValue) {
                    ReturnVal = vIndexValue[1];
                }
                return ReturnVal;
            });
        }
    }
    return ReturnVal;
}

$(document).ready(function () {
    $(".date").kendoDatePicker({
        format: "{0:dd/MM/yyyy}",
        change: function () {
            //var value = this.value();
            //alert(value);
        }
    });
});


