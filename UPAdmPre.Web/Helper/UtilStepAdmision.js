
((function () {

    $(function () {
        app.init()
    })

    var app = {
        init: function () {
            app.GetStepsAdmision()
        },
        GetStepsAdmision: function () {

            $.ajax({
                url: UrlAcion.UrlGetStepsAdmision,
                type: 'post',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, b, c) {
                    document.getElementById("contentStepsAdmision").innerHTML = data.d
                    tooltip.refresh();
                }
            })

        }
    }
})())


function Check(id) {
    $(id).closest("tbody").find("input").not(id).prop('checked', false);
}