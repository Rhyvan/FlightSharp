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
        GetFked(from, to, returnJson);
    }
}


function hasNumber(myString) {
    return /\d/.test(myString);
}

function GetFked(fromPlace, toPlace, callback) {
    fetch(`api/search?origin=${fromPlace}&destination=${toPlace}`, {
        method: 'GET',
        credentials: 'same-origin'
    })
        .then(response => response.json())
        .then(json_response => callback(json_response));
}

const returnJson = function (arrayOfFlights)
{
    for (var i = 0; i < arrayOfFlights.length; i++) {
        JSON.stringify(arrayOfFlights[i]);
        console.log(JSON.stringify(arrayOfFlights[i]));
    }

}