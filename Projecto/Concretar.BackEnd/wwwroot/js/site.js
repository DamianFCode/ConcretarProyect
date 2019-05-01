$(function () {

    $(document).ready(function () {
        $(document).on("keypress", "form", function (event) {
            return event.keyCode != 13;
        });

        seenotifications(); // Validar si existen alertas a mostrar

    })
});