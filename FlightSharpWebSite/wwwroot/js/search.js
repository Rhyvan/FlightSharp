var searchBtn = document.getElementById("search");
var divForResults = document.getElementById("showResults");


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
        GetFlights(from, to, createAndSetFlightsHTML);
        
    }
}


function hasNumber(myString) {
    return /\d/.test(myString);
}

function GetFlightToAdd(numberInList) {
    fetch(`api/addlFlight?numInFlightList=${numberInList}`, {
        method: 'GET',
        credentials: 'same-origin'
    })
        /*.then(response => response.json())
        .then(json_response => callback(json_response))*/
}

function GetFlights(fromPlace, toPlace, callback) {
    fetch(`api/search?origin=${fromPlace}&destination=${toPlace}`, {
        method: 'GET',
        credentials: 'same-origin'
    })
        .then(response => response.json())
        .then(json_response => callback(json_response))
}

const createAndSetFlightsHTML = function (arrayOfFlights)
{
    /*<button class="blueBTN" id="search">Search</button>*/
    //divForResults.innerHTML = "";
    let table = document.createElement("table");
    table.className = "divInputs";
    let header = document.createElement("tr");

    table.appendChild(header);

    let thForAirLine = document.createElement("th");
    thForAirLine.appendChild(document.createTextNode("AirLine"));
    header.appendChild(thForAirLine);

    let thForDestination = document.createElement("th");
    thForDestination.appendChild(document.createTextNode("Destination"));
    header.appendChild(thForDestination);

    let thForPrice = document.createElement("th");
    thForPrice.appendChild(document.createTextNode("Price"));
    header.appendChild(thForPrice);

    let thForDeparture = document.createElement("th");
    thForDeparture.appendChild(document.createTextNode("Departure"));
    header.appendChild(thForDeparture);

    let thForReturn = document.createElement("th");
    thForReturn.appendChild(document.createTextNode("Return"));
    header.appendChild(thForReturn);

    let thForButton = document.createElement("th");
    thForButton.appendChild(document.createTextNode("Add to cart"));
    header.appendChild(thForButton);

    divForResults.appendChild(table);


    for (var i = 0; i < arrayOfFlights.length; i++) {
        let nextTR = document.createElement("tr");
        //nextTR.setAttribute("jsonData", JSON.stringify(arrayOfFlights[i]));

        let tdForAirLine = document.createElement("td");
        tdForAirLine.appendChild(document.createTextNode(`${JSON.stringify(arrayOfFlights[i].airLine)}`));
        nextTR.appendChild(tdForAirLine);

        let tdForDestination = document.createElement("td");
        tdForDestination.appendChild(document.createTextNode(`${JSON.stringify(arrayOfFlights[i].destination)}`));
        nextTR.appendChild(tdForDestination);

        let tdForPrice = document.createElement("td");
        tdForPrice.appendChild(document.createTextNode(`${JSON.stringify(arrayOfFlights[i].priceHUF)}`));
        nextTR.appendChild(tdForPrice);

        let tdForDeparture = document.createElement("td");
        tdForDeparture.appendChild(document.createTextNode(`${JSON.stringify(arrayOfFlights[i].departure)}`));
        nextTR.appendChild(tdForDeparture);

        let tdForReturn = document.createElement("td");
        tdForReturn.appendChild(document.createTextNode(`${JSON.stringify(arrayOfFlights[i].return)}`));
        nextTR.appendChild(tdForReturn);

        let tdButton = document.createElement("button");
        tdButton.setAttribute("jsonData", JSON.stringify(arrayOfFlights[i]));
        tdButton.className = "blueBTN";
        tdButton.textContent = "Add";
        nextTR.appendChild(tdButton);

        table.appendChild(nextTR);


        JSON.stringify(arrayOfFlights[i]);
        console.log(JSON.stringify(arrayOfFlights[i]));
    }

}

/*return priceHUF destination departure flightNo airLine*/