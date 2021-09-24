var dataTable;

$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/adjectives",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { data: "adjectiveID", width: "25%" },
            { data: "adjName", width: "25%" },
            { data: "adjDefinition", width: "25%" },
            { data: "adjType", width: "25%" },          
            
        ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
    });
}



