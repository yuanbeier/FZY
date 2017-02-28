/**
*  Js基类 
   by 贺黎亮 2016-07-20创建
   袁贝尔 20160824 修改
*/
var topevery = {
    /**
     * 遮罩层加载
     * @returns {} 
     */
    ajaxLoading: function () {
        $("<div class=\"datagrid-mask\"></div>").css({ display: "block", width: "100%", height: $(window).height() }).appendTo("body");
        $("<div class=\"datagrid-mask-msg\"></div>").html("正在处理，请稍候...").appendTo("body").css({ display: "block", left: ($(document.body).outerWidth(true) - 190) / 2, top: ($(window).height() - 45) / 2 });
    },
    /**
     * 遮罩层关闭
     * @returns {} 
     */
    ajaxLoadEnd: function () {
        $(".datagrid-mask").remove();
        $(".datagrid-mask-msg").remove();
    },
    /**
     * 
     * @param {} args ajax参数
     * @param {} callback 回调函数
     * @param {} isShowLoading 是否需要加载动态图片
     * @returns {} 
     */
    ajax: function (args, callback, isShowLoading) {
        //采用jquery easyui loading css效果
     
        if (isShowLoading) {
            topevery.ajaxLoading();
        }
        args.url = virtualDirName + args.url;
        args = $.extend({}, { type: "POST", dataType: "json" }, args);
        $.ajax(args).done(function(data) {
                if (isShowLoading) {
                    topevery.ajaxLoadEnd();
                }
                if (callback) {
                    callback(data);
                }
            })
            .fail(function (jqXHR) {
                if (isShowLoading) {
                    topevery.ajaxLoadEnd();
                }
                var json = JSON.parse(jqXHR.responseText);
                //使用window.top属性是为了防止重复提交,如有更改.先联系lmy
                if (json.error.message.indexOf("|") > -1) {
                    window.top.alert("提示", json.error.message.split("|")[1]);
                }
                else if (json.error.message.indexOf("Required permissions") > -1) {
                    window.top.alert("提示", "请配置权限：" + json.error.message.split(":")[1]);
                }
                else {
                    window.top.alert("提示", "操作失败");
                }
            });
    },
    serializeObject : function (form) {
        var o = {};
        $.each(form.serializeArray(), function (index) {
            if (o[this['name']]) {
                o[this['name']] = o[this['name']] + "," + $.trim(this['value']);
            } else {
                o[this['name']] =$.trim(this['value']);
            }
        });
        return o;
    },

    /**
   * 上传控件的初始化
   * @param {} moduleId 模块Id
   * @param {} keyId    申请主体Id
   * @param {} activityInstanceId 环节实例Id
   * @param {} target 目标
   * @returns {} 
   */
    setUploadFile : function (moduleId, keyId, activityInstanceId,target) {
        target = $("#"+target);
        topevery.ajax({
            type: "POST",
            url: "api/services/app/FileRelation/GetFileRDtoList",
            contentType: "application/json",
            data: JSON.stringify({ keyId: keyId, ModuleType: moduleId, ActivityIntanceId: activityInstanceId })
        }, function(row) {
            if (row.success) {
                var data = row.result;
                var hdFileData = "";
                for (var i = 0; i < data.length; i++) {
                    var li = target.next().next().find("ul").clone().html();
                    li = li.replace("{imgUrl}", data[i].imageShowUrl).replace("{imgName}", data[i].fileName).replace("{imgName}", data[i].fileName)
                    .replace("{fileId}", data[i].fileId);
                    target.next().append(li);
                    if (hdFileData === "") {
                        hdFileData = data[i].fileId + "," + data[i].fileName ;
                    } else {
                        hdFileData += ";" + data[i].fileId + "," + data[i].fileName ;
                    }
                }
                //回发时还原hiddenfiled的保持数据
                target.next().next().find("input").val(hdFileData);
            }
        });
    },
 



    /*弹出新窗口 */
    openWindow: function(args) {

        var dialog = ezg.modalDialog({
            width: args.width,
            height: args.height,
            title: args.title,
            url: args.url,
            buttons: args.buttons
        });
        return dialog;
    },
    /* 获取url参数*/
    getQuery: function(name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return decodeURI(r[2]);
        return null;
    },
    /* 处理null */
    proNull: function(parm) {
        return (typeof (parm) == "undefined" || parm == "undefined" || parm == null) ? "" : parm;
    },
    /* 处理数字null */
    proNumNull: function(parm) {
        return (typeof(parm) == "undefined" || parm == null || parm == "" || parm == NaN) ? "0" : parm;
    },
    jsonToDate: function(t, format) { //格式日期 如：jsonToDate(date,"yyyy-MM-dd"):2012-08-14
        if (t == "") return t;
        try {
            var obj = {};
            if ("object" == typeof (t)) {
                obj = t;
            } else {
                t = t.replace("date", "Date");
                obj = eval('new ' + (t.replace(/\//g, '')));
            }
            return obj.format(format);
        } catch (e) {
            return t;
        }
    },
    /* 设置json变量到查看表单 */
    setParmByLookForm: function(args) {
        try {
            for (var item in args.JSON) {
                var domtype = $("#" + item).attr("type");
                var dysetMes = "", checkvalue = 0;
                dysetMes = "$('#" + item + "').html(args.JSON." + item + ");";
                try {
                    eval(dysetMes);
                } catch (e) {
                }
                dysetMes = "$('#" + item + "').val(args.JSON." + item + ");";
                try {
                    eval(dysetMes);
                } catch (e) {
                }
                dysetMes = "$('#" + item + "').textbox('setValue',args.JSON." + item + ");";
                try {
                    eval(dysetMes);
                } catch (e) {
                }
            }
        } catch (e) {
        }
    },
    /*获取图片缩略图地址 */
    getSmallmg: function(Url) {
        var smallimg = Url.substring(0, Url.lastIndexOf('.')) + "_t" + Url.substring(Url.lastIndexOf('.'));
        return smallimg;
    },
    /* 计算日期相差月数量 */
    DateDiff: function(interval, date1, date2) {
        var long = date2.getTime() - date1.getTime(); //相差毫秒  
        switch (interval.toLowerCase()) {
        case "y":
            return parseInt(date2.getFullYear() - date1.getFullYear());
        case "m":
            return parseInt((date2.getFullYear() - date1.getFullYear()) * 12 + (date2.getMonth() - date1.getMonth()));
        case "d":
            return parseInt(long / 1000 / 60 / 60 / 24);
        case "w":
            return parseInt(long / 1000 / 60 / 60 / 24 / 7);
        case "h":
            return parseInt(long / 1000 / 60 / 60);
        case "n":
            return parseInt(long / 1000 / 60);
        case "s":
            return parseInt(long / 1000);
        case "l":
            return parseInt(long);
        }
    },
    /*获取提交值*/
    GetFormVal: function(formid) {
        var objs = $("input,textarea", $("#" + formid));
        var postdata = {};
        try {
            objs.each(function() {
                var o = $(this);


                if (o.attr("type") == "radio" && o.attr("name") != "") {
                    field = o.attr("name");
                    postdata[field] = $("input[type='radio'][name='" + field + "']:checked").val();
                    return true;
                }

                if (typeof (o) != "undefined" && o.attr("id")) {
                    var field = o.attr("id");
                    if (o.isTag("input")) {
                        if (o[0].type == "text" || o[0].type == "hidden" || o[0].type == "password") {
                            if (o.hasClass("iptval") && o.attr("rel") == o.val())
                                postdata[field] = "";
                            else
                                postdata[field] = o.val();
                        } else if (o.hasClass("checkbox")) {
                            postdata[field] = o.attr("checked") ? 1 : 0
                        }
                    } else if (o.isTag("textarea")) {
                        postdata[field] = o.val();
                    } else if (o.isTag("select")) {
                        postdata[field] = o.val();
                    } else if (o.isTag("radio")) {
                        postdata[field] = $("input[type='radio'][name='" + field + "']:checked").val();
                    }
                }
            });
        } catch (e) {
            alert(e);
        }
        return postdata;
    },
    /*数字转大写*/
    convertCurrency: function(currencyDigits) {
        // Constants: 
        var MAXIMUM_NUMBER = 99999999999.99;
        // Predefine the radix characters and currency symbols for output: 
        var CN_ZERO = "零";
        var CN_ONE = "壹";
        var CN_TWO = "贰";
        var CN_THREE = "叁";
        var CN_FOUR = "肆";
        var CN_FIVE = "伍";
        var CN_SIX = "陆";
        var CN_SEVEN = "柒";
        var CN_EIGHT = "捌";
        var CN_NINE = "玖";
        var CN_TEN = "拾";
        var CN_HUNDRED = "佰";
        var CN_THOUSAND = "仟";
        var CN_TEN_THOUSAND = "万";
        var CN_HUNDRED_MILLION = "亿";
        var CN_SYMBOL = "人民币";
        var CN_DOLLAR = "元";
        var CN_TEN_CENT = "角";
        var CN_CENT = "分";
        var CN_INTEGER = "整";

        // Variables: 
        var integral; // Represent integral part of digit number. 
        var decimal; // Represent decimal part of digit number. 
        var outputCharacters; // The output result. 
        var parts;
        var digits, radices, bigRadices, decimals;
        var zeroCount;
        var i, p, d;
        var quotient, modulus;

        // Validate input string: 
        currencyDigits = currencyDigits.toString();
        if (currencyDigits == "") {
            //alert("Empty input!"); 
            return "";
        }
        if (currencyDigits.match(/[^,.\d]/) != null) {
            alert("Invalid characters in the input string!");
            return "";
        }
        if ((currencyDigits).match(/^((\d{1,3}(,\d{3})*(.((\d{3},)*\d{1,3}))?)|(\d+(.\d+)?))$/) == null) {
            alert("Illegal format of digit number!");
            return "";
        }

        // Normalize the format of input digits: 
        currencyDigits = currencyDigits.replace(/,/g, ""); // Remove comma delimiters. 
        currencyDigits = currencyDigits.replace(/^0+/, ""); // Trim zeros at the beginning. 
        // Assert the number is not greater than the maximum number. 
        if (Number(currencyDigits) > MAXIMUM_NUMBER) {
            alert("Too large a number to convert!");
            return "";
        }

        // Process the coversion from currency digits to characters: 
        // Separate integral and decimal parts before processing coversion: 
        parts = currencyDigits.split(".");
        if (parts.length > 1) {
            integral = parts[0];
            decimal = parts[1];
            // Cut down redundant decimal digits that are after the second. 
            decimal = decimal.substr(0, 2);
        } else {
            integral = parts[0];
            decimal = "";
        }
        // Prepare the characters corresponding to the digits: 
        digits = new Array(CN_ZERO, CN_ONE, CN_TWO, CN_THREE, CN_FOUR, CN_FIVE, CN_SIX, CN_SEVEN, CN_EIGHT, CN_NINE);
        radices = new Array("", CN_TEN, CN_HUNDRED, CN_THOUSAND);
        bigRadices = new Array("", CN_TEN_THOUSAND, CN_HUNDRED_MILLION);
        decimals = new Array(CN_TEN_CENT, CN_CENT);
        // Start processing: 
        outputCharacters = "";
        // Process integral part if it is larger than 0: 
        if (Number(integral) > 0) {
            zeroCount = 0;
            for (i = 0; i < integral.length; i++) {
                p = integral.length - i - 1;
                d = integral.substr(i, 1);
                quotient = p / 4;
                modulus = p % 4;
                if (d == "0") {
                    zeroCount++;
                } else {
                    if (zeroCount > 0) {
                        outputCharacters += digits[0];
                    }
                    zeroCount = 0;
                    outputCharacters += digits[Number(d)] + radices[modulus];
                }
                if (modulus == 0 && zeroCount < 4) {
                    outputCharacters += bigRadices[quotient];
                    zeroCount = 0;
                }
            }
            outputCharacters += CN_DOLLAR;
        }
        // Process decimal part if there is: 
        if (decimal != "") {
            for (i = 0; i < decimal.length; i++) {
                d = decimal.substr(i, 1);
                if (d != "0") {
                    outputCharacters += digits[Number(d)] + decimals[i];
                }
            }
        }
        // Confirm and return the final output string: 
        if (outputCharacters == "") {
            outputCharacters = CN_ZERO + CN_DOLLAR;
        }
        if (decimal == "") {
            outputCharacters += CN_INTEGER;
        }
        outputCharacters = CN_SYMBOL + outputCharacters;
        return outputCharacters;
    },
    /*得到新增日期后的值*/
    DateAdd: function(interval, number, date) {
        switch (interval.toLowerCase()) {
        case "y":
            return new Date(date.setFullYear(date.getFullYear() + number));
        case "m":
            return new Date(date.setMonth(date.getMonth() + number));
        case "d":
            return new Date(date.setDate(date.getDate() + number));
        case "w":
            return new Date(date.setDate(date.getDate() + 7 * number));
        case "h":
            return new Date(date.setHours(date.getHours() + number));
        case "n":
            return new Date(date.setMinutes(date.getMinutes() + number));
        case "s":
            return new Date(date.setSeconds(date.getSeconds() + number));
        case "l":
            return new Date(date.setMilliseconds(date.getMilliseconds() + number));
        }
    },
    /* 表单验证 */
    validForm: function(formid) {
        var objs = $("#" + formid + " input")
        var isValid = true;
        try {
            objs.each(function() {
                var o = $(this);
                if (isValid && typeof (o) != "undefined" && o.attr("id")) {
                    var field = o.attr("id");
                    if (o.isTag("input")) {
                        if (o[0].type == "text") {
                            if (o.attr("required") == "required" && o.val() == "") {
                                o.focus();
                                isValid = false;
                                return;
                            }
                        }
                    }
                }
            });
        } catch (e) {
            alert(e);
        }
        return isValid;
    },
    /* 检测Flash这是否安装 */
    flashChecker: function() {
        var hasFlash = 0; //是否安装了flash
        var flashVersion = 0; //flash版本

        if (document.all) {
            var swf = new ActiveXObject('ShockwaveFlash.ShockwaveFlash');
            if (swf) {
                hasFlash = 1;
                VSwf = swf.GetVariable("$version");
                flashVersion = parseInt(VSwf.split(" ")[1].split(",")[0]);
            }
        } else {
            if (navigator.plugins && navigator.plugins.length > 0) {
                var swf = navigator.plugins["Shockwave Flash"];
                if (swf) {
                    hasFlash = 1;
                    var words = swf.description.split(" ");
                    for (var i = 0; i < words.length; ++i) {
                        if (isNaN(parseInt(words[i]))) continue;
                        flashVersion = parseInt(words[i]);
                    }
                }
            }
        }
        return { f: hasFlash, v: flashVersion };
    },
    ///两个对象合并成一个
    extend: function(obj1, obj2) {
        if (obj1 !== null && obj2 !== null) {
            for (var key in obj2) {
                if (obj1.hasOwnProperty(key))continue; //有相同的属性则略过 
                obj1[key] = obj2[key];
            }
            return JSON.stringify(obj1);
        } else if (obj1 !== null) {
            return JSON.stringify(obj1);
        } else if (obj2 !== null) {
            return JSON.stringify(obj2);
        } else {
            return null;
        }
    },
    ///json转换成对象
    form2Json: function(id) {
        var arr = $("#" + id).serializeArray();
        var jsonStr = "";
        jsonStr += '{';
        for (var i = 0; i < arr.length; i++) {
            jsonStr += '"' + arr[i].name + '":"' + $.trim(arr[i].value) + '",';
        }
        jsonStr = jsonStr.substring(0, (jsonStr.length - 1));
        jsonStr += '}';
        var json = JSON.parse(jsonStr);
        return json;
    },
    ///导出时查询对象装换成url
    ExportUrl:function(input) {
        var url = "";
        var j = 0;
        for (var i in input) {//用javascript的for/in循环遍历对象的属性 
            if (j === 0) {
                url += "?" + i + "=" + input[i] + "";
            } else {
                url += "&" + i + "=" + input[i] + "";
            }
            j++;
        }
        return url;
    },

    ///时间格式装换  
    dataTimeFormat: function(datatime) {
        if (datatime !== null) {
            var data = datatime.split("T")[0];
            return data;
        } else {
            {
                return null;
            }
        }
    },
    ///时间格式装换  
    dataTimeFormat1: function (datatime) {
        if (datatime !== null) {
            var data = datatime.split("T")[1].substring(0,5);
            return data;
        } else {
            {
                return null;
            }
        }
    },
    dataTimeFormatTT: function(datatime) {
        if (datatime !== null) {
            var data = datatime.replace("T", " ");
            return data;
        } else {
            {
                return null;
            }
        }
    },
    ///0,1 check格式转换
    checkFormat: function(value) {
        if (value === 1) return "<input type='checkbox' checked='checked' disabled='disabled' />";
        else if (value === 0) return "<input type='checkbox' disabled='disabled' />";
        else return "";
    },
    ///清空查询数据，并且查询
    Clear: function (myform, table) {
        $("#" + myform + "").form('clear');
        $("#" + table + "").datagrid('load');
    },
    ///附件展示公共方法  id  申请表主键编号，type 相关模块，attachmentId  要绑定内容的编号  wodth 图片宽度
    PicturesShow: function(id, type, attachmentId, width,height) {
        if (width == null || width === "") {
            width = 30;
        }
        if (!height) {
            height = 30;
        }
        topevery.ajax({
            type: "POST",
            url: "api/services/app/FileRelation/GetFileRDtoList",
            contentType: "application/json",
            data: JSON.stringify({ KeyId: id, ModuleType: type })
        }, function(row) {
            if (row.success) {
                var data = row.result;
                var html = "";
                for (var i = 0; i < data.length; i++) {
                    var suffix = data[i].fileName.split('.')[1];
                    switch (suffix) {
                    case "xlsx":
                        html += "<a title=\"" + data[i].fileName + "\" href=\"" + data[i].fileDownUrl + "\" target=\"_blank\"><img width='" + width + "' src=\"" +virtualDirName+ "Images/xlsx.jpg\"> <img/>" + data[i].fileName + "</a>";
                        break;
                        case "xls":
                            html += "<a title=\"" + data[i].fileName + "\" href=\"" + data[i].fileDownUrl + "\" target=\"_blank\"><img width='" + width + "' src=\"" +virtualDirName+ "Images/xlsx.jpg\"> <img/>" + data[i].fileName + "</a>";
                            break;
                    case "doc":
                        html += "<a title=\"" + data[i].fileName + "\" href=\"" + data[i].fileDownUrl + "\" target=\"_blank\"><img width='" + width + "' src=\"" + virtualDirName+"Images/Word.png\"> <img/>" + data[i].fileName + "</a>";
                        break;
                    case "docx":
                        html += "<a title=\"" + data[i].fileName + "\" href=\"" + data[i].fileDownUrl + "\" target=\"_blank\"><img width='" + width + "' src=\"" + virtualDirName+"Images/Word.png\">  <img/>" + data[i].fileName + "</a>";
                        break;
                    case "PDF":
                        html += "<a title=\"" + data[i].fileName + "\" href=\"" + data[i].fileDownUrl + "\" target=\"_blank\"><img width='" + width + "' src=\"" +virtualDirName+ "Images/pdf.png\">  <img/>" + data[i].fileName + "</a>";
                        break;
                    case "pdf":
                        html += "<a title=\"" + data[i].fileName + "\" href=\"" + data[i].fileDownUrl + "\" target=\"_blank\"><img width='" + width + "' src=\"" + virtualDirName+"Images/pdf.png\">  <img/>" + data[i].fileName + "</a>";
                        break;
                    case "pptx":
                        html += "<a title=\"" + data[i].fileName + "\" href=\"" + data[i].fileDownUrl + "\" target=\"_blank\"><img width='" + width + "' src=\"" +virtualDirName+ "Images/PPT.png\">  <img/>" + data[i].fileName + "</a>";
                        break;
                    default:
                        html += "<a title=\"" + data[i].fileName + "\" href=\"" + data[i].imageShowUrl + "\" target=\"_blank\"><img width='" + width + "' height='" + height + "' href=\"" + data[i].fileDownUrl + "\" src=\"" + data[i].imageShowUrl + "\"> <img/>" + data[i].fileName + "</a>";
                        break;
                    }
                }
                $("#" + attachmentId + "").append(html);
            } else {
                error();
            }
        }, true);
    },
    ImgShow: function (id, type, attachmentId, width, height) {
        if (width) {
            width = 100;
        };
        if (!height) {
            height = 100;
        };
        topevery.ajax({
            type: "POST",
            url: "api/services/app/FileRelation/GetFileRDtoList",
            contentType: "application/json",
            data: JSON.stringify({ KeyId: id, ModuleType: type })
        }, function (row) {
            if (row.success) {
                var data = row.result;
                var html = "<ul class='photo-list'>";
                for (var i = 0; i < data.length; i++) {
                    var suffix = data[i].fileName.split('.')[1];
                    html += "<li><a title=\"" + data[i].fileName + "\" href=\"" + data[i].imageShowUrl + "\" target=\"_blank\"><img width='" + width + "' height='" + height + "' href=\"" + data[i].fileDownUrl + "\" src=\"" + data[i].imageShowUrl + "\"> <img/></a><p>" + data[i].fileName + "</p></li>"
                }
                html += "</ul>";
                $("#" + attachmentId + "").append(html);
            } else {
                error();
            }
        }, true);
    },
    ProcessAttachment: function(data, width) {
        if (width == null || width === "") {
            width = 30;
        }
        var html = "<div style='margin:2px;'>";
        for (var i = 0; i < data.length; i++) {
            var suffix = data[i].fileName.split('.')[1];
            switch (suffix) {
            case "xlsx":
                html += "<a title=\"" + data[i].fileName + "\" href=\"" + data[i].fileDownUrl + "\" target=\"_blank\"><img width='" + width + "' height='30' src=\"" + virtualDirName+"Images/xlsx.jpg\"> </a>";
                break;
            case "xls":
                html += "<a title=\"" + data[i].fileName + "\" href=\"" + data[i].fileDownUrl + "\" target=\"_blank\"><img width='" + width + "' height='30' src=\"" + virtualDirName+"Images/xlsx.jpg\"> </a>";
                break;
            case "doc":
                html += "<a title=\"" + data[i].fileName + "\" href=\"" + data[i].fileDownUrl + "\" target=\"_blank\"><img width='" + width + "' height='30' src=\"" + virtualDirName+"Images/Word.png\"> </a>";
                break;
            case "docx":
                html += "<a title=\"" + data[i].fileName + "\" href=\"" + data[i].fileDownUrl + "\" target=\"_blank\"><img width='" + width + "' height='30' src=\"" + virtualDirName+"Images/Word.png\">  </a>";
                break;
            case "PDF":
                html += "<a title=\"" + data[i].fileName + "\" href=\"" + data[i].fileDownUrl + "\" target=\"_blank\"><img width='" + width + "' height='30' src=\"" + virtualDirName+"Images/pdf.png\">  </a>";
                break;
            case "pdf":
                html += "<a title=\"" + data[i].fileName + "\" href=\"" + data[i].fileDownUrl + "\" target=\"_blank\"><img width='" + width + "' height='30' src=\"" + virtualDirName+"Images/pdf.png\">  </a>";
                break;
            case "pptx":
                html += "<a title=\"" + data[i].fileName + "\" href=\"" + data[i].fileDownUrl + "\" target=\"_blank\"><img width='" + width + "' height='30' src=\"" + virtualDirName+"Images/PPT.png\">  </a>";
                break;
            default:
                html += "<a title=\"" + data[i].fileName + "\" href=\"" + data[i].imageShowUrl + "\" target=\"_blank\"><img width='" + width + "' height='30' href=\"" + data[i].fileDownUrl + "\" src=\"" + data[i].imageShowUrl + "\"> </a>";
                break;
            }
        }
        html += "</div>";
        return html;
    }
}
