(function() {
    $(function () {
        var url = virtualDirName + "api/services/app/webSiteAppServer/GetCategoryListAsync";
        var href = "List?categoryId=";
        $.ajax({
            url: url,
            type: "Post",
            data: JSON.stringify({ pageIndex: 1, pageCount: 100 }),
            contentType: "application/json",
            success: function (data) {
                if (data.success) {
                    var $ul = $(".about_title ul");
                    var liHtml = "";
                    var length = data.result.rows.length;
                    for (var i = 0; i < length; i++) {
                        liHtml +=
                            '<li><a href="List?categoryId=' +
                            data.result.rows[i].id + '">>> <span>' +
                            data.result.rows[i].name +
                            '</span></a></li>';
                    }
                    $ul.html(liHtml);
                }
            }
        });

        if (categoryId != "") {
            url = virtualDirName + "api/services/app/webSiteAppServer/GetProductListAsync";
            href = "index?id=";
        }
        $.ajax({
            url: url,
            type: "Post",
            data: JSON.stringify({categoryId:categoryId, pageIndex: 1, pageCount: 100 }),
            contentType: "application/json",
            success: function (data) {
                if (data.success) {
                    var $div = $(".contents");
                    var divHtml = "";
                    var length = data.result.rows.length;
                    for (var i = 0; i < length; i++) {
                        divHtml +=
                            '<div class="cnsz_lists_list"><div class="cnsz_img">' +
                            '<p style="text-align:center"><a href="' + href +
                            data.result.rows[i].id +
                            '"><img src="' + GetUrl(data.result.rows[i].fileId) +
                        '"  onload="DrawImg1(this,200,200);" /></a></p>' +
                            '</div><div class="cnsz_names"><p><a style="font-weight:bold;color:#008837;" href="' + href + data.result.rows[i].id + '">' + data.result.rows[i].name + '</a></p></div></div>';
                    }
                    $div.html(divHtml);
                }
            }
        });

        function GetUrl(imageUrl) {
            var url = virtualDirName + 'Ashx/ThumbImage.ashx?FID=' + imageUrl + '&W=200&H=180';
            return url;
        }
    });


})();

