const Toast = Swal.mixin({
    toast: true,
    position: "top-end",
    showConfirmButton: false,
    timer: 3000,
    timerProgressBar: true,
    didOpen: (toast) => {
        toast.onmouseenter = Swal.stopTimer;
        toast.onmouseleave = Swal.resumeTimer;
    }
});

if (success === "True")
    Toast.fire({
        icon: "success",
        title: "Password Changed Successfully!"
    });
else if (success == "False")
    Toast.fire({
        icon: "error",
        title: "Invalid Old Password!"
    });