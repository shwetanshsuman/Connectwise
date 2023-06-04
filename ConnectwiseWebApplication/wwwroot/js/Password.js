
// Get the password input element
var passwordInput = document.getElementById("userPassword");

var form = document.getElementById("registrationForm");

// Get the password validation message element
var passwordValidationMessage = document.getElementById("passwordValidationMessage");

// Define the regular expression for password validation
var passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()])[a-zA-Z0-9!@#$%^&*()]{8,}$/;

// Function to handle password validation
function validatePassword() {
    var password = passwordInput.value;

    if (passwordRegex.test(password)) {
        passwordValidationMessage.innerText = "";
    } else {
        passwordValidationMessage.innerText = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character (!@#$%^&*()).";
    }
}

// Attach the event handler to the password input's "input" event
passwordInput.addEventListener("input", validatePassword);

function submitForm(event) {
    var password = passwordInput.value;

    if (!passwordRegex.test(password)) {
        event.preventDefault(); // Prevent form submission
        passwordValidationMessage.innerText = "Please enter a valid password.";
    }
}

// Attach the event handler to the form's "submit" event
form.addEventListener("submit", submitForm);
