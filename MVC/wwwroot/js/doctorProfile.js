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
    updateQueryString("tab", "details");
});

document.getElementById("reviews-tab").addEventListener("click", function () {
    updateQueryString("tab", "reviews");
});

document.getElementById("calender-tab").addEventListener("click", function () {
    updateQueryString("tab", "calender");
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


// Image Code

let imageForm = document.querySelector("#imageForm")
let imgFile = document.querySelector(".SP-file");

imageForm.addEventListener("click", function () {
    imgFile.click()
})

imgFile.addEventListener("change", function () {
    imageForm.submit();
})



// Calender Code

let currentDate = new Date();
let currentYear = currentDate.getFullYear();
let currentMonth = currentDate.getMonth();
let nextMonth = new Date(currentYear, currentMonth + 1, 1);

const calendar = new calendarJs("SP-Calendar",{ // Target element
    initialDateTime: currentDate,

    // Restrict year range to current and next year (if next month is in next year)
    minimumYear: currentYear,
    maximumYear: nextMonth.getFullYear(),

    // Allow event creation
    manualEditingEnabled: false,

    // View configuration
    views: {
        fullMonth: {
            enabled: true,              // Enable full-month view
            isPinUpViewEnabled: false,  // Disable pin-up images
            showExtraTitleBarButtons: false, // Hide extra buttons
            showPreviousNextMonthNames: false // Hide month names in navigation
        },
        fullDay: { enabled: false },
        fullWeek: { enabled: false },
        fullYear: { enabled: false },
        timeline: { enabled: false },
        allEvents: { enabled: false }
    },

    // Disable features not needed
    fullScreenModeEnabled: false,
    exportEventsEnabled: false,
    importEventsEnabled: false,
    configurationDialogEnabled: false,
    jumpToDateEnabled: false,
    dragAndDropForEventsEnabled: false,
    
    sideMenu: {
        showDays: false,
        showEventTypes: false,
        showGroups: false,
        showWorkingDays: false,
        showWeekendDays: false
    },
    events: {
        onPreviousMonth: function (displayDate) {
            calendar.setCurrentDisplayDate(new Date(currentYear, currentMonth, 1));

        },
        onNextMonth: function (displayDate) {
            calendar.setCurrentDisplayDate(new Date(currentYear, currentMonth + 1, 1));
        },
        onDayClick: function (date, event) {
            var dateString = date.toDateString();
            // Log the click
            console.log("Clicked day:", dateString);

        },
        onBeforeEventAddEdit(event) {
            
            console.log(event)


        },
        onEventClick: function (event) {
            console.log(event)
        }


    }
});

let events = []

for (let i = 1; i <= 30; i++) {
    let date = new Date();
    date.setDate(date.getDate() + i)
    date = new Date(date.getFullYear(), date.getMonth(), date.getDate(),0,0,0,0);

    let event = {
        from: date,
        to: date,
        title: "Add Work +",
        color : "#004085",
        colorBorder: "#B3E5FC",
        description:"Vacation Day",
        status: "Done",
        isAllDay: true
    }
    calendar.addEvent(event);
}

calendar.addEvent(cal);

