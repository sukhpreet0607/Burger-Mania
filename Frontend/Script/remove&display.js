const URL = "https://localhost:7219/api/BurgerCart";


var totalQuantity = 0;
var totalPrice = 0;

async function display() {
    // var cartItems = JSON.parse(localStorage.getItem('myCart'));

    try {
        const response = await fetch(URL);
        if (!response.ok) {
            throw new Error(`Response status: ${response.status}`);
        }
        const myCart = await response.json();
        console.log(myCart);

        myCart.forEach((item) => {
            let row = document.createElement('tr');
            for (let key in item) {

                let col = document.createElement('td');
                col.innerText = item[key];
                row.appendChild(col);
            };

            let col = document.createElement('td');
            let button = document.createElement('button');
            button.innerText = 'Remove';
            button.onclick = () => removeFromCart(item.burger, item.category);

            col.appendChild(button);
            row.appendChild(col);

            document.getElementById('itemsTable').appendChild(row);
        });

        for (i=0;i<myCart.length;i++)
        {
            totalQuantity += myCart[i].quantity;
            totalPrice += myCart[i].price;
        }

        // myCart.forEach((item) => {
        //     totalQuantity += item.quantity;
        //     totalPrice += item.price;
        // })

        console.log(totalQuantity,totalPrice)

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
    // var cartItems = JSON.parse(localStorage.getItem('myCart'));
    // cartItems.splice(index, 1);
    // localStorage.setItem('myCart', JSON.stringify(cartItems));
    // // location.reload();

    console.log("Into the remove method with :", burger, category);
    try {
        const response = await fetch(URL + `/${burger}/${category}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        location.reload();
        return response;
    } catch (error) {
        console.log("Error in removing an item from the cart: ", error);
    }

}

function placeOrder() {
    var dis = 0;
    var disPrice = totalPrice;
    if (totalPrice >= 500 && totalPrice < 1000) {
        dis = 5;
        disPrice = totalPrice - (totalPrice * dis) / 100;
    }
    else if (totalPrice >= 1000) {
        dis = 10;
        disPrice = totalPrice - (totalPrice * dis) / 100;
    }
    var msg = `Total Quantity is ${totalQuantity} & Total Price is Rs. ${totalPrice} you will get ${dis}% discount & Total Price after Discount is Rs. ${disPrice}/-`;
    alert(msg);
}

display();


