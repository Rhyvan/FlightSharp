var searchBtn = document.getElementById("search");

searchBtn.onclick = function () {
    var from = document.getElementById("from").value;
    var to = document.getElementById("to").value;

    var toSend = {
        fromPlace: from,
        toPlace: to
    }

    var jsonString = JSON.stringify(toSend);
    GetReq(from, to);
}

function GetReq(fromPlace, toPlace) {
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open("GET", `api/search?origin=${fromPlace}&destination=${toPlace}`, true);
    xmlHttp.send(null);
}