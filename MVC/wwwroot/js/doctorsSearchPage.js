// Initialize Years of Experience Slider
var YOESlider = document.querySelector('.SP-YOESlider');
let minYearInput = document.querySelector(".SP-filters .minYear");
let minPriceInput = document.querySelector(".SP-filters .minPrice");
let maxPriceInput = document.querySelector(".SP-filters .maxPrice");
var priceSlider = document.querySelector('.SP-priceSlider');

noUiSlider.create(YOESlider, {
    start: [minYearInput.value ?? 0],
    step: 1,
    connect: "lower",
    range: { min: 0, max: 10 },
    format: wNumb({ decimals: 0, prefix: "" }),
    pips: {
        mode: 'values',
        values: [...Array(11).keys()], // [0, 1, 2, ..., 10]
        density: 2
    }
});


noUiSlider.create(priceSlider, {
    start: [minPriceInput.value ?? 50,maxPriceInput.value ?? 10000],
    step: 1,
    connect: true,
    range: { min: 50, max: 10000 },
    format: wNumb({
        decimals: 0,
        thousand: ',',
        encoder: value => Math.round(value * 100) / 100
    }),
    tooltips: [
        wNumb({ decimals: 0, postfix: "E£", thousand: ',' }),
        wNumb({ decimals: 0, postfix: "E£", thousand: ',' })
    ]
});

// Update Inputs on Slider Change

YOESlider.noUiSlider.on("update", values => {
    minYearInput.value = values;
});



priceSlider.noUiSlider.on("update", values => {
    minPriceInput.value = values[0].replace(/,/g, "");
    maxPriceInput.value = values[1].replace(/,/g, "");
});

// Initialize Swiper
var swiper = new Swiper(".mySwiper", {
    slidesPerView: 3,
    spaceBetween: 1,
    scrollbar: { el: ".swiper-scrollbar", draggable: true }
});

// Paganation Code
let currentPage = Number(document.querySelector(".SP-active").innerText) || -1;
let nextBTN = document.querySelector(".SP-nextBTN");
let prevBTN = document.querySelector(".SP-prevBTN");



if (currentPage == 1) prevBTN.classList.add("disabled")


nextBTN.addEventListener("click", function () {
    document.querySelector(".SP-pageNum").value = (currentPage)
    document.querySelector(".SP-filters input[type='submit']").click();
})

prevBTN.addEventListener("click", function () {
    document.querySelector(".SP-pageNum").value = (currentPage - 2)
    document.querySelector(".SP-filters input[type='submit']").click();
    
})

function GoToPage(pageNum) {
    document.querySelector(".SP-pageNum").value = (pageNum - 1)
    document.querySelector(".SP-filters input[type='submit']").click();
}


// Form Code
let resetBTN = document.querySelector(".SP-resetBTN");

resetBTN.addEventListener("click", function (event) {
    event.preventDefault(); // Prevent default reset

    priceSlider.noUiSlider.set([50,10000]);
    YOESlider.noUiSlider.set(0);
    document.querySelector("#All").checked = true;
    document.querySelector("#Male").checked = false;
    document.querySelector("#Female").checked = false;
    document.querySelector("#All").dispatchEvent(new Event("change", { bubbles: true }));

    document.querySelector(".SP-docSearch").value = "";
    document.querySelector(".SP-select").value = 0;
})

let filterBTN = document.querySelector(".SP-filterBTN")
filterBTN.addEventListener("click", function () {
    document.querySelector(".SP-pageNum").value = 0;
})

//// Appointment Data
//let appointments = [
//    { day: 0, app: "10:00 AM|02:00 PM" },
//    { day: 1, app: undefined },
//    { day: 2, app: "01:00 PM|05:00 PM" },
//    { day: 3, app: undefined },
//    { day: 4, app: "09:00 AM|12:00 PM" },
//    { day: 5, app: "03:00 PM|08:00 PM" },
//    { day: 6, app: undefined }
//];

//// Sort Appointments Starting from Today
//function sortAppointmentsByToday(appointments) {
//    let todayIndex = new Date().getDay();
//    return appointments.slice(todayIndex).concat(appointments.slice(0, todayIndex));
//}

//// Get Next Date for Appointment Display
//function getNextDate(dayNumber) {
//    let today = new Date();
//    let todayNumber = today.getDay();
//    let daysToAdd = (dayNumber - todayNumber + 7) % 7 || 7;

//    let nextDate = new Date();
//    nextDate.setDate(today.getDate() + daysToAdd);

//    return nextDate.toLocaleString("en-US", { weekday: "short", day: "numeric", month: "numeric" });
//}

//// Render Appointments
//let sortedAppointments = sortAppointmentsByToday(appointments);
//let swiperContainer = document.querySelector(".swiper-wrapper");
//swiperContainer.innerHTML = "";

//for (let app of sortedAppointments) {
//    let [from, to] = app.app ? app.app.split("|") : [undefined, undefined];
//    let date = new Date();

//    let card = document.createElement("div");
//    card.className = "card text-center swiper-slide";

//    card.innerHTML = `
//      <div class="card-header SP-bookHeader p-1">
//         ${app.day === date.getDay() ? "Today" : app.day === date.getDay() + 1 ? "Tomorrow" : getNextDate(app.day)}
//      </div>
//      <div class="card-body SP-primaryText d-flex flex-column justify-content-center p-2">
//         ${from && to
//            ? `<p class="mb-1">From <strong>${from}</strong></p>
//               <p class="mb-1">To <strong>${to}</strong></p>`
//            : `<p class="mb-1">No Available</p>
//               <p class="mb-1">Appointments</p>`}
//      </div>
//      ${app.app !== undefined
//            ? `<a href="#" class="card-footer btn SP-bookBTN p-1">Book</a>`
//            : `<div class="card-footer btn SP-bookBTN disabled p-1">Book</div>`}
//   `;

//    swiperContainer.appendChild(card);
//}

