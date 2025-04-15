let gender = document.getElementById("Gender")
let governorate = document.getElementById("Governorate").firstElementChild.remove();
if (gender != null)
    gender.firstElementChild.remove();
if (governorate != null)
    governorate.firstElementChild.remove();
document.querySelectorAll(".accordion-button")[1].onclick = function () {
    map.invalidateSize();
    userMarker.openPopup();
}

let pickLocationButton = document.getElementById("doctor-register-pick-location");
let lngInput = document.getElementById("Lng");
let latInput = document.getElementById("Lat");

const map = L.map('map').setView([latInput.value, lngInput.value], 10);
let userMarker = L.marker([latInput.value, lngInput.value])
    .addTo(map)
    .bindPopup("Doctor's Clinic");
L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: '&copy; OpenStreetMap contributors'
}).addTo(map);

map.on('click', function (e) {
    lngInput.value = e.latlng.lng;
    latInput.value = e.latlng.lat;

    userMarker.setLatLng([latInput.value, lngInput.value]).openPopup();
});

pickLocationButton.onclick = function () {
    navigator.geolocation.getCurrentPosition((position) => {
        lngInput.value = position.coords.longitude;
        latInput.value = position.coords.latitude;

        console.log(position.coords);

        userMarker.setLatLng([latInput.value, lngInput.value]).openPopup();

        map.setView([latInput.value, lngInput.value], 18);
    },
        (error) => {
            Swal.fire({
                title: "Error",
                text: "An error has occurred, please try again",
                color: "#004085",
                confirmButtonColor: "#004085",
                icon: "error"
            });
        });
}
