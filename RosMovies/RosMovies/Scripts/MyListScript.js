$(document).ready(function () {
    $('#mySearch').on('keyup paste', function () {
        //console.log($(this).val());
        var obj = { myQuery: $(this).val() };
        console.log($(this).val());
        $.ajax({
            type: 'POST',
            url: '/Movie/NewMovieList',
            data: JSON.stringify(obj),
            contentType: 'application/json; charset=UTF-8',
            success: function (response) {
                console.log(response)
            },
            error: function (response) {
                console.log(response)
            }
        });
    });
});






    //$('#Search').on('keyup paste',
    //        function () {
    //    console.log($(this).val());
    //    var obj = {
    //                searchName: $(this).val()
    //                };
    //            $.ajax({
    //                type: 'POST',
    //                url: '/Products/Search',
    //                data: JSON.stringify(obj),
    //                contentType: 'application/json; charset=UTF-8',
    //                success: function (response) {
    //    console.log(response);
    //var trHtml = response.map(function (el) {
    //                        return createTr(el);
    //                    }).join("");
    //                    $('#ourtable tbody').html(trHtml);
    //                },
    //                error: function (response) {
    //    console.log(response);
    //}
    //            });
    //        });


    //    function createTr(product) {
    //        return '<tr><th>' +
    //            product.Name + '</th><th>' + product.Price + '</th><th>' + product.Category + '</th><td>' +
    //            '</th><td> <a href="/Products/Buy/' +
    //        product.Id +
    //            '">Buy</a> |<a href="/Products/Edit/' +
    //        product.Id +
    //            '">Edit</a> |<a href="/Products/Details/' +
    //    product.Id +
    //            '">Details</a> | <a href="/Products/Delete/' +
    //    product.Id +
    //'">Delete</a> </td></tr>';

    //    }

      
    //</script >

    //<script>
    //    $(document).ready(function () {
    //        $('#theme').change(function () {
    //            var obj = {
    //                searchName: $(this).val()
    //            };
    //            $.ajax({
    //                type: 'POST',
    //                url: '/Products/CategorySearch',
    //                data: JSON.stringify(obj),
    //                contentType: 'application/json; charset=UTF-8',
    //                success: function (response) {
    //                    console.log(response);
    //                    var newHtml = response.map(function (el) {
    //                        return createPr(el);
    //                    }).join("");
    //                    $('#ourtable tbody').html(newHtml);
    //                },
    //                error: function (response) {
    //                    console.log(response);
    //                }
    //            });
    //        });

    //    });



    //        function createPr(product) {
    //            return '<tr><th>' +
    //                product.Name + '</th><th>' + product.Price + '</th><th>' + product.Category + '</th><td>' +
    //                '</th><td> <a href="/Products/Buy/' +
    //            product.Id +
    //                '">Buy</a> |<a href="/Products/Edit/' +
    //            product.Id +
    //                '">Edit</a> |<a href="/Products/Details/' +
    //        product.Id +
    //                '">Details</a> | <a href="/Products/Delete/' +
    //    product.Id +
    //'">Delete</a> </td></tr>';

    //        }

