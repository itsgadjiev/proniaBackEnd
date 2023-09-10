$(document).ready(function () {
    $('.view-details-btn').click(function () {
        var orderId = $(this).data('order-id');

        $.ajax({
            url: '/client/order-details/' + orderId,
            type: 'GET',
            dataType: 'html',
            success: function (data) {
                $('#orderDetailsContainer').html(data);
            },
            error: function () {
            }
        });
    });
});