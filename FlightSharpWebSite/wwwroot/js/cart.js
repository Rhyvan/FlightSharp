Run();
const add = "plus";
const minus = "minus";

function Run() {
    document.querySelector("table").addEventListener("click", (event) => {
        if (event.target.classList.contains("minus")) {
            changeQuantityAndSend(event.target, minus);
        }
        if (event.target.classList.contains("plus")) {
            changeQuantityAndSend(event.target, add);
        }
        if (event.target.classList.contains("del")) {
            deleteAllItemFromCart(event.target)
        }
    });
}


function changeQuantityAndSend(target, option) {
    let selected = target.closest(`[data-json]`);
    let ticket = selected.dataset.json;
    let ticketJson = JSON.parse(ticket);
    let amountNode = selected.querySelector(".item-amount");
    let change = 0;

    if (option == add) {
        change = 1;
    } else if (option == minus) {
        change = -1
    }

    let { amount, deletable } = checkGetNewAmountIsValid(parseInt(amountNode.innerText), change);
    ticketJson.Quantity = amount;

    if (deletable) {
        deleteItemFromCart(selected);
    }

    makePostRequest("api/cart", ticketJson, () => {
        amountNode.innerHTML = parseInt(amountNode.innerText) + change;
    });
}

function checkGetNewAmountIsValid(original, change) {
    let newAmount = original + change;

    if (newAmount > 0) {
        return { amount: change, deletable: false };
    }
    if (newAmount == 0) {
        return { amount: change, deletable: true } 
    }
    if (newAmount < 0) { 
        return { amount: original, deletable: true}
    }
}
/**
* Deletes a DOM element.
* @param target The target DOM element, which will be deleted
*/
function deleteItemFromCart(target) {
    target.parentNode.removeChild(target);
}

function deleteAllItemFromCart(buttonTarget) {

    let targetRow = buttonTarget.closest("[data-json]");
    let ticketJson = JSON.parse(targetRow.dataset.json);

    makePostRequest("api/delete", ticketJson, () => {
        deleteItemFromCart(targetRow);
    });
}

function makePostRequest(route, data, callback) {
    fetch(route, {
        method: 'POST',
        credentials: 'same-origin',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    })
        .then(response => callback())
        .catch(() => errorHandler());
}

function errorHandler() {
    console.log("some error occurred");
}