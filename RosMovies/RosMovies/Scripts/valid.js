//alert('scripts works')

$(document).ready(function () {
    $('#myId').on('keyup paste', function () {
        //console.log($(this).val());
        var myQuery = $(this).val();
        var myInt = 250;
        if (myQuery.length > myInt) {
            console.log($(this).val());
            $('#myButtomId').attr({ "type": "hidden" });
        }
        else {
            $('#myButtomId').attr({ "type": "submit" });
        }
    });
});

