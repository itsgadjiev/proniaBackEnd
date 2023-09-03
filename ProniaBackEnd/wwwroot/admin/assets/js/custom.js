﻿function GetAjaxCategories() {
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
        success: function (newCategory) {
            let newRow = `
                <tr class="category-${newCategory.id}-ajax category-add-ajax">
                    <td>${newCategory.name}</td>
                    <td>
                        <a class="btn btn-de-primary update-category-ajax" data-bs-toggle="modal" data-bs-target="#updateModalLogin" onclick="UpdateCategoryModal('${newCategory.name}', '${newCategory.id}', '${newCategory.createdOn}')">
                            Update
                        </a>
                        <a class="btn btn-danger" onclick="DeleteCategory('${newCategory.Id}')">Delete</a>
                    </td>
                </tr>
            `;
            $('#categoryList').append(newRow);
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
    let creationDateString = $('.categoryCreationDate-update-modal').val();

    let creationDate = new Date(creationDateString);


    let categoryVM = {
        Id: catId,
        Name: categoryName,
        CreatedOn: creationDate
    };

    let elementClass = `category-${catId}-ajax-name`;

    $.ajax({
        url: '/Manage/category/update/' + catId,
        type: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify(categoryVM),
        success: function (response) {

            $(`.${elementClass}`).text(categoryVM.Name);
            console.log(categoryVM)

            UpdateCategoryModal(categoryVM.Name, categoryVM.Id, categoryVM.CreatedOn);

            $('.categoryId-update-modal').val(response.Id);
            $('.category-name-update-ajax').val(categoryVM.Name);
            $('.categoryCreationDate-update-modal').val(response.createdOn);

        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
}

function UpdateCategoryModal(categoryName, categoryId, categoryCreationDate) {
    $('.categoryId-update-modal').val(categoryId);
    $('.category-name-update-ajax').val(categoryName);
    $('.categoryCreationDate-update-modal').val(categoryCreationDate);

}

function DeleteCategory(categoryId) {
    $.ajax({
        url: '/Manage/category/delete/' + categoryId,
        type: 'DELETE',
        success: function (response) {

        },
        error: function (error) {
            console.error('Error:', error);
        }
    });

    $(`[class*="category-${categoryId}-ajax"]`).remove();
}





