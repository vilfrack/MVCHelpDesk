
//CUANDO CERREMOS EL MODAL
$("button[data-dismiss='modal']").click(function () {
    $('#login').val('');
    $('#password').val('');
    $('#Nombre').val('');
    $('#Apellido').val('');
    $('#Email').val('');
    $('#file').val('');
    $('#alert_success').hide();
    $(".fileinput-remove-button").click();
    $("#btnEnviar").prop('disabled', false);
    $('#alert_danger').hide();
    $("#btnEliminar").prop('disabled', false);
    $("#btnEnviarEditar").prop('disabled', false);
    esconderMensajes();
});


//SE SACAN DEL REEADY PORQUE LUEGO SE EJECUTAN DOS VECES
$("#formEdit").submit(function (e) {
    e.preventDefault();
    var parametros = new FormData($(this)[0]);
    $.ajax({
        type: "POST",
        url: "/Departamento/Edit",
        cache: false,
        contentType: false, //importante enviar este parametro en false
        processData: false, //importante enviar este parametro en false
        //CONVIERTO EL OBJETO EN FORMATO JSON
        // data: JSON.stringify(list),
        data: parametros,

        dataType: "json",
        success: function (data) {

            if (data.success) {
                //REESTABLECE LOS ESTILOS PREDEFINIDOS DE LOS LABEL ERRORES Y DIV, EN CASO DE QUE SE HAYAN MOSTRADO Y SE HAYAN CORREGIDO
                $.each(parametros, function (key, value) {
                    $("#ErrorEdit_" + key).html('');
                    $("#divEdit_" + key).removeClass(" has-error has-feedback");
                });
                $("#btnEnviar").prop('disabled', true);
                $('#alert_success').show("fast");
                $('#alert_success').show("fast");
                $('#alert_danger').hide();
                LoadGridRequerimientos();

            }
            else {
                //VA A CAPTURAR TODOS LOS ERRORES ENVIADOS DEL CONTROLADOR
                $.each(data.Errors, function (key, value) {
                    //VALUE VA A TRAER SOLO AQUELLOS VALORES QUE NO CUMPLAN CON LOS REQUISITOS ESTABLECIDOS EN EL MODELSTATE
                    //CREAR EN LO POSIBLE UNA CLASE QUE GUARDE ESTE CODIGO
                    if (value != "true") {
                        $("#ErrorEdit_" + key).html(value[value.length - 1].ErrorMessage);
                        $("#divEdit_" + key).addClass(" has-error has-feedback");
                    } else {
                        $("#ErrorEdit_" + key).html('');
                        $("#divEdit_" + key).removeClass(" has-error has-feedback");
                    }

                });
            }
        },
        error: function (data) {
            $('#alert_danger').html(data);
            $('#alert_danger').show("fast");
        },

    });
});
$("#formCreate").submit(function (e) {
    e.preventDefault();
    //ruta la cual recibira nuestro archivo
    var parametros = new FormData($(this)[0]);
    //CREO EL AJAX

    $.ajax({
        type: "POST",
        url: "/Departamento/Create",
        cache: false,
        contentType: false, //importante enviar este parametro en false
        processData: false, //importante enviar este parametro en false
        //CONVIERTO EL OBJETO EN FORMATO JSON
        // data: JSON.stringify(list),
        data: parametros,

        dataType: "json",
        success: function (data) {
            if (data.success) {
                //REESTABLECE LOS ESTILOS PREDEFINIDOS DE LOS LABEL ERRORES Y DIV, EN CASO DE QUE SE HAYAN MOSTRADO Y SE HAYAN CORREGIDO
                $.each(parametros, function (key, value) {
                    $("#Error_" + key).html('');
                    $("#div_" + key).removeClass(" has-error has-feedback");
                });
                $('#alert_success').show("fast");
                $("#btnEnviar").prop('disabled', true);
                $('#alert_danger').hide();
                LoadGridRequerimientos();
            }
            else {

                //VA A CAPTURAR TODOS LOS ERRORES ENVIADOS DEL CONTROLADOR
                $.each(data.Errors, function (key, value) {
                    //VALUE VA A TRAER SOLO AQUELLOS VALORES QUE NO CUMPLAN CON LOS REQUISITOS ESTABLECIDOS EN EL MODELSTATE
                    //CREAR EN LO POSIBLE UNA CLASE QUE GUARDE ESTE CODIGO
                    if (value != "true") {
                        $("#Error_" + key).html(value[value.length - 1].ErrorMessage);
                        $("#div_" + key).addClass(" has-error has-feedback");
                    } else {
                        $("#Error_" + key).html('');
                        $("#div_" + key).removeClass(" has-error has-feedback");
                    }

                });
            }

        },
        error: function (data) {
            $('#alert_danger').html('error');
            $('#alert_danger').show("fast");
        },

    });
});
//CUANDO SE DE CLICK A EDITAR DESDE LA TABLA
$("#tableRequerimiento").on('click', 'tr #asignar', function () {
    var id = $(this).parents("tr").find("td").eq(0).html();
    var url = "/Asignar/asignar?id=" + id + ""; // Establecer URL de la acción
    $("#btnEnviarEditar").prop('disabled', false);
    $("#contenedor-editar").load(url);

});

//FUNCIONES
function LoadGridRequerimientos() {
    $('#tableRequerimiento').dataTable({
        destroy: true,//PERMITE DESTRUIR LA TABLA PARA VOLVERLA A CREAR
        bProcessing: true,
        sAjaxSource: '/Asignar/get',
        "columns": [//DEBE ESTAR EN EL MISMO ORDEN PORQUE SI NO DA ERROR
          { "data": "TaskID" },
          { "data": "Titulo" },
          { "data": "Descripcion" },
          { "data": "Solicitante" },
          {
              "data": "Fecha",
              "render": function (jsonDate) {
                  var date = new Date(parseInt(jsonDate.substr(6)));
                  var month = date.getMonth() + 1;
                  return date.getDate() + "/" + month + "/" + date.getFullYear();
                  //https://www.youtube.com/watch?v=TgD24a9gxXw   explicacion de como cambiar el formato fecha
              }
          },

          {
              "data": null,
              defaultContent: "<button id='asignar' class='btn btn-success btn-sm'" +
                              "data-toggle='modal' data-target='#editModal' ><span class='glyphicon glyphicon-retweet'></span> Asignar </button>"
          }
        ]
    })
}
function esconderMensajes() {
    $('#alert_danger').hide();
    $('#alert_success').hide();
    $('#alert_success_eliminar').hide();
    $('#alert_danger_eliminar').hide();
}
