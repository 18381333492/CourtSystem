

/**后台常用的js的封装
*author:TT
*@date:2017/5/18
*@version:1.1
**/

function adminPackage() {
    
    //bootstrap的模态框的封装

    //创建模态框 @params:url
    function creatModal(url) {
        var res = url.indexOf("?");
        if (res > -1)
            url = url + "&time=" + new Date().getTime();
        else url = url + "?time=" + new Date().getTime();
        var element = $('<div/ class="modal fade"  tabindex="-1" role="dialog" aria-hidden="true" id="modalDialog">').
                modal({ remote: url });
    
        $(element).css("overflow", "scroll");  //允许模态对话框的半透明背景滚动
        //创建成功后绑定隐藏时移除元素
        $(element).on("hidden.bs.modal", function () {
            $(element).remove();
        });
    }

    //关闭模态框
    function closeModal() {
        setTimeout(function () {
            $('#modalDialog').modal('hide');
        }, 1000);
    }

    //提示框
    function tip() {

    }

    //弹出框
    function alert() {

    }



    return {
        creatModal: creatModal,
        closeModal: closeModal,

    }

}