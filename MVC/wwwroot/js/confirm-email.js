"use strict";
const duration = 3;
let counter = duration;
let durationSpan = document.getElementById("confirm-duration");
let durationBar = document.getElementById("confirm-bar-movable");
let redirectLink = document.getElementById("confirm-redirect");
if (durationSpan != null)
    durationSpan.innerText = counter.toString();
if (durationBar != null)
    durationBar.style.width = `${counter / duration * 100}%`;
let handler = setInterval(() => {
    if (counter > 0) {
        counter--;
        if (durationSpan != null)
            durationSpan.innerText = counter.toString();
        if (durationBar != null)
            durationBar.style.width = `${counter / duration * 100}%`;
    }
    else
        redirectLink === null || redirectLink === void 0 ? void 0 : redirectLink.click();
    console.log(durationBar);
}, 1000);
