"use strict";
let inputs = document.querySelectorAll("input");
let summaryDiv = document.getElementById("validation-summary");
let submitButton = document.getElementById("login-submit");
inputs.forEach(element => {
    element.oninput = () => {
        if (summaryDiv != null)
            summaryDiv.style.display = "none";
    };
});
if (submitButton != null)
    submitButton.onclick = () => {
        if (summaryDiv != null)
            summaryDiv.style.display = "none";
    };
