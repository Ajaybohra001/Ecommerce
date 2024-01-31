var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/Admin/GetAll' },
        "columns": [
            { data: 'productId', "width": "25%" },
            { data: 'productName', "width": "15%" },
            { data: 'productPrice', "width": "10%" },
            { data: 'cateogryName', "width": "15%" },
            { data: 'productCreatedDate', "width": "25%" },

            
            { data: 'isAvailable', "width": "10%" },
            { data: 'isTrending', "width": "15%" },
            

           
            {
                data: 'productId',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                     <a href="/Admin/ViewProduct/${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square "></i> Edit</a>   
                     

                     <a onClick=Delete('/admin/DeleteProduct/${data}') class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
                    </div>`
                },
                "width": "25%"
            }
        ]
    });
}
function editProduct(productId) {
    // Redirect to the edit page with the product ID
    window.location.href = `/admin/ViewProduct/?id=${productId}`
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
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
    })
}