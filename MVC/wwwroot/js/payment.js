﻿paypal.Buttons({
    async createOrder() {
        const response = await fetch("/Payment/CreateOrder", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                reservationId: ResId,
            })
        });

        if (!response.ok) {
            throw new Error('Network response was not ok');
        }

        const order = await response.json();

        return order.id;
    },
    async onApprove(data) {
        const response = await fetch("/Payment/CaptureOrder", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                orderID: data.orderID
            })
        })

        if (!response.ok) {
            throw new Error('Network response was not ok');
        }

        const details = await response.json();
        // Show a success message to your buyer
        if (details.status === "COMPLETED") {
            document.getElementById("notification-container").innerHTML = `
                <div class="alert alert-success alert-dismissible fade show" role="alert">
                    <strong>Transaction Completed!</strong> Thank you for your payment. You will be redirected shortly.
                    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                </div>
            `;
            // Redirect to success page
            setTimeout(() => {
                window.location.href = `/patient/add-appointment?doctorReservationId=${ResId}&patientId=${details.patientId}`;
            }, 2000);
        } else {
            document.getElementById("notification-container").innerHTML = `
                <div class="alert alert-danger alert-dismissible fade show" role="alert">
                    <strong>Transaction Failed!</strong> ${details}
                    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                </div>
            `;
        }
        return details;
    },
    onError(err) {
        //console.error(err);
        if (err.message === "Expected an order id to be passed") {
            Swal.fire({
                title: "Info",
                text: "You have already reserved in this slot!",
                color: "#004085",
                confirmButtonColor: "#004085",
                icon: "info"
            }).then(() => document.querySelector('#exampleModal button').click());
        }
        else
            document.getElementById("notification-container").innerHTML = `
                <div class="alert alert-danger alert-dismissible fade show" role="alert">
                    <strong>An Error Occurred! Please retry later.</strong> ${err.message}
                    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                </div>
            `;
    },
    onCancel(data) {
        //alert('Transaction was cancelled.');
        document.getElementById("notification-container").innerHTML = `
            <div class="alert alert-warning alert-dismissible fade show" role="alert">
                <strong>Transaction Canceled!</strong> Please retry later.
                <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </div>
        `;
    }
}).render('#paypal-button-container');
