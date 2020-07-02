var searchBtn = document.getElementById("search");
var divForResults = document.getElementById("showResults");
let buttons;

function addEventListeners(buttonsList)
{
    buttonsList.forEach(function (currentBtn) {
        currentBtn.addEventListener('click', function () {
            var flightData = currentBtn.getAttribute("jsonData");
            var obj = JSON.parse(flightData);

            var price = obj.priceHUF;
            var airLine = obj.airLine;
            var departure = obj.departure;
            var destination = obj.destination;
            var expires = obj.expirationDate;
            var returnDate = obj.return;
            var flightNum = obj.flightNo;
            var origin = obj.origin;


            var jsonToPost =
            {
                "Flight": {
                    "PriceHUF": price,
                    "AirLine": airLine,
                    "Return": returnDate,
                    "Destination": destination,
                    "FlightNo": flightNum,
                    "ExpirationDate" : expires,
                    "Departure": departure,
                    "Origin": origin
                },
                "Quantity": 1
            }

            makePostRequest("api/cart", JSON.stringify(jsonToPost));
        }
        )
    })
}

function makePostRequest(whereToSend, whatToSend)
{
    fetch(whereToSend, {
        method: 'POST',
        credentials: 'same-origin',
        headers: {
            'Content-Type': 'application/json'
        },
        body: whatToSend
    })
}

// handling click event on search button, also some input validation
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

// send GET request to APIController for retrieving flight data
function GetFlights(fromPlace, toPlace, callback) {
    fetch(`api/search?origin=${fromPlace}&destination=${toPlace}`, {
        method: 'GET',
        credentials: 'same-origin'
    })
        .then(response => response.json())
        .then(json_response => callback(json_response));
}

const createAndSetFlightsHTML = function (arrayOfFlights)
{
    //divForResults.innerHTML = "";
    console.log(arrayOfFlights[0]);
    // create table with header
    let table = document.createElement("table");
    table.className = "divInputs";
    let header = document.createElement("tr");

    table.appendChild(header);

    let thForAirLine = document.createElement("th");
    thForAirLine.appendChild(document.createTextNode("AirLine"));
    header.appendChild(thForAirLine);

    let thForOrigin = document.createElement("th");
    thForOrigin.appendChild(document.createTextNode("From"));
    header.appendChild(thForOrigin);

    let thForDestination = document.createElement("th");
    thForDestination.appendChild(document.createTextNode("To"));
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

    // create and set rows with provided flight data
    for (var i = 0; i < arrayOfFlights.length; i++) {
        let nextTR = document.createElement("tr");

        let tdForAirLine = document.createElement("td");
        tdForAirLine.appendChild(document.createTextNode(`${JSON.stringify(arrayOfFlights[i].airLine)}`));
        nextTR.appendChild(tdForAirLine);

        let tdForOrigin = document.createElement("td");
        tdForOrigin.appendChild(document.createTextNode(`${JSON.stringify(arrayOfFlights[i].origin)}`));
        nextTR.appendChild(tdForOrigin);

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
        tdButton.setAttribute("origin", JSON.stringify(arrayOfFlights[i].Origin))
        tdButton.id = "AddBTN";
        tdButton.className = "blueBTN";
        tdButton.textContent = "Add";
        nextTR.appendChild(tdButton);

        table.appendChild(nextTR);
    }
    // add buttons with eventlisteners to be able to send PostReq
    buttons = document.querySelectorAll("#AddBTN");
    addEventListeners(buttons);
}