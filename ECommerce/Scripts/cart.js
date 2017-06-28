$(function () {
    $("#addToCart").on('click', function () {
        var dateCreated = $('.hidden').val();
        var productId = $(this).attr('data-productId');
        var quantity = $("#quantity option:selected").val();
        $.post('/home/addToCart', { DateCreated: dateCreated, ProductId: productId, Quantity: quantity }, function (result) {
            window.location.reload();
        })
    })

})