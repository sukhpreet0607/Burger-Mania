const form = document.getElementById('loginForm');

form.onsubmit = async (evt) => {
    evt.preventDefault();

    localStorage.clear();

    var isFormValid = true;
    const username = form.username.value;
    const password = form.password.value;

    // console.log(username);
    // console.log(password);

    if (username.length < 5) {
        isFormValid = false;
        alert('Please enter a valid username');
    }

    if (password.length < 5) {
        isFormValid = false;
        alert('Please enter a valid password');
    }


    if (isFormValid) {
        try {
            var response = await fetch(`https://localhost:7219/api/User/GetUser/${username}/${password}`,
                {
                    method: 'GET',
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }
            );

            if (!response.ok) {
                alert('User not found');
            }
            else {
                const userData = await response.json();
                console.log(userData);
                localStorage.setItem("userId", `${userData.userId}`);
                alert('You are successfully login to Burger Mania');
                window.location.href = '../HTML/home.html';
            }
        }
        catch (error) {
            throw error;
        }

    }

};