var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tableData').DataTable({
        "ajax": {
            url: '/admin/company/getall'
        },
        "columns": [
            { data: 'name', "width": '15%' },
            { data: 'streetAddress', "width": '10%' },
            { data: 'city', "width": '15%' },
            { data: 'state', "width": '15%' },
            { data: 'phoneNumber', "width": '10%' },
            {
                data: 'id',
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Admin/Company/Upsert?id=${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                <i class="bi bi-pencil-square"></i> Edit
                            </a>
                            &nbsp;
                            <a onClick=Delete("/Admin/Company/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer; width:100px;">
                                <i class="bi bi-trash-fill"></i> Delete
                            </a>
                        </div>`;
                },
                "width": "15%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    });
}