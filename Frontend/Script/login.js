const form = document.getElementById('loginForm');

form.onsubmit = (evt) => {
    evt.preventDefault();

    var isFormValid = true;
    var username = form.username;
    var password = form.password;
    
    if (!username.checkValidity()) {
        if (username.innerText.length<8) {
            isFormValid = false;
            alert('Please enter a valid username');
        }
    }

    if (!password.checkValidity()) {
        if (password.innerText.length<8) {
            isFormValid = false;
            alert('Please enter correct password');
        }
    }

    if (isFormValid) {
        alert('You are successfully login to Burger Mania');
        window.location.href = '../HTML/home.html';
    }

};