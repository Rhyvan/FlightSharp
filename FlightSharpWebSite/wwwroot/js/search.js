var searchBtn = document.getElementById("search");
var divForResults = document.getElementById("showResults");
let buttons;
let toastCounter = 0;
const toastContainer = document.querySelector("#toastContainer");

function addEventListeners(buttonsList) {
    buttonsList.forEach(function(currentBtn) {
        currentBtn.addEventListener('click',
            function () {
                var flightData = currentBtn.getAttribute("jsonData");

                var jsonTicket =
                {
                    "Flight": JSON.parse(flightData),
                    "Quantity": 1
                };

                makePostRequest("api/cart", JSON.stringify(jsonTicket));
        }
        )
    })
}

function makePostRequest(whereToSend, whatToSend) {
    fetch(whereToSend, {
        method: 'POST',
        credentials: 'same-origin',
        headers: {
            'Content-Type': 'application/json'
        },
        body: whatToSend
    })
        .then(response => {
            if (response.ok) {
                showToast("Successfully added to cart.", "info");
            } else {
                showToast("Some error occurred", "warning");
            }
        });
}

// handling click event on search button, also some input validation
searchBtn.onclick = function () {
    var from = document.getElementById("from").value.toUpperCase();
    var to = document.getElementById("to").value.toUpperCase();
    var currencyElement = document.getElementById("currency");
    var currency = currencyElement.options[currencyElement.selectedIndex].text.toUpperCase();

    if (hasNumber(from) || hasNumber(to)) {
        alert("Inputs can not contain numbers!");
    }
    else if (from.length != 3 || to.length != 3) {
        alert("Both airport codes must be 3 characters long!");
    }
    else {
        let maxPrice = document.getElementById("maxPrice").value;

        if (maxPrice.length == 0 || (maxPrice.length > 0 && maxPrice[0] == 0)) {
            maxPrice = 0;
        }

        GetFlights(from, to, createAndSetFlightsHTML, maxPrice, currency);
    }
}


function hasNumber(myString) {
    return /\d/.test(myString);
}

// send GET request to APIController for retrieving flight data
function GetFlights(fromPlace, toPlace, callback, maxPrice, currency) {
    fetch(`api/search?origin=${fromPlace}&destination=${toPlace}&price=${maxPrice}&currency=${currency}`, {
        method: 'GET',
        credentials: 'same-origin'
    })
        .then(response => response.json())
        .then(json_response => callback(json_response));
}

const createAndSetFlightsHTML = function (arrayOfFlights)
{

    // clear previous search results
    while (divForResults.firstChild) {
        divForResults.removeChild(divForResults.firstChild);
    } 

    // create table with header
    let table = document.createElement("table");
    table.className = "divInputs";
    table.classList.add("table", "table-bordered");
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
        tdForPrice.appendChild(document.createTextNode(`${JSON.stringify(arrayOfFlights[i].price)}`));
        nextTR.appendChild(tdForPrice);

        let tdForDeparture = document.createElement("td");
        tdForDeparture.appendChild(document.createTextNode(ConvertDateTime(arrayOfFlights[i].departure)));
        nextTR.appendChild(tdForDeparture);

        let tdForReturn = document.createElement("td");
        tdForReturn.appendChild(document.createTextNode(ConvertDateTime(arrayOfFlights[i].return)));
        nextTR.appendChild(tdForReturn);

        let tdForButton = document.createElement("td");
        let tdButton = document.createElement("button");
        tdButton.setAttribute("jsonData", JSON.stringify(arrayOfFlights[i]));
        tdButton.setAttribute("origin", JSON.stringify(arrayOfFlights[i].Origin))
        tdButton.id = "AddBTN";
        tdButton.className = "btn-primary";
        tdButton.textContent = "Add";
        tdForButton.appendChild(tdButton);
        nextTR.appendChild(tdForButton);

        table.appendChild(nextTR);
    }
    // add buttons with eventlisteners to be able to send PostReq
    buttons = document.querySelectorAll("#AddBTN");
    addEventListeners(buttons);
}

function ConvertDateTime(dateTime) {
    let date = new Date(dateTime);
    return date.getFullYear()
        + "." + ("0" + (date.getMonth() + 1)).slice(-2)
        + "." + (date.getUTCDate())
        + " " + date.getHours() + ":" + ("0" + date.getMinutes()).slice(-2);
}




function showToast(message, bgColor) {
    let toastId = 'toast' + toastCounter;
    let toast = `
    <div id="${toastId}" class="toast" role="alert" aria-live="assertive" aria-atomic="true" data-autohide="true" data-delay=2000>
      <div class="toast-body bg-${bgColor} text-white">
        <strong class="mr-auto">${message}</strong>
        <button type="button" class="ml-2 mb-1 close" data-dismiss="toast" aria-label="Close">
        <span aria-hidden="true">&times;</span>
        </button>
      </div>
    </div>`;
    toastContainer.insertAdjacentHTML("afterbegin", toast);
    $(`#${toastId}`).toast("show");
    $(`#${toastId}`).on('hidden.bs.toast', function () {
        $(this).remove();
    })

    toastCounter++;
}