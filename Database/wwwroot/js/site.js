$(document).ready(function () {

    $(document).ready(function () {
        $('#good').on("click", function () {
            var UserCreate = { Name: "sfdasfdasg" }
            $.ajax({
                url: "/Users/GoodSave",
                dataType: 'json',
                contentType: "application/json",
                type: "POST",
                data: '{UserCreate: ' + JSON.stringify(UserCreate) + '}',
                success: function (response) {
                    alert(response.result);
                },
                failure: function (response) {
                    alert(response.responseText);
                },
                error: function (response) {
                    alert(response.responseText);
                }
            });
        });
    });


    
}