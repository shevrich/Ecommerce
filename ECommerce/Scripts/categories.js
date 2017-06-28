$(function () {
    $(".categories-list").on("click", function () {
        var id = $(this).attr('data-categoryId');
        $.get('/home/getProducts', { categId: id }, function (data) {
            $(".col-sm-4").remove();
            data.forEach(function (product) {
                $('.pics').append(`
                        <div class ="col-sm-4 col-lg-4 col-md-4 " style="height: 200px; width: 250px; ">
                         <div class="thumbnail">
                            <img src="/Images/${product.imageFile}" height="100" width="75"  alt="">
                            <div class="caption">
                                <h4 class ="pull-right">$${product.price}</h4>
                                <h4>
                                    <a href="/home/viewImage?Id=${product.id}">${product.title}</a>
                                </h4>
                                <p>${product.description.Length < 70 ? product.description : product.description.substring(0, 70) +"..."} </p>
                            </div>
                        </div>
                    </div>`)
            })
        })
    })
})