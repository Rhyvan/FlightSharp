var searchBtn = document.getElementById("search");

searchBtn.onclick = function () {
    var from = document.getElementById("from").value.toUpperCase();
    var to = document.getElementById("to").value.toUpperCase();

    if (hasNumber(from) || hasNumber(to)) {
        alert("Inputs can not contain numbers!");
    }
    else if (from.length != 3 || to.length != 3) {
        alert("Both airport codes must be 3 characters long!");
    }
    else {
        var toSend = {
            fromPlace: from,
            toPlace: to
        }

        var jsonString = JSON.stringify(toSend);
        GetReq(from, to);
    }
}

function GetReq(fromPlace, toPlace) {
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open("GET", `api/search?origin=${fromPlace}&destination=${toPlace}`, true);
    xmlHttp.send(null);
}

function hasNumber(myString) {
    return /\d/.test(myString);
}