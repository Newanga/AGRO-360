var dataTable;

$(document).ready(function () {
    loadList();
})


function loadList() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/Admin/Harvest/GetAllHarvestTypes/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "harvestId", "width": "" },
            { "data": "harvestType", "width": "auto" },
            {
                "data": "harvestId",
                "render": function (data) {
                    return ` <span style="display:block; text-align:center; margin:0 auto;">
                                <a href="/Admin/Harvest/Edit?id=${data}" class=" btn-sm btn-warning text-black" style="cursor:pointer; width:100px;">
                                    <i class="fas fa-edit"></i>
                                </a>&nbsp

                    </span>`;
                }, "width": "auto"
            }
        ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
    });
}
