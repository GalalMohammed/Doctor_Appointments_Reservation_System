"use strict";
const Toast = Swal.mixin({
    toast: true,
    position: "top-end",
    showConfirmButton: false,
    timer: 3000,
    timerProgressBar: true,
    didOpen: (toast) => {
        toast.onmouseenter = Swal.stopTimer;
        toast.onmouseleave = Swal.resumeTimer;
    }
});

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

if (emailNotConfirmed === "True")
    Toast.fire({
        icon: "error",
        title: "Email Is Not Confirmed!"
    });