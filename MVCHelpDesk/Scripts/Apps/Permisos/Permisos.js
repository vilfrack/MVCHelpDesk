$(function () {
    $('#h').change(function () {
        alert('ohh');
    });
});
//CUANDO CERREMOS EL MODAL
//$("button[data-dismiss='modal']").click(function () {
//    $('#login').val('');
//    $('#password').val('');
//    $('#Nombre').val('');
//    $('#Apellido').val('');
//    $('#Email').val('');
//    $('#file').val('');
//    $('#alert_success').hide();
//    $(".fileinput-remove-button").click();
//    $("#btnEnviar").prop('disabled', false);
//    $('#alert_danger').hide();
//    $("#btnEliminar").prop('disabled', false);
//    $("#btnEnviarEditar").prop('disabled', false);
//    esconderMensajes();
//});
//AL HACER CLICK EN EL BOTON ELIMINAR QUE ESTA EN LA TABLA, ESTE OBTIENE EL ID Y SE LO ENVIA AL HIDDEN QUE ESTA EN EL MODAL DE ELIMINAR
//$("#tablePermisos").on('click', 'tr #eliminar', function () {
//    var id = $(this).parents("tr").find("td").eq(0).html();
//    $('.mensaje').html('Seguro desea eliminar el registro seleccionados?');
//    document.getElementById("hdId").value = id;

//});
//ELIMINAR EL REGISTRO
//$("#btnEliminar").click(function () {
//    var params = {
//        id: $('#hdId').val()
//    };
//    $.ajax({
//        type: "POST",
//        url: "/Usuario/Delete",
//        data: params,
//        dataType: "json",
//        success: function (data) {
//            console.log(data);
//            if (data.success == true) {
//                $('.mensaje').html('');
//                $('#alert_success_eliminar').html('Registro eliminado');
//                $('#alert_success_eliminar').show("fast");
//                $("#btnEliminar").prop('disabled', true);
//                LoadGridPermisos();
//            }
//            else {
//                $('#alert_danger_eliminar').html(data.mensaje);
//                $('#alert_danger_eliminar').show("fast");
//            }
//        },
//        error: function (data) {
//        },
//    });
//});
//SE SACAN DEL REEADY PORQUE LUEGO SE EJECUTAN DOS VECES
//$("#formEdit").submit(function (e) {
//    e.preventDefault();
//    var parametros = new FormData($(this)[0]);
//    $.ajax({
//        type: "POST",
//        url: "/Usuario/Edit",
//        cache: false,
//        contentType: false, //importante enviar este parametro en false
//        processData: false, //importante enviar este parametro en false
//        //CONVIERTO EL OBJETO EN FORMATO JSON
//        // data: JSON.stringify(list),
//        data: parametros,

//        dataType: "json",
//        success: function (data) {

//            if (data.success) {
//                //REESTABLECE LOS ESTILOS PREDEFINIDOS DE LOS LABEL ERRORES Y DIV, EN CASO DE QUE SE HAYAN MOSTRADO Y SE HAYAN CORREGIDO
//                $.each(parametros, function (key, value) {
//                    $("#Error_" + key).html('');
//                    $("#div_" + key).removeClass(" has-error has-feedback");
//                });
//                $('#alert_success').show("fast");
//                $('#alert_success').show("fast");
//                $('#alert_danger').hide();
//                LoadGridPermisos();
//            }
//            else {
//                //VA A CAPTURAR TODOS LOS ERRORES ENVIADOS DEL CONTROLADOR
//                $.each(data.Errors, function (key, value) {
//                    //VALUE VA A TRAER SOLO AQUELLOS VALORES QUE NO CUMPLAN CON LOS REQUISITOS ESTABLECIDOS EN EL MODELSTATE
//                    //CREAR EN LO POSIBLE UNA CLASE QUE GUARDE ESTE CODIGO
//                    if (value != "true") {
//                        $("#Error_" + key).html(value[value.length - 1].ErrorMessage);
//                        $("#div_" + key).addClass(" has-error has-feedback");
//                    } else {
//                        $("#Error_" + key).html('');
//                        $("#div_" + key).removeClass(" has-error has-feedback");
//                    }
//                });
//            }
//        },
//        error: function (data) {
//            $('#alert_danger').html(data);
//            $('#alert_danger').show("fast");
//        },
//    });
//});
//$("#formCreate").submit(function (e) {
//    e.preventDefault();
//    //ruta la cual recibira nuestro archivo
//    var parametros = new FormData($(this)[0]);
//    //CREO EL AJAX

//    $.ajax({
//        type: "POST",
//        url: "/Usuario/Create",
//        cache: false,
//        contentType: false, //importante enviar este parametro en false
//        processData: false, //importante enviar este parametro en false
//        //CONVIERTO EL OBJETO EN FORMATO JSON
//        // data: JSON.stringify(list),
//        data: parametros,

//        dataType: "json",
//        success: function (data) {
//            if (data.success) {
//                //REESTABLECE LOS ESTILOS PREDEFINIDOS DE LOS LABEL ERRORES Y DIV, EN CASO DE QUE SE HAYAN MOSTRADO Y SE HAYAN CORREGIDO
//                $.each(parametros, function (key, value) {
//                    $("#Error_" + key).html('');
//                    $("#div_" + key).removeClass(" has-error has-feedback");
//                });
//                $('#alert_success').show("fast");
//                $("#btnEnviarEditar").prop('disabled', true);
//                $('#alert_danger').hide();
//                LoadGridPermisos();
//            }
//            else {

//                //VA A CAPTURAR TODOS LOS ERRORES ENVIADOS DEL CONTROLADOR
//                $.each(data.Errors, function (key, value) {
//                    //VALUE VA A TRAER SOLO AQUELLOS VALORES QUE NO CUMPLAN CON LOS REQUISITOS ESTABLECIDOS EN EL MODELSTATE
//                    //CREAR EN LO POSIBLE UNA CLASE QUE GUARDE ESTE CODIGO
//                    if (value != "true") {
//                        $("#Error_" + key).html(value[value.length - 1].ErrorMessage);
//                        $("#div_" + key).addClass(" has-error has-feedback");
//                    } else {
//                        $("#Error_" + key).html('');
//                        $("#div_" + key).removeClass(" has-error has-feedback");
//                    }

//                });
//            }

//        },
//        error: function (data) {
//            $('#alert_danger').html('error');
//            $('#alert_danger').show("fast");
//        },

//    });
//});
//CUANDO SE DE CLICK A EDITAR DESDE LA TABLA
//$("#tablePermisos").on('click', 'tr #editar', function () {
//    var idTask = $(this).parents("tr").find("td").eq(0).html();
//    var url = "/Usuario/Edit?id=" + idTask + ""; // Establecer URL de la acción
//    $("#btnEnviarEditar").prop('disabled', false);
//    $("#contenedor-editar").load(url);

//});
//$("input[name='CheekRol']").click(function () {
//    //var idTask = $(this).parents("tr").find("td").eq(0).html();
//    alert('ha');
//    //if ($(this).is(":checked")) // "this" refers to the element that fired the event
//    //{
//    //    alert(idTask);
//    //}
//});
//AL HACER CLICK EN AGREGAR NOS MOSTRARA EL MODAL CON EL FORMULARIO
//$("#agregar").click(function () {
//    var url = "/Usuario/Create"; // Establecer URL de la acción
//    $("#contenedor-agregar").load(url);
//});
$('#frm-example').on('submit', function (e) {
    var form = this;

    var rows_selected = table.column(0).checkboxes.selected();

    // Iterate over all selected checkboxes
    $.each(rows_selected, function (index, rowId) {
        // Create a hidden element
        $(form).append(
            $('<input>')
               .attr('type', 'hidden')
               .attr('name', 'id[]')
               .val(rowId)
        );
    });

    // FOR DEMONSTRATION ONLY
    // The code below is not needed in production

    // Output form data to a console
    $('#example-console-rows').text(rows_selected.join(","));

    // Output form data to a console
    $('#example-console-form').text($(form).serialize());

    // Remove added elements
    $('input[name="id\[\]"]', form).remove();

    // Prevent actual form submission
    e.preventDefault();
});

//FUNCIONES
function LoadGridPermisos() {
    var table = $('#tablePermisos').DataTable({
        sAjaxSource: '/Permisos/get',
        "columnDefs": [
            //targets DEBE SER IGUAL AL NUMERO DE LA COLUMNA QUE SE VA A AGRUPAR
            //SE VUELVEN INVISIBLE LAS COLUMNAS
            { "visible": false, "targets": [4, 0] },
            { "className": 'select-checkbox', "targets": 3 },
            { "className": 'select-checkbox', "targets": 2 }

        ],
        "columns": [
          { "data": "PermisoID" },
          { "data": "PermisoDescripcion" },
           {
               data: "CheekRol",
               render: function (data, type, row) {
                   if (type === 'display') {
                       return '<input id="h" type="checkbox" class="editor-active">';
                   }
                   return data;
               },
               className: "dt-body-center"

           },
          //{ "data": "CheekUsuarios" },
          {
              data:   "CheekUsuarios",
              render: function (data, type, row) {
                  if ( type === 'display' ){
                      if (data == true) {
                          return '<input name="CheekUsuarios" type="checkbox" checked>';
                      }
                      else {
                          return '<input name="CheekUsuarios" type="checkbox">';
                      }
                  }
                  else {
                      return '<input type="checkbox">';
                  }
              },

          },
          { "data": "ModuloDescripcion" },
        ],
        //4 ES EL NUMERO DE LA COLUMNA QUE SE VA A AGRUPAR
        "order": [[3, 'asc']],
        "displayLength": 25,
        "drawCallback": function (settings) {
            var api = this.api();
            var rows = api.rows({ page: 'current' }).nodes();
            var last = null;

            api.column(4, { page: 'current' }).data().each(function (group, i) {
                if (last !== group) {
                    $(rows).eq(i).before(
                        '<tr class="group" style="background-color: #E0F2F1;"><td colspan="5">' + group + '</td></tr>'
                    );

                    last = group;
                }
            });
        }

    });
}
//function esconderMensajes() {
//    $('#alert_danger').hide();
//    $('#alert_success').hide();
//    $('#alert_success_eliminar').hide();
//    $('#alert_danger_eliminar').hide();
//}
