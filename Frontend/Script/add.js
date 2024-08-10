const URL = "https://localhost:7219/api/BurgerCart";

// if (!localStorage.getItem('myCart')) {
//     const myCart = [];
//     localStorage.setItem('myCart', JSON.stringify(myCart));
// }


async function addToCart(divId) {
    var div = document.getElementById(divId);
    var selectElement = div.querySelector('select');
    var span = div.querySelector('span');
    var burger = span.innerText;
    var category = selectElement.options[selectElement.selectedIndex].value;
    // console.log(category);
    // console.log(burger);
    // const myCart = JSON.parse(localStorage.getItem('myCart'));

    try {
        const response = await fetch(URL);
        if (!response.ok) {
            throw new Error(`Response status: ${response.status}`);
        }
        const myCart = await response.json();
        console.log(myCart);

        for (let item in myCart) {
            if (myCart[item].burger == burger && myCart[item].category == category) {
                const updatedItem = {
                    "itemId": myCart[item].itemId,
                    "burger": myCart[item].burger,
                    "category": myCart[item].category,
                    "price": myCart[item].price,
                    "quantity": myCart[item].quantity + 1,
                    "totalPrice": myCart[item].totalPrice
                };
                const response = await fetch(URL + `/${burger}/${category}`, {
                    method: 'PUT',
                    headers: {
                        'Content-Type': 'application/json' 
                    },
                    body: JSON.stringify(updatedItem)
                })

                return response;
            }
        }

        const newItem = {
            "burger": burger,
            "category": category,
            "quantity": 1,
        };
        const res = await fetch(URL, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json' 
            },
            body: JSON.stringify(newItem)
        });

        return res;

    }
    catch (error) {
        console.log("Error encountered while accessing cart : ", error);
    }



    // localStorage.setItem('myCart', JSON.stringify(myCart));
    // console.log(localStorage.getItem('myCart'));
    // for (let item in myCart) {
    //     console.log(myCart[item].name + ' ' + myCart[item].category + ':' + myCart[item].totalPrice);
    // }

}


function Burger(name, category) {
    this.name = name;
    this.category = category;
    this.quantity = 1;
    this.price = 0;
    this.totalPrice = 0;

    if (this.category === 'Veg') {
        this.price = 100;
    }
    else if (this.category === 'Egg') {
        this.price = 150;
    }
    else {
        this.price = 200;
    }

    this.calculatePrice = () => {
        this.totalPrice = this.quantity * this.price;
    }

}


