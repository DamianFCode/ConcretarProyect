//Mensajes de notificación-----------------------------------------------
$('.swal-delete').click(function (e) {
    var href = $(this).attr('href');
    e.preventDefault();
    swal({
        title: "Confirmar",
        text: "El registro se eliminará ¿Está Seguro?",
        type: "warning",
        width: 350,
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si",
        cancelButtonText: "No"
    }).then(function () {
        window.location = href;
    }, function (dismiss) {

    });
});
$('body').on('click', '.swal-confirmation', function (e) {
    var href = $(this).attr('href');
    var msg = $(this).data('msg');
    e.preventDefault();
    swal({
        title: "¿Está seguro?",
        text: msg,
        type: "warning",
        width: 350,
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si",
        cancelButtonText: "No"
    }).then(function () {
        window.location = href;
    }, function (dismiss) {

    });
});

//TOGGLER RADIO BUTTONS URL
$(".toggler-url").click(function () {
    $(".toggler-control2").addClass("hidden-xs-up");
    $("#" + $(this).data("toggle-element")).removeClass("hidden-xs-up");
});

function addHidden(theForm, key, value) {
    // Create a hidden input element, and append it to the form:
    var input = document.createElement('input');
    input.type = 'hidden';
    input.name = key; // 'the key/name of the attribute/field that is sent to the server
    input.value = value;
    theForm.appendChild(input);
}

function nSuccess(msj, type) {
    var title = "EXITO";
    GenerateNotify(type, msj, title);

}
function nError(msj, type) {
    var title = "ERROR";
    GenerateNotify("danger", msj, title);
}

function GenerateNotify(alertType, msj, title) {
    $.notify(
        {
            title: "<strong>" + title + ":</strong>",
            message: msj.replace("'", '"')
        },
        {
            type: alertType,
            offset: {
                y: 80,
                x: 20
            },
            delay: 5000,
            placement: {
                from: "top",
                align: "right"
            },
            animate: {
                enter: 'animated flipInY',
                exit: 'animated flipOutX'
            }
        });
}
//Función que cierra todos los modales Bootstrap de la pantalla.
function CloseForms() {
    $(".modal").modal('hide');
    $(".modal").find("form").get(0).reset();
}

//Metodo que permite subida de archivos via ajax request en un form HTML.
//Para utilizarlo, colocarle al form la clase "psm-custom-ajax" y los siguientes atributos:
// data-ajax-url= Url del request. 
// data-ajax-method= POST / GET
// data-ajax-update= Selector del elemento HTML donde se colocará el resultado del request.
// data-ajax-success= Nombre de la función JS a invocar cuando el request termine exitosamente.
// TODO: Atributos opcionales. Posibilidad de especificar si con el resultado se hace replace, append, etc. Funcion de error. Validación de form
$(".psm-custom-ajax").bind('submit', function (e) {

    var formdata = new FormData($(this).get(0));
    var update = $(this).data("ajax-update");
    var successFunction = $(this).data("ajax-success");
    var type = $(this).data("ajax-method");
    var url = $(this).data("ajax-url");
    $.ajax({
        url: url,
        type: type,
        data: formdata,
        processData: false,
        contentType: false,
        success: function (result) {
            $(update).html(result);
            window[successFunction]();
        }

    });
    e.preventDefault();
    return false;
});

//Metodo para añadir spinner al clickear boton
$('.btn-loading').click(function () {
    $(this).html('<i class="zmdi zmdi-rotate-right zmdi-hc-spin"></i> Procesando');
});

