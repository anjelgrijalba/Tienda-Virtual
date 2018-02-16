$(function () {
    //Eventos botones
    $('#btnAumentarCantidad').click(function (e) {
        e.preventDefault();

        var cantidad = ++($('#cantidad').get(0).value);
        var precio = $('#lblPrecio').data("importe");

        $('#lblTotal').html(cantidad * precio);

        //$('#cantidad').val($('#cantidad').val() + 1);
    });

    $('#btnReducirCantidad').click(function (e) {
        e.preventDefault();

        var cantidad = $('#cantidad').get(0).value;

        if (cantidad != 0) {
            
            $('#cantidad').get(0).value = --cantidad;

            var precio = $('#lblPrecio').data("importe");

            $('#lblTotal').html(cantidad * precio);
        }
        //$('#cantidad').val($('#cantidad').val() + 1);
    });
});