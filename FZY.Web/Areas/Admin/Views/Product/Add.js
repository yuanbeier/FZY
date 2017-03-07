(function () {
    $(function () {
        $('.icon-check-square-o').click(function (e) {
            e.preventDefault();
            var callback = function (data) {
                location.href =  '/admin/product/index';
            }
            var obj =topevery.serializeObject($(".form-x"));
            topevery.ajax({
                url: 'api/services/app/webSiteAppServer/AddProductAsync',
                type: 'POST',
                data: JSON.stringify($.extend(obj,
                {
                    "productImage": $("input[name='IdFilehiddenFile']").val(),
                    "styleImage": $("input[name='IdFile2hiddenFile']").val()
                })),
                contentType: "application/json"
            }, callback, false);

        });

    });
})();