paypal.Buttons({
    createOrder: function (data, actions) {
        return fetch(@Url.Action("Order"), {
            method: 'post'
        }).then(function (response) {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        }).then(function (orderData) {
            return orderData.id;
        });
    },
    onApprove: function (data, actions) {
        return fetch(`${@Url.Action("Capture")}?orderId=${data.orderId}`, {
            method: 'post'
        }).then(function (response) {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        }).render('#paypal-button-container');
    }
})