paypal.Buttons({
    async createOrder() {
        //const value = document.getElementById("amount").value;
        const value = document.getElementById("reservationId").value;
        const response = await fetch("/Payment/CreateOrder", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                reservationId: value,
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
                    <strong>Transaction Completed!</strong> Thank you for your payment.
                    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                </div>
            `;
            const reservationId = document.getElementById("reservationId").value;
            const patientId = $c.displayCookie("currentId");
            // Redirect to success page
            window.location.href = `/patient/add-appointment?reservationId=${reservationId}&patientId=${patientId}`;
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
        console.error(err);
        document.getElementById("notification-container").innerHTML = `
            <div class="alert alert-danger alert-dismissible fade show" role="alert">
                <strong>An Error Occured! Please retry later.</strong> ${err.message}
                <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </div>
        `;
    },
    onCancel(data) {
        alert('Transaction was cancelled.');
        document.getElementById("notification-container").innerHTML = `
            <div class="alert alert-warning alert-dismissible fade show" role="alert">
                <strong>Transaction Cancelled!</strong> Please retry later.
                <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </div>
        `;
    }
}).render('#paypal-button-container');
