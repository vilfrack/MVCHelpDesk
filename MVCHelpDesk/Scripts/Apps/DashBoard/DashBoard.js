function CantidadTask() {
    var ctx = document.getElementById("CantidadTask").getContext('2d');
    var myBarChart = new Chart(ctx, {
        type: 'bar',
        //type: 'horizontalBar',
        data: {
            labels: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"],
            datasets: [{
                label: "Rechazado",
                backgroundColor: "rgba(255, 99, 132, 0.2)",
                data: [3, 7, 4, 5, 8, 9, 2, 10, 9, 4, 5, 4],
                borderColor: 'rgba(255,99,132,1)',
                borderWidth: 1
            }, {
                label: "Asignados",
                backgroundColor: "rgba(54, 162, 235, 0.2)",
                data: [3, 7, 4, 5, 8, 9, 2, 10, 9, 4, 5, 4],
                borderColor: 'rgba(54, 162, 235, 1)',
                borderWidth: 1
            }, {
                label: "En desarrollo",
                backgroundColor: "rgba(255, 206, 86, 0.2)",
                data: [3, 7, 4, 5, 8, 9, 2, 10, 9, 4, 5, 4],
                borderColor: 'rgba(255, 206, 86, 1)',
                borderWidth: 1
            }],

        },
    });
}
