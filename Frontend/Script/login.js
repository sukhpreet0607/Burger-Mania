const form = document.getElementById('loginForm');

form.onsubmit = (evt) => {
    evt.preventDefault();
    var isFormValid = true;

    var mobNumber = form.mobNumber;
    // console.log(mobNumber);
    
    if (!mobNumber.checkValidity()) {
        if (mobNumber.innerText.length != 10) {
            isFormValid = false;
            alert('Please enter valid a 10 digit number');
        }
    }

    if (isFormValid) {
        alert('You are successfully login to Burger Mania');
        window.location.href = '../HTML/home.html';
    }

};