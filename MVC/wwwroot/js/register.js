"use strict";
let nextButton = document.getElementById("register-next");
let backButton = document.getElementById("register-back");
let step1 = document.getElementById("register-step-1");
let step2 = document.getElementById("register-step-2");
window.onload = () => {
    if (step1 != null) {
        step1.classList.add("d-flex");
        step1.classList.remove("d-none");
    }
    if (step2 != null) {
        step2.classList.add("d-none");
        step2.classList.remove("d-flex");
    }
};
if (nextButton != null)
    nextButton.onclick = () => {
        if (step1 != null)
            step1.style.opacity = "0";
        if (step2 != null)
            step2.style.opacity = "1";
        setTimeout(() => {
            if (step1 != null) {
                step1.classList.add("d-none");
                step1.classList.remove("d-flex");
            }
            if (step2 != null) {
                step2.classList.add("d-flex");
                step2.classList.remove("d-none");
            }
        }, 200);
    };
if (backButton != null)
    backButton.onclick = () => {
        if (step1 != null)
            step1.style.opacity = "1";
        if (step2 != null)
            step2.style.opacity = "0";
        setTimeout(() => {
            if (step1 != null) {
                step1.classList.add("d-flex");
                step1.classList.remove("d-none");
            }
            if (step2 != null) {
                step2.classList.add("d-none");
                step2.classList.remove("d-flex");
            }
        }, 200);
    };
