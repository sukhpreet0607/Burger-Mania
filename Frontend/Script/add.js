const URL = "https://localhost:7219/api/Order";

async function addToCart(divId) {
    var div = document.getElementById(divId);
    var selectElement = div.querySelector('select');
    var span = div.querySelector('span');
    var burger = span.innerText;
    var category = selectElement.options[selectElement.selectedIndex].value;
    // console.log(category);
    // console.log(burger);

    try {
        const response = await fetch(URL);
        if (!response.ok) {
            throw new Error(`Response status: ${response.status}`);
        }
        const myOrders = await response.json();
        // console.log(myOrders);

        for (let item in myOrders) {
            if (myOrders[item].userId == localStorage.getItem("userId") && myOrders[item].burger == burger && myOrders[item].category == category && !myOrders[item].isCheckout) {
                const updatedItem = {
                    "itemId": myOrders[item].itemId,
                    "burger": myOrders[item].burger,
                    "category": myOrders[item].category,
                    "price": myOrders[item].price,
                    "quantity": myOrders[item].quantity + 1,
                    "totalPrice": myOrders[item].totalPrice,
                    "userId": localStorage.getItem("userId"),
                    "isCheckout": myOrders[item].isCheckout
                };
                const response = await fetch(URL + "/UpdateOrder", {
                    method: 'PUT',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({ "Burger": myOrders[item].burger, "Category": myOrders[item].category, "UpdatedItem": updatedItem })
                })
                return response;
            }
        }

        const newItem = {
            "burger": burger,
            "category": category,
            "quantity": 1,
            "userId": localStorage.getItem("userId"),
            "isCheckout": false
        };
        const res = await fetch(URL + "/AddOrder", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(newItem)
        });

        return res;

    }
    catch (error) {
        console.log("Error encountered while accessing Database : ", error);
    }

}


