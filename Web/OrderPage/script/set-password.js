/**
 * Created by zwp on 2016/5/18.
 */
var wait=60;
function time(o) {
    if (wait == 0) {
        o.removeAttribute("disabled");
        o.value="获取验证码";
        wait = 60;
    } else {
        o.setAttribute("disabled", true);
        o.value="重新发送(" + wait + ")";
        wait--;
        setTimeout(function() {
                time(o)
            },
            1000)
    }
};
$('body').find('#js_sure').click(function(){
    //tips('密码设置成功!',2000);
    showToast("密码设置成功!","2","","");
});
