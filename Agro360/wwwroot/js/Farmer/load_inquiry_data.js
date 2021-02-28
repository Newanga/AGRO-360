$(document).ready(function () {
    loadList();
})


function loadList() {
    var DT_load_pending = $('#DT_load_pending').DataTable({
        destroy: true,
        "ajax": {
            "url": "/Farmer/Inquiry/GetAllPendingInquiry/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "date", "width": "auto" },
            { "data": "time", "width": "auto" },
            { "data": "reportId", "width": "auto" },
            {
                "data": "inquiryId",
                "render": function (data) {
                    return ` <span style="display:block; text-align:center; margin:0 auto;">
                             <a href="/Farmer/Inquiry/View?id=${data}" class="btn-sm btn-info text-white" style="cursor:pointer; width:100px;">
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

    var DT_load_solved = $('#DT_load_solved').DataTable({
        destroy: true,
        "ajax": {
            "url": "/Farmer/Inquiry/GetAllSolvedInquiry/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "date", "width": "auto" },
            { "data": "time", "width": "auto" },
            { "data": "reportId", "width": "auto" },
            {
                "data": "inquiryId",
                "render": function (data) {
                    return ` <span style="display:block; text-align:center; margin:0 auto;">
                             <a href="/Farmer/Inquiry/View?id=${data}" class="btn-sm btn-info text-white" style="cursor:pointer; width:100px;">
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

