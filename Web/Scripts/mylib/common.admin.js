



/**后台常用的js的封装
*author:TT
*@date:2017/5/18
*@version:1.1
**/

function adminPackage() {

    var virtualPath = "";//需要发在在虚拟目录可进行配置

    //datagird相关
    var grid = "";
    var toolbar = "";
    var form ="";//数据表单
    var basePath = "";
    var button = {
        disabled: function () {
            $('#ok').prop("disabled", true);
        },
        enable: function () {
            $('#ok').prop("disabled", false);
        }
    }
 
    /*bootstrap表单验证封装
    *ValidatorForm:需要验证的表单
    *ValidatorParam：需要验证的参数
    */
    function formValidator(ValidatorForm, ValidatorParam) {
        ValidatorForm.bootstrapValidator({
            message: 'This value is not valid',
            feedbackIcons: {
                valid: 'glyphicon glyphicon-ok',
                invalid: 'glyphicon glyphicon-remove',
                validating: 'glyphicon glyphicon-refresh'
            },
            fields: ValidatorParam
        });
    }

    /*注册路由 
    @params area:域名
            control:控制器名
    */
    function routeRegister(area, control) {
        basePath = virtualPath + "/" + area + "/" + control + "/";
        grid = $('#' + control);
        toolbar = $('#' + control + "_toolbar");
        form = $('#dataForm');
        //****动态改变返回值*****//
        res.form = form;
        res.grid = grid;
        res.toolbar = toolbar;
    }

    //获取url
    function route(action, value) {
        if (value) {
            return virtualPath + action;
        }
        else
            return basePath + action;
    }

    //bootstrap的模态框的封装
    //创建模态框 @params:url
    function creatModal(url,title) {
        var res = url.indexOf("?");
        if (res > -1)
            url = url + "&time=" + new Date().getTime();
        else url = url + "?time=" + new Date().getTime();
        var element = $('<div/ class="modal fade" role="dialog" aria-hidden="false" id="MyModalDialog">').
                modal({ remote: url, backdrop: 'static' });//backdrop:static 禁止点击无效区域关闭对话框

        ////绑定拖动事件
        //$(element).on("shown.bs.modal", function () {
        //    $(element).draggable({
        //        handle: ".modal-header"   // 只能点击头部拖动
        //    });
        //});
        //$(element).on("loaded.bs.modal", function () {
        //    debugger
        //    $(".modal-title", element).text("fdsfdsfds");
        //});

        $(element).css("overflow", "scroll");  //允许模态对话框的半透明背景滚动
        //创建成功后绑定隐藏时移除元素
        $(element).on("hidden.bs.modal", function () {
            $(element).remove();
        });
        return $(element);
    }

    //关闭模态框
    function closeModal() {
        setTimeout(function () {
            $('#MyModalDialog').modal('hide');
        }, 1000);
    }

    //提示框 message提示的消息
    function tip(message) {
       if ($('.mytip').length > 0) {
           $('.mytip span').text(message);
        }
        else {
            var html = [];
            html.push('<div class="alert alert-warning mytip">');
            html.push('<a href="#" class="close" data-dismiss="alert">&times;</a>');
            html.push('<strong>提示！</strong><span>' + message + '</span></div>');
            $('.modal-body').append(html.join(''));
        }
    }

    //弹出框
    function alert(tip,time,callback) {
        var html = [];
        html.push('<div class="modal fade" role="dialog" aria-hidden="true" id="myAlert">');
        html.push('<div class="modal-dialog">');
        html.push('<div class="modal-content">');
        html.push('<div class="modal-header">');
        html.push('<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>');
        html.push('<h4 class="modal-title">提示</h4>');
        html.push('</div>');
        html.push('<div class="modal-body">');
        html.push('<div class="alert alert-warning mytip">');
        html.push('<a href="#" class="close" >&times;</a>');
        html.push('<strong>提示！</strong><span>' + tip + '</span></div>');
        html.push('</div>');
        html.push('<div class="modal-footer" style="padding:10px;">');
        html.push('<button id="closeAlert" type="button" class="btn btn-primary">确认</button>');
        html.push('</div>');
        html.push('</div>');
        html.push('</div>');
        $('body').append(html.join(''));
        $('#myAlert').modal();

        //绑定隐藏时移除元素
        $('#myAlert').on("hidden.bs.modal", function () {
            $('#myAlert').remove();
        });

        $('#closeAlert').on("click", function () {
            $('#myAlert').modal('hide');
            if (callback) callback();//回调函数
        });

        if (time) {
            setTimeout(function () {
                $('#myAlert').modal('hide');
            }, time);
        }
    }

    //确认弹出框
    function confirm(tip, callback) {
        var html = [];
        html.push('<div class="modal fade"  role="dialog" aria-hidden="true" id="myConfirm">');
        html.push('<div class="modal-dialog">');
        html.push('<div class="modal-content">');
        html.push('<div class="modal-header">');
        html.push('<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>');
        html.push('<h4 class="modal-title">提示</h4>');
        html.push('</div>');
        html.push('<div class="modal-body">' + tip + '');
        html.push('</div>');
        html.push('<div class="modal-footer" style="padding:10px;">');
        html.push('<button type="button" class="btn btn-default" data-dismiss="modal">取消</button>');
        html.push('<button id="confirm" type="button" class="btn btn-primary">确认</button>');
        html.push('</div>');
        html.push('</div>');
        html.push('</div>');
        $('body').append(html.join(''));
        $('#myConfirm').modal();
        $('#confirm').on("click", function () {
            if (callback) {
                $('#myConfirm').modal('hide');
                callback();
            }
        });

        //绑定隐藏时移除元素
        $('#myConfirm').on("hidden.bs.modal", function () {
            $('#myConfirm').remove();
        });

        $('#myConfirm').prev().on("click", function () {
            $('#myConfirm').modal('hide');
        });
    }

    //字符串的格式化函数
    function formate(str,replaceStr) {
        return str.replace(/#/g, replaceStr);
    }

    /*
    * ajax的post请求的封装（form）.
    * @author [汤台]
    * @version 1.0.0
    * @param   url,data,callback,er_callback,async
    * @return {void}
    */
    function Ajax(url, data, callback, er_callback, async) {
        $.ajax({
            url: url,
            data: data,
            type: 'POST',
            dataType: 'json',
            async: (async == null) ? true : async,
            success: function (r) {
                if (!r.over) { /*判断登录是否过期*/
                    if (r.success) {
                        callback(r);
                    }
                    else {
                        if (er_callback) {
                            er_callback(r);//手动提示错误
                        }
                        else {
                            if ($('#MyModalDialog').length > 0) {
                                tip(r.info);
                                button.enable();
                            }
                            else
                                alert(r.info);
                        }
                    }
                }
                else {
                    //f.alert("登录过期,请重新登录");
                    //location.href = "/Admin/Home/Tip";
                }
            },
            // jqXHR 是经过jQuery封装的XMLHttpRequest对象
            // textStatus 可能为null、 'timeout（超时）'、 'error（错误）'、 'abort(中止)'和'parsererror（解析错误)'等
            // errorMsg 是错误信息字符串(响应状态的文本描述部分，例如'Not Found'或'Internal Server Error')
            error: function (jqXHR, textStatus, errorMsg) {
                switch (jqXHR.status) {
                    case 404:alert('链接地址错误!'); break;
                    case 500: alert('服务器内部错误!'); break;
                    default: alert(jqXHR.status + ":" + jqXHR.statusText);
                }
                button.enable();
            }
        });
    }

    //返回结果
    var res={
        creatModal: creatModal,
        closeModal: closeModal,
        virtualPath: virtualPath,
        routeRegister: routeRegister,
        route: route,
        Ajax: Ajax,
        tip: tip,
        alert:alert,
        confirm: confirm,
        button: button,
        formValidator: formValidator,
        formate:formate
    }
    return res;

}

//**对js原生的属性扩展***//

//数组的扩展
Array.prototype.toObject = function () {
    var obj = {};
    for (var i = 0; i < this.length; i++) {
        var key = this[i]["name"];
        var value = this[i]["value"];
        obj[key] = value;
    }
    return obj;
}
