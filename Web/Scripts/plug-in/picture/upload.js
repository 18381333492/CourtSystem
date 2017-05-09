

    /**
    /* 引用的Js:
    /* jquery.easyui.min.js
    /* @description:单张图片上传插件的封装
    /* @author:[汤台]
    /* @time:2016-09-19
    /* @version:1.0.0
    */

function upload() {

    var element;
    //默认参数
    var defaults = {
        url: "/HandleProgram/PictureUpload.ashx",
        width: 120,
        height: 120,
    }

    /*!
    * method:创建图片上传插件
    * author:[汤台]
    * version:[1.0.0]
    */
    function create(id, option) {
        //替换默认参数
        defaults = $.extend(defaults, option);
        element = document.getElementById(id);
        init();
    }


    /*!
    * method:图片插件初始化
    * author:[汤台]
    * version:[1.0.0]
    */
    function init() {
        $(element).load("/Scripts/plug-in/picture/style.html",
            function (data) {
                $(element).html("");
                data = data.trim();
                $(element).append(data);//追加html
                //初始化图片路径
                if ($(element).next().val() != "") {
                    $(element).find("img").attr("src", $(element).next().val())
                }
                //设置图片的大小
                $(element).find("img").height(defaults.height);
                $(element).find("img").width(defaults.width);
               
                bingEvent();

            });
    }

    /*!
    * method:绑定事件
    * author:[汤台]
    * version:[1.0.0]
    */
    function bingEvent() {
        $(element).find("img").on("click", function () {
            $(element).find("input").click();
        });
        $(element).find("input").on("change", function () {
            if ($(this).val() != "") {
                Up();
            }
        });
    }

    /*!
    * method:上传图片
    * author:[汤台]
    * version:[1.0.0]
    */
    function Up() {
        //利用easyui的表单提交上传图片
        $(element).find("form").form('submit', {
            url: defaults.url,
            //queryParams: { path: option.path },//额外参数
            success: function (data) {
                data = JSON.parse(data);
                if (data.error == 0) {
                    $(element).find("img").attr("src", data.url);
                    $(element).next().val(data.url);
                } else {
                    alert(data.message);
                }
            }
        });
    }

    return {
        create: create,
    }
}
