// Employee form validation
const fnameInput = document.getElementById("fNameInput");
const lnameInput = document.getElementById("lNameInput");
const employeeEmailInput = document.getElementById("emailInput");
const employeeNumberInput = document.getElementById("numInput");
const employeeSubmitButton = document.getElementById("employeeSubmitBtn");
const employeeErrorMessageContainer = document.getElementById("employeeErrorMessage");
const ogBorderColour = fnameInput.style.borderColor;

let validFname;
let validLname;
let validEmployeeEmail;
let validEmployeeNumber;

//disables form from posting if inputs are invalid
function ValidateEmployeeForm(event) {
    validFname = fnameInput.value.length > 0 && fnameInput.value != " ";
    validLname = lnameInput.value.length > 0 && lnameInput.value != " ";
    validEmployeeEmail = emailRegex.test(employeeEmailInput.value) || employeeEmailInput.value == "";
    validEmployeeNumber = phoneRegex.test(employeeNumberInput.value) || employeeNumberInput.value == "";

    SetEmployeeInputsValid();
    if (!validFname || !validLname || !validEmployeeEmail || !validEmployeeNumber) {
        event.preventDefault();
        SetEmployeeInputsInvalid();
    }
}

//sets invalid fields border to error colour and adds error message
function SetEmployeeInputsInvalid() {
    if (!validFname) {
        fnameInput.style.borderColor = borderErrorColor;
        employeeErrorMessageContainer.innerText += "Please enter your first name\n";
    }
    if (!validLname) {
        lnameInput.style.borderColor = borderErrorColor;
        employeeErrorMessageContainer.innerText += "Please enter your last name\n";
    }
    if (!validEmployeeEmail) {
        employeeEmailInput.style.borderColor = borderErrorColor;
        employeeErrorMessageContainer.innerText += "Please enter a valid email address\n";
    }
    if (!validEmployeeNumber) {
        employeeNumberInput.style.borderColor = borderErrorColor;
        employeeErrorMessageContainer.innerText += "Please enter a valid phone number\n";
    }
}

//removes errors on fields
function SetEmployeeInputsValid() {
    fnameInput.style.borderColor = ogBorderColour;
    lnameInput.style.borderColor = ogBorderColour;
    employeeEmailInput.style.borderColor = ogBorderColour;
    employeeNumberInput.style.borderColor = ogBorderColour;
    employeeErrorMessageContainer.innerText = "";
}

employeeSubmitButton.addEventListener("click", ValidateEmployeeForm);