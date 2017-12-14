$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "/DashBoard/getCantidadTask",
        dataType: "json",
        success: function (data) {
            getCantidadTask();
        },
            error: function (data) {
        },
    });
});

function getCantidadTask() {
    //DivCantidadTask
    var url = "/DashBoard/CantidadTask"; // Establecer URL de la acción
    $("#DivCantidadTask").load(url);
}