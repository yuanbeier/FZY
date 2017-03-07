(function () {
    $(function () {
        $('.icon-check-square-o').click(function (e) {
            e.preventDefault();
            var callback = function (data) {
                location.href =  '/admin/adv/index';
            }
            var obj =topevery.serializeObject($(".form-x"));
            topevery.ajax({
                url:  'api/services/app/webSiteAppServer/AddHomePicAsync',
                type: 'POST',
                data: JSON.stringify($.extend(obj,{ "imageUrl": $("input[name='IdFilehiddenFile']").val()})),
                contentType: "application/json"
            }, callback, false);

        });

    });
})();