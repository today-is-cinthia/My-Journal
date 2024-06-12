$('#example').DataTable({
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
    // Atributo para hacer la tabla responsive
    responsive: true,
    // Agregar scroll a la tabla para que quepa en el contenedor
    "sScrollX": "100%",
    "sScrollXInner": "110%",
    "bScrollCollapse": true

});

document.addEventListener('DOMContentLoaded', (event) => {
    // Obtener la fecha actual
    const today = new Date().toISOString().split('T')[0];

    // Asignar la fecha actual a los campos de entrada
    document.getElementById('startDate').value = today;
    document.getElementById('endDate').value = today;
});

function submitDates() {
    const startDate = document.getElementById('startDate').value;
    const endDate = document.getElementById('endDate').value;

    alert(`Start Date: ${startDate}\nEnd Date: ${endDate}`);

    // Aquí puedes añadir lógica para manejar los datos como desees
}