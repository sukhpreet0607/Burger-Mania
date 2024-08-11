const URL = "https://localhost:7219/api/Order";

var totalQuantity = 0;
var totalPrice = 0;

async function display() {
    try {
        const response = await fetch(URL);
        if (!response.ok) {
            throw new Error(`Response status: ${response.status}`);
        }
        const myOrders = await response.json();
        // console.log(myOrders);

        myOrders.forEach((item) => {
            if (!item.isCheckout && item.userId == localStorage.getItem("userId")) {
                let row = document.createElement('tr');
                for (let key in item) {
                    if (key != "userId" && key != "isCheckout") {
                        let col = document.createElement('td');
                        col.innerText = item[key];
                        row.appendChild(col);
                    }
                };
                let col = document.createElement('td');
                let button = document.createElement('button');
                button.innerText = 'Remove';
                button.onclick = () => removeFromCart(item.burger, item.category);

                col.appendChild(button);
                row.appendChild(col);
                document.getElementById('itemsTable').appendChild(row);

                totalQuantity += item.quantity;
                totalPrice += item.totalPrice;
            }
        });

        var sp = document.createElement('span');
        sp.innerText = `Total Quantity is ${totalQuantity} & Total Price is Rs. ${totalPrice}/-`;
        sp.style.fontFamily = 'Georgia';
        sp.style.fontSize = '20px';
        document.body.appendChild(sp);

    }
    catch (error) {
        console.log("Erro in displaying cart: ", error);
    }
};


async function removeFromCart(burger, category) {
    try {
        const response = await fetch(URL + `/${burger}/${category}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({"userId":localStorage.getItem("userId")})
        });
        // location.reload();
        return response;
    } catch (error) {
        console.log("Error in removing an item from the cart: ", error);
    }
}

async function placeOrder() {
    var discountPercentage = 0;
    var discountPrice = totalPrice;
    if (totalPrice >= 500 && totalPrice < 1000) {
        discountPercentage = 5;
        discountPrice = totalPrice - (totalPrice * discountPercentage) / 100;
    }
    else if (totalPrice >= 1000) {
        discountPercentage = 10;
        discountPrice = totalPrice - (totalPrice * discountPercentage) / 100;
    }
    const msg = `Total Quantity is ${totalQuantity} & Total Price is Rs. ${totalPrice} you will get ${discountPercentage}% discount & Total Price after Discount is Rs. ${discountPrice}/-`;


    try {
        const response = await fetch(URL);
        if (!response.ok) {
            throw new Error(`Response status: ${response.status}`);
        }
        const myOrders = await response.json();

        for (let item in myOrders) {
            if (myOrders[item].userId == localStorage.getItem("userId")) {
                const updatedItem = myOrders[item];
                updatedItem.isCheckout = true;
                await fetch(URL + `/${myOrders[item].burger}/${myOrders[item].category}`, {
                    method: 'PUT',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(updatedItem)
                })
            }
        }
        alert(msg);
        location.reload();
    }
    catch (error) {
        console.log("Error encountered while placing order: ", error);
    }

}

display();


