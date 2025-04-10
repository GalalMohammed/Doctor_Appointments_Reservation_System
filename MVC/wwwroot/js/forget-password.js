


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


 //emailExists

if (emailExists =="NotExist") {
    Toast.fire({
        icon: "success",
        title: "The Email Exists!"
    });
} else if (emailExists == "Exist") {
    Toast.fire({
        icon: "error",
        title: "The Email Doesn't Exist!"
    });
}
