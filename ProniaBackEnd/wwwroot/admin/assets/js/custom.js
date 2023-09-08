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
        success: function (newCategory) {
            let $newRow = $('<tr>', {
                class: `category-${newCategory.id}-ajax category-add-ajax`,
            });

            let $nameTd = $('<td>', {
                text: newCategory.name,
                class: `category-${newCategory.id}-ajax-name`,
            });

            $newRow.append($nameTd);

            let $actionsTd = $('<td>');
            let $updateButton = $('<a>', {
                class: `btn btn-de-primary update-category-ajax update-category-ajax-${newCategory.id}`,
                'data-bs-toggle': 'modal',
                'data-bs-target': '#updateModalLogin',
                text: 'Update',
                click: function () {
                    UpdateCategoryModal(newCategory.name, newCategory.id, newCategory.createdOn);
                },
            });
            let $deleteButton = $('<a>', {
                class: 'btn btn-danger',
                click: function () {
                    DeleteCategory(newCategory.id);
                },
            }).text('Delete');
            $actionsTd.append($updateButton, $deleteButton);
            $newRow.append($actionsTd);

            $updateButton.attr('onclick', `UpdateCategoryModal('${newCategory.name}', '${newCategory.id}', '${newCategory.createdOn}')`);

            $('#categoryList').append($newRow);
        },
        error: function (error) {
            for (var i = 0; i < error.responseJSON.length; i++) {
                let elementId = error.responseJSON[i].propertyName;

                $(`#${elementId}`).text(error.responseJSON[i].errorMessage);

                setTimeout(function () {
                    $(`#${elementId}`).text("");
                }, 3000);
            }
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

    let elementClass = `update-category-ajax-${catId}`;

    if ($(`.${elementClass}`).length > 0) {
        $.ajax({
            url: '/Manage/category/update/' + catId,
            type: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify(categoryVM),
            success: function (response) {
                $(`.category-${catId}-ajax-name`).text(categoryVM.Name);

                let newOnclick = `UpdateCategoryModal('${categoryName}', '${catId}', '${creationDateString}')`;
                $(`.update-category-ajax-${catId}`).attr('onclick', newOnclick);

                console.log(response, "gfsdgadfg");
            },
            error: function (error) {
                for (var i = 0; i < error.responseJSON.length; i++) {
                    let elementId = error.responseJSON[i].propertyName;

                    $(`#${elementId}-Update`).text(error.responseJSON[i].errorMessage);

                    setTimeout(function () {
                        $(`#${elementId}-Update`).text("");
                    }, 3000);
                }
            }
        });
    } 
}
function UpdateCategoryModal(categoryName, categoryId, categoryCreationDate) {
    $('.categoryId-update-modal').val(categoryId);
    $('.category-name-update-ajax').val(categoryName);
    $('.categoryCreationDate-update-modal').val(categoryCreationDate);

    $('#updateModalLogin').modal('show');
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