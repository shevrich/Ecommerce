$(function () {
    $(".btn-danger").on('click', function () {
        var cartId = $(this).attr("data-cart-Id");
        var productId = $(this).attr("data-product-Id");
        $.post('/home/delete', { CartId: cartId, ProductId: productId }, function (result) {
            window.location.reload();
        })
    })

    $(".btn-warning").on('click', function () {
        var id = $(this).attr("data-id");
        $(`.quantity-${id}`).hide();
        $(`.quantity-selector-${id}`).show();
        $(`.edit-${id}`).hide();
        $(`.save-${id}`).show();
    })

    $(".btn-info").on('click', function () {
        var id = $(this).attr("data-id");
        var cartId = $(this).attr("data-cart-Id");
        var productId = $(this).attr("data-product-Id");
        var quantity = $(`.quantity-selector-${id} option:selected`).val();
        $.post('/home/edit', { ProductId: productId, CartId: cartId, Quantity: quantity }, function (result) {
            window.location.reload();
        })
    })
})