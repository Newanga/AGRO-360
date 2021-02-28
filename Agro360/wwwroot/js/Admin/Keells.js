var dataTable;

$(document).ready(function () {
    loadList();
})


function loadList() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/Admin/Keells/GetAll/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "firstName", "width": "auto" },
            { "data": "lastName", "width": "auto" },
            { "data": "email", "width": "auto" },
            { "data": "address", "width": "auto" },
            { "data": "nic", "width": "auto" },
            { "data": "phoneNumber", "width": "auto" },
            {
                "data": { id: "id", lockoutEnd: "lockoutEnd" },
                "render": function (data) {
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();
                    if (lockout > today) {
                        //currently user is locked
                        return ` <div class="text-center">
                                <a class="btn btn-danger text-white" style="cursor:pointer; width:100px;" onclick=LockUnlock('${data.id}')>
                                   <i class="fas fa-lock-open"></i> Unlock
                                </a></div>`;

                    }
                    else {
                        return ` <div class="text-center">
                                <a class="btn btn-success text-white" style="cursor:pointer; width:100px;" onclick=LockUnlock('${data.id}')>
                                    <i class="fas fa-lock"></i> Lock
                                </a></div>`;
                    }

                }, "width": "auto"
            }
        ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
    });
}


function LockUnlock(id) {

    $.ajax({
        type: 'POST',
        url: '/Admin/Keells/LockUnlock/',
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                dataTable.ajax.reload();
            }
        }
    });

}
