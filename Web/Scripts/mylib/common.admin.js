

/**后台常用的js的封装
*author:TT
*@date:2017/5/18
*@version:1.1
**/

function adminPackage() {
    
    var virtualPath = "";//需要发在在虚拟目录可进行配置
    var form = $('#dataForm');//数据表单
    var basePath = "";
    /*注册路由 
    @params area:域名
            control:控制器名
    */
    function routeRegister(area, control) {
        basePath =virtualPath+"/" + area + "/" + control+"/";
    }

    //获取url
    function route(action) {
        return basePath + action;
    }

    //bootstrap的模态框的封装
    //创建模态框 @params:url
    function creatModal(url,title) {
        var res = url.indexOf("?");
        if (res > -1)
            url = url + "&time=" + new Date().getTime();
        else url = url + "?time=" + new Date().getTime();
        var element = $('<div/ class="modal fade"  tabindex="1" role="dialog" aria-hidden="true" id="MyModalDialog">').
                modal({ remote: url });
   
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

    //提示框
    function tip() {

    }

    //弹出框
    function alert() {

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
                            alert(r.info);
                            $('#ok').prop("disabled", false);
                            //f.alert("操作失败,请联系管理员!");
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
                //switch (jqXHR.status) {
                //    case 404: tipAlert('链接地址错误!'); break;
                //    case 500: tipAlert('服务器内部错误!'); break;
                //    default: tipAlert(jqXHR.status + ":" + jqXHR.statusText);
                //}
            }
        });
    }


    return {
        creatModal: creatModal,
        closeModal: closeModal,
        virtualPath: virtualPath,
        routeRegister: routeRegister,
        route: route,
        form: form,
        Ajax: Ajax

    }

}