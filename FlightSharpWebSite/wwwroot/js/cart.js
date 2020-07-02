function addEventListeners(buttonsList) {
    buttonsList.forEach(function (currentBtn) {
        currentBtn.addEventListener('click',
            function () {
                var flightData = currentBtn.getAttribute("jsonData");
                var obj = JSON.parse(flightData);

                var price = obj.priceHUF;
                var airLine = obj.airLine;
                var departure = obj.departure;
                var destination = obj.destination;
                var expires = obj.expirationDate;
                var returnDate = obj.return;
                var flightNum = obj.flightNo;


                var jsonToPost =
                {
                    "Flight": {
                        "PriceHUF": price,
                        "AirLine": airLine,
                        "Return": returnDate,
                        "Destination": destination,
                        "FlightNo": flightNum,
                        "ExpirationDate": expires,
                        "Departure": departure
                    },
                    "Quantity": 1
                }

                makePostRequest("api/cart", JSON.stringify(jsonToPost));
            }
        );
    });
}

plusButtons = document.querySelectorAll("plus");
addEventListeners(plusButtons);
