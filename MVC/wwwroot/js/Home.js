const swiper = new Swiper(".swiper", {
    effect: "fade",
    fadeEffect: {
        crossFade: true,
    },
    loop: true,
    autoplay: {
        delay: 3000,
        disableOnInteraction: false,
    },
    speed: 1000,
    pagination: {
        el: ".swiper-pagination",
        clickable: true,
    },
    scrollbar: {
        el: ".swiper-scrollbar",
    },
});


var swiper2 = new Swiper(".swiper-container", {
     //loop: true,
    navigation: {
        nextEl: ".doctor-button-next",
        prevEl: ".doctor-button-prev",
    },
    watchOverflow: true,
    autoplay: false,
    spaceBetween: 15,
    breakpoints: {
        320: { slidesPerView: 2 }, // Small screens (≤ 767px)
        768: { slidesPerView: 3 }, // Medium screens (768px - 1023px)
        1024: { slidesPerView: 3 }, // Large screens (≥ 1024px)
    },
});

var swiper3 = new Swiper(".swiper-container-btns", {
    // loop: true,
    navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev",
    },
    spaceBetween: 5,
    breakpoints: {
        100: { slidesPerView: 2 }, // Small screens (≤ 767px)
        300: { slidesPerView: 3 }, // Medium screens (768px - 1023px)
        500: { slidesPerView: 4 }, // Medium screens (768px - 1023px)
        1024: { slidesPerView: 10 }, // Large screens (≥ 1024px)
    },
});

// Initialize MixItUp (Filtering)
var mixer = mixitup("#mixContainer", {
    animation: {
        duration: 300,
    },
    callbacks: {
        onMixEnd: function () {
            console.log("MixItUp filter completed");
            swiper2.update();
        }
    }
});

window.addEventListener("scroll", function () {
    console.log("===============================");

    console.log(window.pageYOffset + "px");

    let lenght =
        document.getElementsByTagName("header")[0].scrollHeight -
        window.pageYOffset -
        document.getElementsByTagName("nav")[0].offsetHeight;
    console.log(lenght, "px");

    if (lenght < 0) {
        document.getElementsByTagName("nav")[0].classList.replace("py-2", "py-3");
        document
            .getElementsByTagName("nav")[0]
            .classList.replace("bg-primary-color-75", "bg-primary-color");
    } else {
        document.getElementsByTagName("nav")[0].classList.replace("py-3", "py-2");
        document
            .getElementsByTagName("nav")[0]
            .classList.replace("bg-primary-color", "bg-primary-color-75");
    }

    console.log("===============================");
});

var typed = new Typed("#words", {
    strings: [
        "Find the best doctors, book appointments instantly!",
        "Quality healthcare, just a click away!",
        "Your health, our priority—trusted care at your fingertips.",
    ],
    typeSpeed: 30,
    backSpeed: 25,
    fadeOut: true,
    loop: true,
});



AOS.init();




