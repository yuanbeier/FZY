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

        function bindCategory(response) {
            if (response.result.rows != null && response.result.total != undefined && response.result.total > 0) {
                var html = "";
                var id = $("#categoryId").val();
                for (var i = 0; i < response.result.rows.length; i++) {
                    var n = response.result.rows[i];
                    if (id && id == n.id) {
                        html = '<option selected value=\'' + n.id + '\'>' + n.name
                      + '</option>';
                    } else {
                        html = '<option value=\'' + n.id + '\'>' + n.name
                      + '</option>';
                    }
                    $("#category").append(html);
                }
            }
        }

        topevery.ajax({
            url: 'api/services/app/webSiteAppServer/GetCategoryListAsync',
            type: 'POST',
            data: JSON.stringify({  pageIndex:  1, pageCount: 20 }),
            contentType: "application/json"
        }, bindCategory, false);

    });
})();