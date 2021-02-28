$(document).ready(function () {
    loadList();
})


function loadList() {
    var DT_load_reviewable = $('#DT_load_reviewable').DataTable({
        destroy: true,
        "ajax": {
            "url": "/Farmer/Report/GetAllReviewableFarmerReports/",
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
                    return ` <span style="display:block; text-align:center; margin:0 auto;">
                             <a href="/Farmer/Report/View?id=${data}" class="btn-sm btn-info text-white" style="cursor:pointer; width:100px;">
                                    <i class="fas fa-eye"></i>
                                </a>&nbsp
                                <a href="/Farmer/Report/Edit?id=${data}" class=" btn-sm btn-warning text-black" style="cursor:pointer; width:100px;">
                                   <i class="fas fa-pen-square"></i>
                                </a>&nbsp
                                 <a href="/Farmer/Report/Delete?id=${data}" class="btn-sm btn-danger text-white" style="cursor:pointer; width:100px;">
                                    <i class="fas fa-trash-alt"></i>
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

    var DT_load_pending = $('#DT_load_pending').DataTable({
        destroy: true,
        "ajax": {
            "url": "/Farmer/Report/GetAllPendingFarmerReports/",
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
                                 <a href="/Farmer/Report/View?id=${data}" class="btn-sm btn-info text-white" style="cursor:pointer; width:100px;">
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
            "url": "/Farmer/Report/GetAllApprovedFarmerReports/",
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
                    return ` <span style="display:block; text-align:center; margin:0 auto;">
                                 <a href="/Farmer/Report/View?id=${data}" class="btn-sm btn-info text-white" style="cursor:pointer; width:100px;">
                                    <i class="fas fa-eye"></i>
                                </a>&nbsp
                                <a href="/Farmer/Inquiry/New?id=${data}" class=" btn-sm btn-warning" style="cursor:pointer; width:100px;">
                                   <i class="fas fa-question "></i>
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

    var DT_load_rejected = $('#DT_load_rejected').DataTable({
        destroy: true,
        "ajax": {
            "url": "/Farmer/Report/GetAllRejectedFarmerReports/",
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
                    return ` <span style="display:block; text-align:center; margin:0 auto;">
                                 <a href="/Farmer/Report/View?id=${data}" class="btn-sm btn-info text-white" style="cursor:pointer; width:100px;">
                                    <i class="fas fa-eye"></i>
                                </a>&nbsp
                                <a href="/Farmer/Inquiry/New?id=${data}" class=" btn-sm btn-warning" style="cursor:pointer; width:100px;">
                                   <i class="fas fa-question "></i>
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

