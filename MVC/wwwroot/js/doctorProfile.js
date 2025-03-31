// initialize Animate on Scroll library
AOS.init();

// Paganation Code
let currentPage = Number(document.querySelector(".SP-active").innerText) || -1;
let nextBTN = document.querySelector(".SP-nextBTN");
let prevBTN = document.querySelector(".SP-prevBTN");



if (currentPage == 1) prevBTN.classList.add("disabled")


nextBTN.addEventListener("click", function () {
    window.scrollTo({ top: 0, behavior: "smooth" });
})

prevBTN.addEventListener("click", function () {
    window.scrollTo({ top: 0, behavior: "smooth" });
})

function GoToPage() {
    window.scrollTo({ top: 0, behavior: "smooth" });
}

document.getElementById("details-tab").addEventListener("click", function () {
    updateQueryString("reviews", "false");
});

document.getElementById("reviews-tab").addEventListener("click", function () {
    updateQueryString("reviews", "true");
});

function updateQueryString(key, value) {
    const url = new URL(window.location);
    url.searchParams.set(key, value); // Update or add the query parameter
    history.pushState(null, "", url); // Update the URL without reloading the page
}


// Initialize Swiper
var swiper = new Swiper(".mySwiper", {
    slidesPerView: 3,
    spaceBetween: 1,
    scrollbar: { el: ".swiper-scrollbar", draggable: true }
});


//Using OpenStreetMap 
var latitude = parseFloat(document.getElementById("map").getAttribute("data-latitude"));
var longitude = parseFloat(document.getElementById("map").getAttribute("data-longitude"));

if (!isNaN(latitude) && !isNaN(longitude)) {
    var map = L.map('map').setView([latitude, longitude], 50);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; OpenStreetMap contributors'
    }).addTo(map);

    L.marker([latitude, longitude]).addTo(map)
        .bindPopup("Doctor's Clinc")
        .openPopup();
} else {
    console.error("Invalid coordinates");
}