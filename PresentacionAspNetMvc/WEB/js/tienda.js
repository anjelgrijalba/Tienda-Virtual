$(function () {
    $('#ficha, #carrito, #factura, #login').hide();

    //Cambios de pantalla
    $('#btnAddCarrito').click(function () {
        //alert($(this).data('id'));
        $('#index').hide(); //fadeOut(2000); //slideUp(); //hide();
        $('#ficha').show(); //fadeIn(2000); //slideDown(); //show();
    });

    $('#frmCarrito').submit(function (e) {
        e.preventDefault();

        $('#ficha').hide();
        $('#carrito').show();
    });

    $('#btnFactura').click(function (e) {
        e.preventDefault();

        $('#carrito').hide();
        $('#factura').show();
    });

    //Eventos botones
    $('#btnAumentarCantidad').click(function (e) {
        e.preventDefault();
        $('#cantidad').get(0).value++;
        //$('#cantidad').val($('#cantidad').val() + 1);
    });

    $('#btnReducirCantidad').click(function (e) {
        e.preventDefault();
        $('#cantidad').get(0).value--;
        //$('#cantidad').val($('#cantidad').val() + 1);
    });
});