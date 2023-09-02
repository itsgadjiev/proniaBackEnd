function GetAjaxCategories() {
    $(document).ready(function () {
        $.ajax({
            url: '/Manage/category', 
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                
                
            },
            error: function (error) {
                console.error('Error:', error);
            }
        });
    });
}



function AddDataCategory() {

    let category = {
        Name: $('.category-name-ajax').val(),
    };
   
    $.ajax({
        url: '/Manage/category/create',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(category),
        success: function (response) {
        },
        error: function (error) {
            console.error('Error:', error);
        }
    });

    $('.category-name-ajax').val('');
}

function UpdateCategory() {
    let categoryName = $('.category-name-update-ajax').val();
    let catId = $('.categoryId-update-modal').val();

    let category = {
        Name: categoryName,
        Id: catId
    }

    $.ajax({
        url: '/Manage/category/update/' + catId , 
        type: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify(category), 
        success: function (response) {
            
        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
}

function UpdateCategoryModal(categoryName,categoryId) {
    $('.categoryId-update-modal').val(categoryId);
    $('.category-name-update-ajax').val(categoryName);
}






