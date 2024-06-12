var table;

function agregarRegistro() {
    // Obtener valores de los inputs
    var categoriaSelect = document.getElementById("categoria");
    var categoria = categoriaSelect.value;
    var categoriaText = categoriaSelect.options[categoriaSelect.selectedIndex].text;

    var descripcion = document.getElementById("descripcion").value;
    var cantidad = document.getElementById("cantidad").value;

    var divisaSelect = document.getElementById("divisa");
    var divisa = divisaSelect.value;
    var divisaText = divisaSelect.options[divisaSelect.selectedIndex].text;

    var tasaCambio = document.getElementById("tasaCambio").value;
    var fecha = document.getElementById("fecha").value;

    // Validar que todos los campos estén llenos
    if (!categoria || !descripcion || !cantidad || !divisa || !tasaCambio || !fecha) {
        alert("Todos los campos son obligatorios.");
        return;
    }

    // Crear una nueva fila
    table.row.add([
        categoriaText,
        descripcion,
        cantidad,
        divisaText,
        tasaCambio,
        fecha,
        '<button class="btn btn-danger btn-sm" onclick="eliminarRegistro(this)"><i class="fa fa-trash" aria-hidden="true"></i></button>'
    ]).draw(false);

    // Añadir campos ocultos para enviar al controlador
    var form = $('form');
    form.append('<input type="hidden" name="OfrendaCategoria" value="' + categoria + '">');
    form.append('<input type="hidden" name="Descripcion" value="' + descripcion + '">');
    form.append('<input type="hidden" name="Cantidad" value="' + cantidad + '">');
    form.append('<input type="hidden" name="Divisa" value="' + divisa + '">');
    form.append('<input type="hidden" name="TasaCambio" value="' + tasaCambio + '">');
    form.append('<input type="hidden" name="Fecha" value="' + fecha + '">');

    // Limpiar los campos de entrada
    document.getElementById("categoria").selectedIndex = 0;
    document.getElementById("descripcion").value = '';
    document.getElementById("cantidad").value = '';
    document.getElementById("divisa").selectedIndex = 0;
    document.getElementById("tasaCambio").value = '';
    document.getElementById("fecha").value = '';

    // Actualizar la tabla para que sea responsive
    table.responsive.recalc();
}

function eliminarRegistro(button) {
    // Obtener la fila padre del botón y eliminarla
    var row = table.row($(button).parents('tr'));
    row.remove().draw();

    // Actualizar la tabla para que sea responsive
    table.responsive.recalc();
}

$(document).ready(function () {
    table = $('#example').DataTable({
        "language": {
            "sProcessing": "Procesando...",
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "No se encontraron resultados",
            "sEmptyTable": "Ningún dato disponible en esta tabla",
            "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
            "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            "sInfoPostFix": "",
            "sSearch": "Buscar:",
            "sUrl": "",
            "sInfoThousands": ",",
            "sLoadingRecords": "Cargando...",
            "oPaginate": {
                "sFirst": "Primero",
                "sLast": "Último",
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            },
            "oAria": {
                "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                "sSortDescending": ": Activar para ordenar la columna de manera descendente"
            }
        },
        responsive: true,
        "sScrollX": "100%",
        "sScrollXInner": "110%",
        "bScrollCollapse": true
    });
});