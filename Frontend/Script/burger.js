
function Burger(name, category) {
    this.name = name;
    this.category = category;
    this.quantity = 1;
    var price = 0;

    this.showPrice = () => {
        return price;
    }

    this.calculatePrice = () => {
        if (this.category === 'Veg') {
            price = (this.quantity * 100);
        }
        else if (this.category === 'Egg') {
            price = (this.quantity * 150);
        }
        else {
            price = (this.quantity * 200);
        }
    }
}

module.exports = Burger;

























// var bur = new Burger('xyz', 'Veg');
// console.log(bur)
// bur.calculatePrice();
// console.log(bur.showPrice());
// bur.quantity += 1;
// console.log(bur)
// bur.calculatePrice();
// console.log(bur.showPrice());




