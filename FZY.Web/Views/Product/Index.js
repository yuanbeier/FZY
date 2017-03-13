(function() {
    $(function () {
        $.ajax({
            url: virtualDirName + "api/services/app/webSiteAppServer/GetCategoryListAsync",
            type: "Post",
            data: JSON.stringify({  pageIndex: 1, pageCount: 100 }),
            contentType: "application/json",
            success: function(data) {
                if (data.success) {
                    var $ul = $(".about_title ul");
                    var liHtml = "";
                    var length = data.result.rows.length;
                    for (var i = 0; i < length; i++) {
                        liHtml +=
                            '<li><a href="Index?id=' +
                            data.result.rows[i].id + '">>> <span>' +
                            data.result.rows[i].name +
                            '</span></a></li>';
                      
                    }
                    $ul.html(liHtml);
                }
            }
        });

        topevery.ajax({
            type: "POST",
            url: "api/services/app/FileRelation/GetFileRDtoList",
            contentType: "application/json",
            data: JSON.stringify({ keyId: productId, ModuleType: 1})
        }, function (row) {
            if (row.success) {
                var data = row.result;
                
                for (var i = 0; i < data.length; i++) {
                    $("#manPic").attr("src", GetUrl(data[i].imageShowUrl,680,330));
                }
        
            }
        });

        topevery.ajax({
            type: "POST",
            url: "api/services/app/FileRelation/GetFileRDtoList",
            contentType: "application/json",
            data: JSON.stringify({ keyId: productId, ModuleType: 2 })
        }, function (row) {
            if (row.success) {
                var data = row.result;
                var hdFileData = "";
                for (var i = 0; i < data.length; i++) {
                    hdFileData += '<li><img src="' +
                       GetUrl(data[i].imageShowUrl,100,100) +
                        '" width="100" Height="100" alt="Nozomi">' +
                        '<br>' + data[i].fileNameWithoutExten + '</li>';

                }
                //回发时还原hiddenfiled的保持数据
                $(".prodcuticon").html(hdFileData);
            }
        });

        function GetUrl(imageUrl,width,heigth) {
            var url =  imageUrl + '&W='+width+'&H='+ heigth;
            return url;
        }
    });


})();

