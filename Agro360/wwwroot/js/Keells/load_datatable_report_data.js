$(document).ready(function () {
    loadList();
})


function loadList() {

    var DT_load_pending = $('#DT_load_pending').DataTable({
        destroy: true,
        "ajax": {
            "url": "/Keells/Report/GetAllPendingFarmerReports/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "date", "width": "auto" },
            { "data": "time", "width": "auto" },
            { "data": "harvest", "width": "auto" },
            { "data": "quantity", "width": "auto" },
            {
                "data": "id",
                "render": function (data) {
                    return ` <span class="text-center">
                                 <a href="/Keells/Report/ViewPending?id=${data}" class="btn-sm btn-info text-white" style="cursor:pointer; width:100px;">
                                        <i class="fas fa-eye"></i>
                                </a>
                    </span>`;
                }, "width": "auto"
            }
        ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
    });

    var DT_load_approved = $('#DT_load_approved').DataTable({
        destroy: true,
        "ajax": {
            "url": "/Keells/Report/GetAllApprovedFarmerReports/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "date", "width": "auto" },
            { "data": "time", "width": "auto" },
            { "data": "harvest", "width": "auto" },
            { "data": "quantity", "width": "auto" },
            {
                "data": "id",
                "render": function (data) {
                    return ` <span class="text-center">
                                 <a href="/Keells/Report/View?id=${data}" class="btn-sm btn-info text-white" style="cursor:pointer; width:100px;">
                                    <i class="fas fa-eye"></i>
                                </a>
                    </span>`;
                }, "width": "auto"
            }
        ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
    });

    var DT_load_keellsrejected = $('#DT_load_keellsrejected').DataTable({
        destroy: true,
        "ajax": {
            "url": "/Keells/Report/GetAllKeellsRejectedFarmerReports/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "date", "width": "auto" },
            { "data": "time", "width": "auto" },
            { "data": "harvest", "width": "auto" },
            { "data": "quantity", "width": "auto" },
            {
                "data": "id",
                "render": function (data) {
                    return ` <span class="text-center">
                                 <a href="/Keells/Report/View?id=${data}" class="btn-sm btn-info text-white" style="cursor:pointer; width:100px;">
                                    <i class="fas fa-eye"></i>
                                </a>
                    </span>`;
                }, "width": "auto"
            }
        ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
    });

    var DT_load_doarejected = $('#DT_load_daorejected').DataTable({
        destroy: true,
        "ajax": {
            "url": "/Keells/Report/GetAllDOARejectedFarmerReports/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "date", "width": "auto" },
            { "data": "time", "width": "auto" },
            { "data": "harvest", "width": "auto" },
            { "data": "quantity", "width": "auto" },
            {
                "data": "id",
                "render": function (data) {
                    return ` <span class="text-center">
                                 <a href="/Keells/Report/View?id=${data}" class="btn-sm btn-info text-white" style="cursor:pointer; width:100px;">
                                    <i class="fas fa-eye"></i>
                                </a>
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

