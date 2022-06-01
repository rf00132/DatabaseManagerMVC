// Company form validation
const companyNameInput = document.getElementById("companyNameAdd");
const companyWebsiteInput = document.getElementById("companyWebsiteInput");
const companyEmailInput = document.getElementById("companyEmailInput");
const companySubmitButton = document.getElementById("companySubmitBtn");
const companyErrorMessageContainer = document.getElementById("companyErrorMessage");
const ogBorderColour = companyNameInput.style.borderColor;

let validCompanyName;
let validCompanyEmail;
let validCompanyWebsite;

//disables form from posting if inputs are invalid
function ValidateCompanyForm(event) {
    validCompanyName = companyNameInput.value.length > 0 && companyNameInput.value != " ";
    validCompanyEmail = emailRegex.test(companyEmailInput.value) || companyEmailInput.value == "";
    validCompanyWebsite = websiteRegex.test(companyWebsiteInput.value) || companyWebsiteInput.value == "";
    SetCompanyInputsValid();
    if (!validCompanyName || !validCompanyEmail || !validCompanyWebsite ) {
        event.preventDefault();
        SetCompanyInputsInvalid();
    }
}

//sets invalid fields border to error colour and adds error message
function SetCompanyInputsInvalid() {
    if (!validCompanyName) {
        companyNameInput.style.borderColor = borderErrorColor;
        companyErrorMessageContainer.innerText += "Please enter the company name\n";
    }
    if (!validCompanyEmail) {
        companyEmailInput.style.borderColor = borderErrorColor;
        companyErrorMessageContainer.innerText += "Please enter a valid email address\n";
    }
    if (!validCompanyWebsite) {
        companyWebsiteInput.style.borderColor = borderErrorColor;
        companyErrorMessageContainer.innerText += "Please enter a valid website\n";
    }
}

//removes errors on fields
function SetCompanyInputsValid() {
    companyNameInput.style.borderColor = ogBorderColour;
    companyEmailInput.style.borderColor = ogBorderColour;
    companyEmailInput.style.borderColor = ogBorderColour;
    companyErrorMessageContainer.innerText = "";
}

companySubmitButton.addEventListener("click", ValidateCompanyForm);