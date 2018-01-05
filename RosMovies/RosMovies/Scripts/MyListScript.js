$(document).ready(function () {
    $('#mySearch').on('keyup paste', function () {
        console.log($(this).val());
        var obj = {
            query: $('#mySearch').val()
            };
        //console.log(obj);
        $.ajax({
            type: 'POST',
            url: '/Movie/SearchMovieList',
            data: JSON.stringify(obj),
            contentType: 'application/json; charset=UTF-8',
            success: function (response) {
                console.log(response)
                var trHtml = response.map(function (el) {
                            return createTr(el);
                        }).join("");
                $('#myTable tbody').html(trHtml);
            },
            error: function (response) {
                console.log(response)
            }
        });
    });
});



function createTr(movie) {
    return '<tr><th>' + '<a href="/Movie/Details/' + movie.Id +'">' + movie.Name + '</a></th></tr>';
}
