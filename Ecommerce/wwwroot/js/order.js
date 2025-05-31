var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tableData').DataTable({
        "ajax": {
            url: '/admin/order/getall'
        },
        "columns": [
            { data: 'id', "width": '5%' },
            { data: 'name', "width": '10%' },
            { data: 'phoneNumber', "width": '20%' },
            { data: 'applicationUser.email', "width": '15%' },
            { data: 'orderStatus', "width": '10%' },
            { data: 'orderTotal', "width": '10%' },
            {
                data: 'id',
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Admin/Order/details?orderId=${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                <i class="bi bi-pencil-square"></i> Details
                            </a>
                        </div>`;
                },
                "width": "15%"
            }
        ]
    });
}