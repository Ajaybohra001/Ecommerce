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
           
            {
                data: 'productCreatedDate', "width": "15%", render: function (data, type, row) {
                    // Format the date using Moment.js
                    return moment(data).format('DD-MM-YYYY HH:mm:ss');
                }
            },

            
            { data: 'isAvailable', "width": "10%" },
            { data: 'isTrending', "width": "15%" },
            

           
            {
                data: 'productId',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                     <a href="/Admin/ViewProduct/${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square "></i> Edit</a>   

                     

                    
                    </div>`
                },
                "width": "25%"
            }
        ],
        "order" : [[4 , 'desc']]
    });

    $('#IsAvailableButton').on('click', function () {
        var isAvailableChecked = $(this).hasClass('active');

        console.log('Button state:', isAvailableChecked);

        // Use DataTable's search API to filter rows based on button status
        var columnIndex = 5; // Change this to the correct index
        var searchTerm = isAvailableChecked ? 'true' : '';

        console.log('Applying search:', searchTerm);

        dataTable.column(columnIndex).search(searchTerm).draw();
    });

    // Toggle active class on button click
    $('#IsAvailableButton').on('click', function () {
        $(this).toggleClass('active');
    });

    $('#IsTrendingButton').on('click', function () {
        var isAvailableChecked = $(this).hasClass('active');

        console.log('Button state:', isAvailableChecked);

        // Use DataTable's search API to filter rows based on button status
        var columnIndex = 6; // Change this to the correct index
        var searchTerm = isAvailableChecked ? 'true' : '';

        console.log('Applying search:', searchTerm);

        dataTable.column(columnIndex).search(searchTerm).draw();
    });

    // Toggle active class on button click
    $('#IsTrendingButton').on('click', function () {
        $(this).toggleClass('active');
    });
  


}
function selectCategory(categoryId, categoryName) {
    // Update the dropdown button text to show the selected category
    $('#categoryDropdown').text(categoryName);

    // Make AJAX call to fetch data based on selected category
    $.ajax({
        url: '/Admin/GetDataByCategory', // Adjust the URL if needed
        type: 'POST',
        data: { categoryId: categoryId },
        success: function (data) {
            // Handle success, update DataTable with new data
            dataTable.clear().rows.add(data).draw();
        },
        error: function (xhr, textStatus, errorThrown) {
            // Handle error
            console.error('Error:', errorThrown);
        }
    });
}
function editProduct(productId) {
    // Redirect to the edit page with the product ID
    window.location.href = `/admin/ViewProduct/?id=${productId}`
}

function Delete(productId) {
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
                url: '/Admin/DeleteProduct/${productId}',
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    })
}