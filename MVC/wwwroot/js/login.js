"use strict";
let inputs = document.querySelectorAll("input");
let summaryDiv = document.getElementById("validation-summary");
inputs.forEach(element => {
    element.oninput = () => {
        if (summaryDiv != null)
            summaryDiv.style.display = "none";
    };
});
